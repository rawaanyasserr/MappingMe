using UnityEngine;
using TMPro;

public class AddPreferenceSearch : MonoBehaviour
{
    public TMP_InputField searchInput;
    public Transform cardsParent;
    public GameObject noResultsText; // 👈 NEW

    void Start()
    {
        if (searchInput != null)
            searchInput.onValueChanged.AddListener(FilterCards);
    }

    public void FilterCards(string query)
    {
        query = query.Trim().ToLower();

        int visibleCount = 0; // 👈 track matches

        foreach (Transform child in cardsParent)
        {
            PreferenceCard card = child.GetComponent<PreferenceCard>();

            if (card != null)
            {
                string title = card.preferenceTitle.ToLower();
                string category = card.category.ToLower();

                bool matches = string.IsNullOrEmpty(query) ||
                               title.Contains(query) ||
                               category.Contains(query);

                child.gameObject.SetActive(matches);

                if (matches)
                    visibleCount++; // 👈 count matches
            }
        }

        // 👇 show/hide "no results"
        if (noResultsText != null)
        {
            bool show = visibleCount == 0 && !string.IsNullOrEmpty(query);
            noResultsText.SetActive(show);
        }
    }
}