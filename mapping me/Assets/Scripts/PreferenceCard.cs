using UnityEngine;
using UnityEngine.UI;

public class PreferenceCard : MonoBehaviour
{
    public string preferenceTitle;
    public string category;
    public Image sourceImage;

    public void OnCardClicked()
    {
        if (PreferencePopupManager.Instance == null)
        {
            Debug.LogError("PreferencePopupManager not found.");
            return;
        }

        if (sourceImage == null || sourceImage.sprite == null)
        {
            Debug.LogError("Source image missing on " + gameObject.name);
            return;
        }

        PreferencePopupManager.Instance.OpenPopup(preferenceTitle, category, sourceImage.sprite);
    }
}