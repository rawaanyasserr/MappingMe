using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;

public class FirebaseAuthManager : MonoBehaviour
{
    public static FirebaseAuthManager Instance;
    private FirebaseAuth auth;

    public bool IsReady { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                IsReady = true;
            }
        });
    }

    public void Login(string email, string password, System.Action<bool, string> callback)
    {
        if (!IsReady)
        {
            callback(false, "System not ready.");
            return;
        }

        auth.SignInWithEmailAndPasswordAsync(email, password)
        .ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
                callback(false, "Login cancelled.");
            else if (task.IsFaulted)
                callback(false, CleanError(task.Exception));
            else
                callback(true, "Login successful.");
        });
    }

    public void SignUp(string email, string password, System.Action<bool, string> callback)
    {
        if (!IsReady)
        {
            callback(false, "System not ready.");
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password)
        .ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
                callback(false, "Sign up cancelled.");
            else if (task.IsFaulted)
                callback(false, CleanError(task.Exception));
            else
                callback(true, "Account created.");
        });
    }

    string CleanError(System.AggregateException e)
    {
        if (e == null) return "Something went wrong.";

        string msg = e.Flatten().Message.ToLower();

        if (msg.Contains("badly formatted"))
            return "Invalid email format.";

        return "Invalid email or password.";
    }
    
    
    public void Logout()
    {
        if (auth != null)
            auth.SignOut();
    }
}