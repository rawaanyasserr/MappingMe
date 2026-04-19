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

    

    public void Logout()
    {
        FirebaseAuthManager.Instance.Logout();
        SceneManager.LoadScene("01_Login");
    }
}