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
        if (popupPanel != null)
            popupPanel.SetActive(true);

        if (popupImage != null)
            popupImage.sprite = image;

        if (titleText != null)
            titleText.text = title;
    }

    public void ClosePopup()
    {
        if (popupPanel != null)
            popupPanel.SetActive(false);
    }
}