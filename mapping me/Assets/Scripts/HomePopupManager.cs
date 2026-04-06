using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomePopupManager : MonoBehaviour
{
    public GameObject popupPanel;
    public Image popupImage;
    public TMP_Text titleText;

    public void OpenPopup(Sprite image, string title)
    {
        popupPanel.SetActive(true);
        popupImage.sprite = image;
        titleText.text = title;
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
}