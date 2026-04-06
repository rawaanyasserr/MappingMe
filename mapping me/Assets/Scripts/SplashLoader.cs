using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashLoader : MonoBehaviour
{
    public float delay = 2.5f;

    void Start()
    {
        Invoke(nameof(GoToLogin), delay);
    }

    void GoToLogin()
    {
        SceneManager.LoadScene("01_Login");
    }
}