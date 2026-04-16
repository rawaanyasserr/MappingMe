using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SavePreferenceData
{
    public string title;
    public string category;
    public string imageName;
}

[System.Serializable]
public class SaveData
{
    public string username;
    public List<SavePreferenceData> preferences = new List<SavePreferenceData>();
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    string savePath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Save Manager awake.");
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        savePath = Path.Combine(Application.persistentDataPath, "saveData_v3.json");
        Debug.Log( "Save Data saved to: " + savePath);
    }

    void Start()
    {
        Debug.Log("Save Data started.");
      
        LoadData();
    }

    public void SaveData()
    {
        Debug.Log("Save Data called.");
        if (UserData.Instance == null)
        {
            Debug.Log("Save Data called without UserData.Instance");
            return;
        }
           

        SaveData data = new SaveData();
        
        data.username = UserData.Instance.username;

        foreach (var pref in UserData.Instance.savedPreferences)
        {
            SavePreferenceData p = new SavePreferenceData();
            p.title = pref.title;
            p.category = pref.category;
            p.imageName = pref.imageName;

            data.preferences.Add(p);
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Saved to: " + savePath);
    }

    public void LoadData()
    {
        if (!File.Exists(savePath) || UserData.Instance == null) return;

        string json = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        if (data == null) return;

        UserData.Instance.username = data.username;
        UserData.Instance.savedPreferences.Clear();

        foreach (var pref in data.preferences)
        {
            string path = "Images/" + pref.imageName;
            Sprite sprite = Resources.Load<Sprite>("Images/" + pref.imageName);
            Debug.Log("Trying to load" + path + (sprite != null));
            if (sprite == null)
            {
                Debug.Log("Missing image: " + pref.imageName);
                continue;
            }
            UserData.Instance.savedPreferences.Add(
                new PreferenceData(pref.title, pref.category, sprite, pref.imageName)
            );
        }

        Debug.Log("Data loaded.");
    }
    
}