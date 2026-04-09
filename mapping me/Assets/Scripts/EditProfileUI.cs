using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EditProfileUI : MonoBehaviour
{
    public TMP_InputField nameInput;

    void Start()
    {
        if (UserData.Instance != null)
        {
            nameInput.text = UserData.Instance.username;
        }
    }

    public void SaveProfile()
    {
        if (UserData.Instance != null)
        {
            UserData.Instance.SetUsername(nameInput.text);
        }

        SceneManager.LoadScene("07_Profile");
    }

    public void GoBack()
    {
        SceneManager.LoadScene("07_Profile");
    }
}