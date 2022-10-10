using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableImgs : MonoBehaviour
{
    [SerializeField] private FadeImg[] AllSpriteManagers;
    [SerializeField] private bool[] State;
    private bool HasActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        DoLogic();
    }

    private void OnEnable()
    {
        DoLogic();
    }

    private void DoLogic()
    {
        if (!HasActivated)
        {
            for (int i = 0; i < AllSpriteManagers.Length; i++)
            {
                AllSpriteManagers[i].Setup();
                if (State[i])
                {
                    AllSpriteManagers[i].FadeIn();
                }
                else
                {
                    AllSpriteManagers[i].FadeOut();
                }
            }
            Destroy(this);
        }
    }
}
