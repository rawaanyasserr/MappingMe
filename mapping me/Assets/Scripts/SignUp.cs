using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SignUpUI : MonoBehaviour
{
    public TMP_InputField firstNameInput;
    public TMP_InputField lastNameInput;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;
    public TMP_Text statusText;

    public void OnSignUpClicked()
    {
        Debug.Log("SIGNUP CLICKED");
        string firstName = firstNameInput.text.Trim();
        string lastName = lastNameInput.text.Trim();
        string email = emailInput.text.Trim();
        string password = passwordInput.text;
        string confirmPassword = confirmPasswordInput.text;

        if (string.IsNullOrEmpty(firstName))
        {
            SetStatus("Please enter your first name.");
            return;
        }

        if (string.IsNullOrEmpty(lastName))
        {
            SetStatus("Please enter your last name.");
            return;
        }

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

        if (password != confirmPassword)
        {
            SetStatus("Passwords do not match.");
            return;
        }

        if (UserData.Instance != null)
        {
            UserData.Instance.SetUsername(firstName);
        }

        FirebaseAuthManager.Instance.SignUp(email, password, (success, message) =>
        {
            SetStatus(message);

            if (success)
            {
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