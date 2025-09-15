using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject MenuUI;
    [SerializeField] private GameObject CreditsUI;
    [SerializeField] private SceneFader fader;
    [SerializeField] private Image img;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private GameObject CreditsBackButton;
    [SerializeField] private GameObject SettingsBackButton;

    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGameScene");
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
