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

        string message = ValidateInput(email, password);

        if (message != "")
        {
            SetStatus(message);
            return;
        }

        if (FirebaseAuthManager.Instance == null)
        {
            SetStatus("Please start from the splash screen.");
            return;
        }

        FirebaseAuthManager.Instance.Login(email, password, (success, resultMessage) =>
        {
            SetStatus(resultMessage);

            if (success)
            {
                if (UserData.Instance != null)
                {
                    string nameFromEmail = email.Split('@')[0];
                    UserData.Instance.SetUsername(nameFromEmail);
                }

                SceneManager.LoadScene("03_Home");
            }
        });
    }

    string ValidateInput(string email, string password)
    {
        if (email == "") return "Please enter your email.";
        if (password == "") return "Please enter your password.";
        return "";
    }

    void SetStatus(string message)
    {
        if (statusText != null)
            statusText.text = message;

        Debug.Log(message);
    }
}