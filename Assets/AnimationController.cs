using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Sprite[] SpriteSheetIdle;
    [SerializeField] Sprite[] SpriteSheetAction;
    [SerializeField] Sprite[] SpriteSheetAction_2;
    [SerializeField] float AnimUpdateRate = 1;
    public int ActionType = 2;
    private int CurrentFrame = 0;
    private SpriteRenderer SR;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.TryGetComponent(out SR);
        InvokeRepeating(nameof(UpdateSprite), AnimUpdateRate, AnimUpdateRate);
    }

    private void UpdateSprite()
    {
        if (ActionType == 0)
        {
            SetSprite(SpriteSheetAction);
        }
        else if (ActionType == 1)
        {
            SetSprite(SpriteSheetAction_2);
        }
        else
        {
            SetSprite(SpriteSheetIdle);
        }
    }

    private void SetSprite(Sprite[] CurrentSpriteSheet)
    {
        CurrentFrame++;
        if(CurrentFrame == CurrentSpriteSheet.Length)
        {
            CurrentFrame = 0;
        }

        SR.sprite = CurrentSpriteSheet[CurrentFrame];
    }
}
