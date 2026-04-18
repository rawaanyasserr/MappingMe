using UnityEngine;
using TMPro;

public class AddPreferenceSearch : MonoBehaviour
{
    public TMP_InputField searchInput;
    public Transform cardsParent;
    public GameObject noResultsText;

    void Start()
    {
        searchInput.onValueChanged.AddListener(FilterCards);
    }

    void FilterCards(string query)
    {
        query = query.ToLower().Trim();
        int matchesCount = 0;

        foreach (Transform child in cardsParent)
        {
            PreferenceCard card = child.GetComponent<PreferenceCard>();
            if (card == null) continue;

            bool matches =
                query == "" ||
                card.preferenceTitle.ToLower().Contains(query) ||
                card.category.ToLower().Contains(query);

            child.gameObject.SetActive(matches);

            if (matches)
                matchesCount++;
        }

        noResultsText.SetActive(matchesCount == 0 && query != "");
    }
}