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
        string firstName = firstNameInput.text.Trim();
        string lastName = lastNameInput.text.Trim();
        string email = emailInput.text.Trim();
        string password = passwordInput.text;
        string confirmPassword = confirmPasswordInput.text;

        string message = ValidateInput(firstName, lastName, email, password, confirmPassword);

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

        FirebaseAuthManager.Instance.SignUp(email, password, (success, resultMessage) =>
        {
            SetStatus(resultMessage);

            if (success)
            {
                if (UserData.Instance != null)
                    UserData.Instance.SetUsername(firstName);

                SceneManager.LoadScene("Home");
            }
        });
    }

    string ValidateInput(string firstName, string lastName, string email, string password, string confirmPassword)
    {
        if (firstName == "") return "Please enter your first name.";
        if (lastName == "") return "Please enter your last name.";
        if (email == "") return "Please enter your email.";
        if (password == "") return "Please enter your password.";
        if (password != confirmPassword) return "Passwords do not match.";

        return "";
    }

    void SetStatus(string message)
    {
        if (statusText != null)
            statusText.text = message;

        Debug.Log(message);
    }
}