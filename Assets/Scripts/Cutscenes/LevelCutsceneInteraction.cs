using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCutsceneInteraction : MonoBehaviour
{
    [SerializeField] private CutscenePlayer CutsceneP;
    [SerializeField] private GameObject[] ChangeStateOnLvl1;
    [SerializeField] private GameObject[] ChangeStateOnLvl2;
    [SerializeField] private GameObject[] ChangeStateOnLvl3;
    private GameObject[][] AllChangeState;
    private int CurrentLevel = 0;
    private bool SearchingForIntro = false;

    private void Start()
    {
        AllChangeState = new GameObject[3][] { ChangeStateOnLvl1, ChangeStateOnLvl2, ChangeStateOnLvl3 };    
        DoIntroCutscene();
    }

    private void DoIntroCutscene()
    {
        for(int i = 0; i < AllChangeState[CurrentLevel].Length; i++)
        {
            print(AllChangeState[CurrentLevel][i].name);
            AllChangeState[CurrentLevel][i].SetActive(!AllChangeState[CurrentLevel][i].activeSelf);
        }
        CutsceneP.PlayCutscene(CurrentLevel);
        SearchingForIntro = false;
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

        //Dev
        //if(Input.GetKeyUp(KeyCode.K))
        //{
        //    CompleteALevel(Random.Range(0, 3));
        //}
    }
}
