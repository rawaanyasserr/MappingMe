using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomePopupManager : MonoBehaviour
{
    public static HomePopupManager Instance;

    public GameObject popupPanel;
    public Image popupImage;
    public TMP_Text titleText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        popupPanel.SetActive(false);
    }

    public void OpenPopup(Sprite image, string title)
    {
        popupImage.sprite = image;
        titleText.text = title;
        popupPanel.SetActive(true);
    }
 
    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
}