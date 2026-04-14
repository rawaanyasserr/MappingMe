using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ProfileUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text subtitleText;

    void Start()
    {
        nameText.text = UserData.Instance.username;
        subtitleText.text = "Soft clean aesthetic";
    }

    public void OpenEditProfile()
    {
        SceneManager.LoadScene("07_Profile");
    }

    public void Logout()
    {
        UserData.Instance.username = "Rue";
        UserData.Instance.ClearPreferences();

        SceneManager.LoadScene("01_Login");
    }
}