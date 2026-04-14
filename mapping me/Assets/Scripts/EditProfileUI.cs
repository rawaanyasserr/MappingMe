using UnityEngine;
using TMPro;

public class EditProfileUI : MonoBehaviour
{
    public TMP_InputField nameInput;

    void Start()
    {
        nameInput.text = UserData.Instance.username;
    }

    public void SaveProfile()
    {
        UserData.Instance.SetUsername(nameInput.text);
    }
}