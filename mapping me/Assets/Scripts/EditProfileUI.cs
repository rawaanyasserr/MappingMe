using UnityEngine;
using TMPro;

public class EditProfileUI : MonoBehaviour
{
    public TMP_InputField nameInput;

    void Start()
    {
        if (UserData.Instance != null && nameInput != null)
        {
            nameInput.text = UserData.Instance.username;
        }
    }

    public void SaveProfile()
    {
        if (UserData.Instance != null && nameInput != null)
        {
            UserData.Instance.SetUsername(nameInput.text);
            Debug.Log("New name: " + UserData.Instance.username);
        }
    }
}