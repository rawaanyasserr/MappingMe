using System;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;

public class FirebaseDataManager : MonoBehaviour
{
    public static FirebaseDataManager Instance;
    DatabaseReference db;

    void Awake()
    {
        Instance = this;
        db = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveAll()
    {
        var user = FirebaseAuth.DefaultInstance.CurrentUser;
        if (user == null) return;

        string json = JsonUtility.ToJson(UserData.Instance);
        db.Child("users").Child(user.UserId).SetRawJsonValueAsync(json);
    }

    public void LoadAll(Action onComplete = null)
    {
        UserData.Instance.timelineEntries.Clear();

        var user = FirebaseAuth.DefaultInstance.CurrentUser;
        if (user == null)
        {
            onComplete?.Invoke();
            return;
        }

        db.Child("users").Child(user.UserId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && task.Result != null && task.Result.Exists)
            {
                string json = task.Result.GetRawJsonValue();
                JsonUtility.FromJsonOverwrite(json, UserData.Instance);

                foreach (var p in UserData.Instance.savedPreferences)
                    p.image = Resources.Load<Sprite>("Images/" + p.imageName);
            }

            onComplete?.Invoke();
        });
    }
}