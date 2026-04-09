using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ProfileUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text subtitleText;

    void Start()
    {
        if (UserData.Instance != null)
        {
            nameText.text = UserData.Instance.username;
        }

        if (subtitleText != null)
        {
            subtitleText.text = "Soft clean aesthetic";
        }
    }

    public void OpenEditProfile()
    {
        SceneManager.LoadScene("07_Profile");
    }

    public void Logout()
    {
        if (UserData.Instance != null)
        {
            UserData.Instance.username = "Rue";
            UserData.Instance.ClearPreferences();
        }

        SceneManager.LoadScene("01_Login");
    }
}