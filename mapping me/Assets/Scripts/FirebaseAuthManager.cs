using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;

public class FirebaseAuthManager : MonoBehaviour
{
    public static FirebaseAuthManager Instance;

    private FirebaseAuth auth;
    public bool IsFirebaseReady { get; private set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var status = task.Result;

            if (status == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                IsFirebaseReady = true;
                Debug.Log("Firebase Auth is ready.");
            }
            else
            {
                Debug.LogError("Firebase dependency error: " + status);
            }
        });
    }

    public void SignUp(string email, string password, System.Action<bool, string> callback)
    {
        if (!IsFirebaseReady)
        {
            callback?.Invoke(false, "Firebase is not ready yet.");
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                callback?.Invoke(false, "Sign up cancelled.");
                return;
            }

            if (task.IsFaulted)
            {
                callback?.Invoke(false, task.Exception != null ? task.Exception.Flatten().Message : "Sign up failed.");
                return;
            }

            callback?.Invoke(true, "Sign up successful.");
        });
    }

    public void Login(string email, string password, System.Action<bool, string> callback)
    {
        if (!IsFirebaseReady)
        {
            callback?.Invoke(false, "Firebase is not ready yet.");
            return;
        }

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                callback?.Invoke(false, "Login cancelled.");
                return;
            }

            if (task.IsFaulted)
            {
                callback?.Invoke(false, task.Exception != null ? task.Exception.Flatten().Message : "Login failed.");
                return;
            }

            callback?.Invoke(true, "Login successful.");
        });
    }

    public FirebaseUser GetCurrentUser()
    {
        return auth != null ? auth.CurrentUser : null;
    }

    public void Logout()
    {
        if (auth != null)
        {
            auth.SignOut();
            Debug.Log("User logged out.");
        }
    }
}