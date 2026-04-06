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

    private string currentTitle;
    private string currentCategory;
    private Sprite currentSprite;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (popupPanel != null)
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
        if (UserData.Instance == null)
        {
            Debug.LogError("UserData.Instance is NULL.");
            return;
        }

        if (currentSprite == null)
        {
            Debug.LogError("No selected preference to save.");
            return;
        }

        UserData.Instance.AddPreference(currentTitle, currentCategory, currentSprite);
        Debug.Log("Saved preference from popup: " + currentTitle);

        popupPanel.SetActive(false);
    }
}