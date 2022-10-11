using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCutsceneInteraction : MonoBehaviour
{
    [SerializeReference] public RandomSong[] AllISongs;
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
        SearchingForIntro = false;
        DoingIntro = true;
    }

    public void CompleteALevel(int ReactionQuality)
    {
        CutsceneP.PlayReactionCutscene(CurrentLevel, ReactionQuality);
        CurrentLevel++; 
        SearchingForIntro = true;
    }

    private void Update()
    {
        if(SearchingForIntro && !CutsceneP.IsPlaying)
        {
            DoIntroCutscene();
        }

        if (DoingIntro && !CutsceneP.IsPlaying)
        {
            DoingIntro = false;
            SC.StartSong(AllISongs[CurrentLevel]);
        }
    }
}
