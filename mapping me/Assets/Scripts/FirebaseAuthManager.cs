using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;

public class FirebaseAuthManager : MonoBehaviour
{
    public static FirebaseAuthManager Instance;

    private FirebaseAuth auth;
    public bool IsFirebaseReady { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                IsFirebaseReady = true;
                Debug.Log("Firebase Auth is ready.");
            }
            else
            {
                Debug.LogError("Firebase dependency error: " + task.Result);
            }
        });
    }

    public void SignUp(string email, string password, System.Action<bool, string> callback)
    {
        if (!IsFirebaseReady)
        {
            callback(false, "Firebase is not ready yet.");
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
                callback(false, "Sign up cancelled.");
            else if (task.IsFaulted)
                callback(false, CleanError(task.Exception));
            else
                callback(true, "Sign up successful.");
        });
    }

    public void Login(string email, string password, System.Action<bool, string> callback)
    {
        if (!IsFirebaseReady)
        {
            callback(false, "Firebase is not ready yet.");
            return;
        }

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
                callback(false, "Login cancelled.");
            else if (task.IsFaulted)
                callback(false, CleanError(task.Exception));
            else
                callback(true, "Login successful.");
        });
    }

    string CleanError(System.AggregateException exception)
    {
        if (exception == null)
            return "Something went wrong.";

        string error = exception.Flatten().Message.ToLower();
        if (error.Contains("badly formatted"))
            return "Please enter a valid email address.";
        

        return "Invalid email or password.";
    }

    string GetSignUpMessage(System.AggregateException exception)
    {
        if (exception == null)
            return "Sign up failed.";

        string error = exception.Flatten().Message.ToLower();

        if (error.Contains("badly formatted"))
            return "Please enter a valid email address.";

        if (error.Contains("at least 6"))
            return "Password must be at least 6 characters.";

        if (error.Contains("already in use"))
            return "This email is already registered.";

        return "Sign up failed.";
    }

    public void Logout()
    {
        if (auth != null)
            auth.SignOut();
    }
}