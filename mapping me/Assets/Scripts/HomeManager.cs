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
        
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        var prefs = UserData.Instance.savedPreferences;

        
        if (prefs.Count == 0)
        {
            emptyStateText.gameObject.SetActive(true);
            return;
        }

        emptyStateText.gameObject.SetActive(false);

        
        foreach (var pref in prefs)
        {
            GameObject newCard = Instantiate(cardPrefab, contentParent);

            HomeCard card = newCard.GetComponent<HomeCard>();
            card.title = pref.title;
            card.cardImage.sprite = pref.image;

            newCard.GetComponentInChildren<TMP_Text>().text = pref.title;
        }
    }
}