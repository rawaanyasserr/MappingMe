using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapManager : MonoBehaviour
{
    public TMP_Text titleText;
    public Image cameraPlaceholder;
    public Image musicPlaceholder;
    public Image tvPlaceholder;
    public TMP_Text noteText;

    void Start()
    {
        if (UserData.Instance == null)
        {
            Debug.LogError("UserData.Instance is NULL.");
            return;
        }

        if (titleText != null)
        {
            titleText.text = UserData.Instance.username + "'s vibe";
        }

        UpdateMap();
    }

    void UpdateMap()
{
    var prefs = UserData.Instance.savedPreferences;

    if (prefs == null || prefs.Count == 0)
    {
        if (noteText != null)
            noteText.text = "No preferences selected yet.";
        return;
    }

    ClearImage(cameraPlaceholder);
    ClearImage(musicPlaceholder);
    ClearImage(tvPlaceholder);
    

    PreferenceData latestMusic = null;
    PreferenceData latestMovie = null;
    PreferenceData latestLifestyle = null;

    // go backwards so we get the latest saved item in each category
    for (int i = prefs.Count - 1; i >= 0; i--)
    {
        var pref = prefs[i];

        if (latestMusic == null && pref.category == "Music")
            latestMusic = pref;

        if (latestMovie == null && pref.category == "Movie")
            latestMovie = pref;

        if (latestLifestyle == null &&
            (pref.category == "Aesthetic" ||
             pref.category == "Place" ||
             pref.category == "Drink" ||
             pref.category == "Fashion"))
        {
            latestLifestyle = pref;
        }

        if (latestMusic != null && latestMovie != null && latestLifestyle != null)
            break;
    }
    Debug.Log("Lifestyle chosen: " + (latestLifestyle != null ? latestLifestyle.title + " | " + latestLifestyle.category : "NONE"));
    Debug.Log("Music chosen: " + (latestMusic != null ? latestMusic.title + " | " + latestMusic.category : "NONE"));
    Debug.Log("Movie chosen: " + (latestMovie != null ? latestMovie.title + " | " + latestMovie.category : "NONE"));

    Debug.Log("cameraPlaceholder object = " + (cameraPlaceholder != null ? cameraPlaceholder.name : "NULL"));
    Debug.Log("musicPlaceholder object = " + (musicPlaceholder != null ? musicPlaceholder.name : "NULL"));
    Debug.Log("tvPlaceholder object = " + (tvPlaceholder != null ? tvPlaceholder.name : "NULL"));
    // assign images + keep your old animation
    if (latestMusic != null && musicPlaceholder != null)
    {
        SetImage(musicPlaceholder, latestMusic.image);
        StartCoroutine(DropFade(musicPlaceholder));
    }

    if (latestMovie != null && tvPlaceholder != null)
    {
        SetImage(tvPlaceholder, latestMovie.image);
        StartCoroutine(DropFade(tvPlaceholder));
    }

    if (latestLifestyle != null && cameraPlaceholder != null)
    {
        SetImage(cameraPlaceholder, latestLifestyle.image);
        StartCoroutine(DropFade(cameraPlaceholder));
    }

    string result = "";

    if (latestLifestyle != null)
        result += "currently into: " + latestLifestyle.title + "\n";

    if (latestMusic != null)
        result += "• soundtrack: " + latestMusic.title + "\n";

    if (latestMovie != null)
        result += "• comfort watch: " + latestMovie.title;

    if (string.IsNullOrWhiteSpace(result))
        result = "Your map is still growing...";

    if (noteText != null)
    {
        noteText.text = "";
        StartCoroutine(TypeText(result.Trim()));
    }
}

    void SetImage(Image target, Sprite sprite)
    {
        if (target == null || sprite == null)
            return;

        target.sprite = sprite;
        target.color = Color.white;
    }

    void ClearImage(Image target)
    {
        if (target != null)
        {
            target.sprite = null;
            target.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    IEnumerator DropFade(Image img)
    {
        if (img == null)
            yield break;

        
        Color c = img.color;
        c.a = 0f;
        img.color = c;

        float duration = 0.22f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float p = time / duration;
            

            // fade in
            img.color = new Color(1f, 1f, 1f, p);

            yield return null;
        }

        img.color = Color.white;
    }
    IEnumerator TypeText(string fullText)
    {
        noteText.text = "";

        foreach (char c in fullText)
        {
            noteText.text += c;
            yield return new WaitForSeconds(0.035f);
        }
    }
}