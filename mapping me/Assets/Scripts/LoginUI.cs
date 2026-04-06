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
        Debug.Log("LOGIN CLICKED");

        string email = emailInput.text.Trim();
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email))
        {
            SetStatus("Please enter your email.");
            return;
        }

        if (string.IsNullOrEmpty(password))
        {
            SetStatus("Please enter your password.");
            return;
        }

        if (FirebaseAuthManager.Instance == null)
        {
            SetStatus("Firebase manager not found. Start from Splash scene.");
            Debug.LogError("FirebaseAuthManager.Instance is NULL");
            return;
        }

        FirebaseAuthManager.Instance.Login(email, password, (success, message) =>
        {
            SetStatus(message);

            if (success)
            {
                if (UserData.Instance != null)
                {
                    string emailName = email.Split('@')[0];
                    UserData.Instance.SetUsername(emailName);
                }

                SceneManager.LoadScene("03_Home");
            }
        });
    }

    void SetStatus(string message)
    {
        if (statusText != null)
            statusText.text = message;

        Debug.Log(message);
    }
}