using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject MenuUI;
    public GameObject CreditsUI;
    public SceneFader fader;
    public Image img;
    public AnimationCurve curve;
    [SerializeField] private GameObject CreditsBackButton;
    [SerializeField] private GameObject SettingsBackButton;

    public void Play()
    {
        //fader.FadeTo(LevelToLoad);
    }

    public void Credits()
    {
        MenuUI.SetActive(false);
        CreditsUI.SetActive(true);
    }

    public void QuitGame()
    {
        //StartCoroutine(FadeOut());
        Debug.Log("Quit");
        Application.Quit();
    }
    
    /*private IEnumerator FadeOut()
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }*/

    public void CreditsBack()
    {
        CreditsBackButton.transform.parent.gameObject.SetActive(false);

        MenuUI.SetActive(true);
    }
    public void SettingsBack()
    {
        SettingsBackButton.transform.parent.gameObject.SetActive(false);

        MenuUI.SetActive(true);
    }
}
