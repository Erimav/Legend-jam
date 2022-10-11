using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCutsceneInteraction : MonoBehaviour
{
    [SerializeReference] public RandomSong[] AllISongs;
    [SerializeField] private FadeImg EndFadeToBlack;
    [SerializeField] private AnimationController CatAC;
    [SerializeField] private AnimationController[] AllJudgesAC;
    [SerializeField] private SongController SC;
    [SerializeField] private CutscenePlayer CutsceneP;
    [SerializeField] private GameObject[] ChangeStateOnLvl1;
    [SerializeField] private GameObject[] ChangeStateOnLvl2;
    [SerializeField] private GameObject[] ChangeStateOnLvl3;
    private GameObject[][] AllChangeState;
    private int CurrentLevel = 0;
    private bool SearchingForIntro = false;
    private bool DoingIntro;

    private void Start()
    {
        AllChangeState = new GameObject[3][] { ChangeStateOnLvl1, ChangeStateOnLvl2, ChangeStateOnLvl3 };   
        DoIntroCutscene();
    }

    private void DoIntroCutscene()
    {
        for(int i = 0; i < AllChangeState[CurrentLevel].Length; i++)
        {
            AllChangeState[CurrentLevel][i].SetActive(!AllChangeState[CurrentLevel][i].activeSelf);
        }
        CutsceneP.PlayCutscene(CurrentLevel);
        CatAC.ActionType = 2;
        SearchingForIntro = false;
        DoingIntro = true;
    }

    public void CompleteALevel(int ReactionQuality)
    {
        foreach(AnimationController AC in AllJudgesAC) { AC.ActionType = 2; }
        CutsceneP.PlayReactionCutscene(CurrentLevel, ReactionQuality);
        CatAC.ActionType = 2;
        CurrentLevel++;
        SearchingForIntro = true;
    }

    private void Update()
    {
        if(SearchingForIntro && !CutsceneP.IsPlaying)
        {
            if (CurrentLevel > 2)
            {
                //EndGame
                CatAC.ActionType = 1;
                EndFadeToBlack.FadeIn();
                Invoke(nameof(EndGame), 7);
            }
            else
            {
                DoIntroCutscene();
            }
        }

        if (DoingIntro && !CutsceneP.IsPlaying)
        {
            DoingIntro = false;
            SC.PlaySongAsync(AllISongs[CurrentLevel]);
            foreach (AnimationController AC in AllJudgesAC) { AC.ActionType = 0; }
            CatAC.ActionType = 0;
        }
    }

    private void EndGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
