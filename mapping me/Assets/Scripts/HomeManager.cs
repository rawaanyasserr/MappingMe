using UnityEngine;
using TMPro;

public class HomeManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform contentParent;
    public TMP_Text emptyStateText;

    void Start()
    {
        LoadSavedPreferences();
    }

    void LoadSavedPreferences()
    {
        if (UserData.Instance == null)
        {
            Debug.LogError("UserData.Instance is NULL.");
            return;
        }

        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        var prefs = UserData.Instance.savedPreferences;

        if (prefs == null || prefs.Count == 0)
        {
            if (emptyStateText != null)
                emptyStateText.gameObject.SetActive(true);

            return;
        }

        if (emptyStateText != null)
            emptyStateText.gameObject.SetActive(false);

        foreach (PreferenceData pref in prefs)
        {
            GameObject newCard = Instantiate(cardPrefab, contentParent);

            HomeCard homeCard = newCard.GetComponent<HomeCard>();
            if (homeCard != null)
            {
                homeCard.title = pref.title;

                if (homeCard.cardImage != null)
                    homeCard.cardImage.sprite = pref.image;
            }

            TMP_Text titleText = newCard.GetComponentInChildren<TMP_Text>();
            if (titleText != null)
                titleText.text = pref.title;
        }
    }
}