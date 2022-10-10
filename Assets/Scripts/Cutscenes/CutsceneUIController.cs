using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneUIController : MonoBehaviour
{
    public bool CutsceneIsComplete;
    [SerializeField] private FadeImg FadeImgController;
    public SpeechBubbleCutscene SpeechBubbleController;

    // Start is called before the first frame update
    void Start()
    {
        StartCutscene(); //Dev
    }

    public void StartCutscene()
    {
        FadeImgController.Setup();
        FadeImgController.FadeIn();
        SpeechBubbleController.Setup();
        InvokeRepeating(nameof(CheckForSpeech), 1.5f, 0.1f);
    }

    private void CheckForSpeech()
    {
        if (!FadeImgController.IsFading)
        {
            SpeechBubbleController.DoNextSpeech();
            CancelInvoke();
            InvokeRepeating(nameof(CheckForEnd), 1f, 0.1f);
        }
    }

    private void CheckForEnd()
    {
        if (SpeechBubbleController.SpeechIsComplete)
        {
            FadeImgController.FadeOut();
            CancelInvoke();
            CutsceneIsComplete = true;
        }
    }
}
