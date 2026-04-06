using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    public void LoadLogin() => SceneManager.LoadScene("01_Login");
    public void LoadSignUp() => SceneManager.LoadScene("02_SignUp");
    public void LoadHome() => SceneManager.LoadScene("03_Home");
    public void LoadAddPreference() => SceneManager.LoadScene("04_AddPreference");
    public void LoadMap() => SceneManager.LoadScene("05_Map");
    public void LoadTimeline() => SceneManager.LoadScene("06_Timeline");
    public void LoadProfile() => SceneManager.LoadScene("07_Profile");
}