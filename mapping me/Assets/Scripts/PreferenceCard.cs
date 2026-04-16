using UnityEngine;
using UnityEngine.UI;

public class PreferenceCard : MonoBehaviour
{
    public string preferenceTitle;
    public string category;
    public string keywords;
    public Image sourceImage;
    public string imageName;

    public void OnCardClicked()
    {
        if (sourceImage == null || sourceImage.sprite == null)
            return;

        PreferencePopupManager.Instance.OpenPopup(
            preferenceTitle,
            category,
            sourceImage.sprite,
            imageName
        );
    }
}