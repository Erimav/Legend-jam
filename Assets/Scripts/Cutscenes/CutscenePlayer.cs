using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutscenePlayer : MonoBehaviour
{
    public bool IsPlaying;
    public bool CutsceneStarted;

    [SerializeField] private GameObject[] AllCutscenes;
    [SerializeField] private GameObject[] GoodReactions;
    [SerializeField] private GameObject[] NeutralReactions;
    [SerializeField] private GameObject[] BadReactions;
    [SerializeField] private GameObject[][] AllReactions;

    private SpeechBubbleCutscene CurrentCutscene;

    private void Start()
    {
        AllReactions = new GameObject[3][] { GoodReactions, NeutralReactions, BadReactions };
    }

    public void PlayCutscene(int CutscenePosition)
    {
        GameObject NewCutscene = GameObject.Instantiate(AllCutscenes[CutscenePosition]);
        CurrentCutscene = NewCutscene.GetComponent<CutsceneUIController>().SpeechBubbleController;
        IsPlaying = true;
        CutsceneStarted = true;
    }

    public void PlayReactionCutscene(int CutscenePosition, int Quality)
    {
        GameObject NewCutscene = GameObject.Instantiate(AllReactions[Quality][CutscenePosition]);
        CurrentCutscene = NewCutscene.GetComponent<CutsceneUIController>().SpeechBubbleController;
        IsPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying && CurrentCutscene != null && CurrentCutscene.SpeechIsComplete)
        {
            IsPlaying = false;
        }
    }
}
