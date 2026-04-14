using System.Collections;
using UnityEngine;
using TMPro;

public class SplashTypewriter : MonoBehaviour
{
    public TMP_Text titleText;
    public string fullText = "Mapping Me";
    public float letterDelay = 0.035f;

    void Start()
    {
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        titleText.text = "";

        foreach (char c in fullText)
        {
            titleText.text += c;
            yield return new WaitForSecondsRealtime(letterDelay);
        }
    }
}