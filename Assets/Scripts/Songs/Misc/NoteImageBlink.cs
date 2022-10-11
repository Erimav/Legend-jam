using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteImageBlink : MonoBehaviour
{
    public SpriteRenderer image;

    public Color hitColor = Color.white;
    public Color missColor = Color.red;

    public float blinkTime = 0.2f;

    private void Start()
    {
        GetComponent<Note>().Hit += res =>
        {
            var color = res != NoteHitResult.Miss ? hitColor : missColor;

            image.DOColor(color, blinkTime).SetLoops(2, LoopType.Yoyo).Play();
        };
    }
}
