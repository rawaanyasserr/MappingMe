using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SignUpUI : MonoBehaviour
{
    public TMP_InputField firstNameInput;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;
    public TMP_Text statusText;

    public void OnSignUpClicked()
    {
        string firstName = firstNameInput.text.Trim();
        string email = emailInput.text.Trim();
        string password = passwordInput.text;
        string confirm = confirmPasswordInput.text;

        if (firstName == "")
        {
            SetStatus("Enter your name.");
            return;
        }

        if (email == "")
        {
            SetStatus("Enter your email.");
            return;
        }

        if (password == "")
        {
            SetStatus("Enter your password.");
            return;
        }

        if (password != confirm)
        {
            SetStatus("Passwords do not match.");
            return;
        }

        FirebaseAuthManager.Instance.SignUp(email, password, (success, message) =>
        {
            SetStatus(message);

            if (success)
            {
                UserData.Instance.savedPreferences.Clear();
                UserData.Instance.timelineEntries.Clear();
                UserData.Instance.SetUsername(firstName);

                FirebaseDataManager.Instance.SaveAll();

                
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