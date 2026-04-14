using UnityEngine;
using UnityEngine.UI;

public class HomeCard : MonoBehaviour
{
    public Image cardImage;
    public string title;

    public void OnCardClicked()
    {
        HomePopupManager.Instance.OpenPopup(cardImage.sprite, title);
    }
}