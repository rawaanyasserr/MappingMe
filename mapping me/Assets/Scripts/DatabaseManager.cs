using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using SQLite4Unity3d;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;

    private string dbPath;
    private SQLiteConnection db;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitDB();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitDB()
    {
        dbPath = Path.Combine(Application.persistentDataPath, "mappingme.db");
        Debug.Log("DB path: " + dbPath);

        db = new SQLiteConnection(dbPath);
        db.CreateTable<PreferenceRecord>();
    }

    public void SavePreference(string title, string category)
    {
        PreferenceRecord record = new PreferenceRecord
        {
            Title = title,
            Category = category
        };

        db.Insert(record);
        Debug.Log("Saved to database: " + title + " | " + category);
    }

    public List<PreferenceRecord> GetAllPreferences()
    {
        return db.Table<PreferenceRecord>().ToList();
    }

    public void ClearAllPreferences()
    {
        db.DeleteAll<PreferenceRecord>();
        Debug.Log("All preferences deleted.");
    }
}

public class PreferenceRecord
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Title { get; set; }
    public string Category { get; set; }
}