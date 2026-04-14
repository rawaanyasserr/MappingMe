using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreferencePopupManager : MonoBehaviour
{
    public static PreferencePopupManager Instance;

    public GameObject popupPanel;
    public Image popupImage;
    public TMP_Text titleText;
    public TMP_Text categoryText;

    string currentTitle;
    string currentCategory;
    Sprite currentSprite;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        popupPanel.SetActive(false);
    }

    public void OpenPopup(string title, string category, Sprite image)
    {
        currentTitle = title;
        currentCategory = category;
        currentSprite = image;

        popupImage.sprite = image;
        titleText.text = title;
        categoryText.text = category;
        popupPanel.SetActive(true);
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }

    public void SaveCurrentPreference()
    {
        UserData.Instance.AddPreference(currentTitle, currentCategory, currentSprite);
        popupPanel.SetActive(false);
    }
}