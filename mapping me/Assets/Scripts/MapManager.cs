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
        titleText.text = UserData.Instance.username + "'s vibe";
        UpdateMap();
    }

    void UpdateMap()
    {
        var prefs = UserData.Instance.savedPreferences;

        if (prefs.Count == 0)
        {
            noteText.text = "No preferences selected yet.";
            return;
        }

        ClearImage(cameraPlaceholder);
        ClearImage(musicPlaceholder);
        ClearImage(tvPlaceholder);

        PreferenceData latestMusic = null;
        PreferenceData latestMovie = null;
        PreferenceData latestLifestyle = null;

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

        if (latestMusic != null)
        {
            SetImage(musicPlaceholder, latestMusic.image);
            StartCoroutine(DropFade(musicPlaceholder));
        }

        if (latestMovie != null)
        {
            SetImage(tvPlaceholder, latestMovie.image);
            StartCoroutine(DropFade(tvPlaceholder));
        }

        if (latestLifestyle != null)
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

        if (result == "")
            result = "Your map is still growing...";

        noteText.text = "";
        StartCoroutine(TypeText(result.Trim()));
    }

    void SetImage(Image target, Sprite sprite)
    {
        target.sprite = sprite;
        target.color = Color.white;
    }

    void ClearImage(Image target)
    {
        target.sprite = null;
        target.color = new Color(1f, 1f, 1f, 0f);
    }

    IEnumerator DropFade(Image img)
    {
        Color c = img.color;
        c.a = 0f;
        img.color = c;

        float duration = 0.22f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float p = time / duration;
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