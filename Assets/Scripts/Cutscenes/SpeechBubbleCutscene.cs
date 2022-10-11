using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;

public class SpeechBubbleCutscene : MonoBehaviour
{
    [SerializeField] private Image CharacterSpeechImg;
    [SerializeField] private TextMeshProUGUI CharacterNameText;
    [SerializeField] private TextMeshProUGUI CharacterSpeechText;
    [SerializeField] private int[] CharacterID;
    [SerializeField] private string[] SpeechStrings;
    [SerializeField] private AudioSource AS;
    private CharacterBlueprint CurrentCharacter;

    private int CurrentPos = 0;
    public bool IsTyping;
    public bool SpeechIsComplete;

    private readonly Keyboard keyboard = Keyboard.current;

    public void Setup()
    {
        //Preload the character so he is there when fading
        CurrentCharacter = CharacterStorage.GetCharacter(CharacterID[CurrentPos]);
        CharacterSpeechImg.sprite = CurrentCharacter.selfSprite;
    }

    public void DoNextSpeech()
    {
        if (CurrentPos < SpeechStrings.Length)
        {
            //Assign new character
            CurrentCharacter = CharacterStorage.GetCharacter(CharacterID[CurrentPos]);
            CharacterSpeechImg.sprite = CurrentCharacter.selfSprite;

            CharacterNameText.text = CurrentCharacter.Name;
            CharacterSpeechText.text = "";

            Invoke(nameof(StartTyping), 0.5f);
        }
        else
        {
            //Cutscene is over, reset it
            CharacterNameText.text = "";
            CharacterSpeechText.text = "";
            SpeechIsComplete = true;
        }
    }

    private void StartTyping()
    {
        IsTyping = true;
        StartCoroutine(nameof(Type), 0.05f);
    }

    private IEnumerator Type(float TimePerLetter)
    {
        //Reset text and get length of string for the for loop
        string CurrentText = "";
        int MaxLetterPos = SpeechStrings[CurrentPos].Length;
        CharacterSpeechText.text = "";

        //Get random pitch to create the illusion he is talking
        float PitchAvg = CurrentCharacter.Speech_Pitch;
        AS.pitch = PitchAvg;
        AS.Play();

        //Loop over each letter, change the pitch, add letter to the text and delay the loop to gradually reveal the sentence
        for (int i = 0; i < MaxLetterPos; i++)
        {
            CurrentText += SpeechStrings[CurrentPos][i];
            CharacterSpeechText.text = CurrentText;
            AS.pitch = Mathf.Clamp(Random.Range(PitchAvg - 0.2f, PitchAvg + 0.2f), -3, 3);
            yield return (new WaitForSeconds(TimePerLetter));
        }

        //Prepare for next loop
        FinallizeSpeech();
    }

    private void FinallizeSpeech()
    {
        CharacterSpeechText.text = SpeechStrings[CurrentPos++];
        WaitForInputThenDoNextSpeech().Forget();
        IsTyping = false;
        AS.Pause();
    }

    private async UniTaskVoid WaitForInputThenDoNextSpeech()
    {
        do
            await UniTask.Yield();
        while (!keyboard.anyKey.wasPressedThisFrame);

        DoNextSpeech();
    }

    private void Update()
    {
        if (IsTyping && keyboard.anyKey.wasPressedThisFrame)
        {
            StopCoroutine(nameof(Type));
            FinallizeSpeech();
        }
    }
}
