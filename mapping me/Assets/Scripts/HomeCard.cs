using UnityEngine;
using UnityEngine.UI;

public class HomeCard : MonoBehaviour
{
    public Image cardImage;
    public string title;

    public void OnCardClicked()
    {
        HomePopupManager popup = FindObjectOfType<HomePopupManager>();

        if (popup != null)
        {
            popup.OpenPopup(cardImage.sprite, title);
        }
        else
        {
            Debug.LogError("HomePopupManager not found.");
        }
    }
}