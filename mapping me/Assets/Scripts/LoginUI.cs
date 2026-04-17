using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text statusText;

    public void OnLoginClicked()
    {
        string email = emailInput.text.Trim();
        string password = passwordInput.text;

        if (email == "")
        {
            SetStatus("Please enter your email.");
            return;
        }

        if (password == "")
        {
            SetStatus("Please enter your password.");
            return;
        }

        FirebaseAuthManager.Instance.Login(email, password, (success, message) =>
        {
            SetStatus(message);

            if (success)
            {
                SceneManager.LoadScene("03_Home");
            }
        });
    }

    void SetStatus(string msg)
    {
        if (statusText != null)
            statusText.text = msg;
    }
}