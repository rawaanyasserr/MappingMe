using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PreferenceData
{
    public string title;
    public string category;
    public Sprite image;

    public PreferenceData(string title, string category, Sprite image)
    {
        this.title = title;
        this.category = category;
        this.image = image;
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

    public string username = "Rue";

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
        }
    }

    public void SetUsername(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            username = "Rue";
            return;
        }

        firstName = firstName.Trim();
        username = char.ToUpper(firstName[0]) + firstName.Substring(1).ToLower();
        Debug.Log("Username set to: " + username);
    }

    public void AddPreference(string title, string category, Sprite image)
    {
        savedPreferences.Add(new PreferenceData(title, category, image));
        timelineEntries.Add(
            new TimelineEntry(
                "Added " + title,
                category + " • " + DateTime.Now.ToString("MMM dd")
            )
        );

        Debug.Log("Saved preference: " + title + " | " + category);
    }

    public void ClearPreferences()
    {
        savedPreferences.Clear();
        timelineEntries.Clear();
        Debug.Log("Cleared saved preferences and timeline.");
    }
}