using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PreferenceData
{
    public string title;
    public string category;
    public Sprite image;
    public string imageName;

    public PreferenceData(string title, string category, Sprite image, string imageName)
    {
        this.title = title;
        this.category = category;
        this.image = image;
        this.imageName = imageName;
    }
}

[System.Serializable]
public class TimelineEntry
{
    public string actionText;
    public string metaText;

    public TimelineEntry(string actionText, string metaText)
    {
        this.actionText = actionText;
        this.metaText = metaText;
    }
}

public class UserData : MonoBehaviour
{
    public static UserData Instance;

    public List<PreferenceData> savedPreferences = new List<PreferenceData>();
    public List<TimelineEntry> timelineEntries = new List<TimelineEntry>();
    public string username = "user";

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

    public void AddPreference(string title, string category, Sprite image, string imageName)
    {
        savedPreferences.Add(new PreferenceData(title, category, image,imageName));

        string date = DateTime.Now.ToString("MMM dd");
        timelineEntries.Add(new TimelineEntry("Added " + title, category + " • " + date));
        SaveManager.Instance.SaveData();
        FirebaseDataManager.Instance.SaveAll();
    }

    public void SetUsername(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            username = "user";
        else
            username = userName.Trim();
        SaveManager.Instance.SaveData();
    }

    
  
}