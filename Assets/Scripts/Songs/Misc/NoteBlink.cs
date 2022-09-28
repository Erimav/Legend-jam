using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Note))]
public class NoteBlink : MonoBehaviour
{
    public float blinkTime = 0.2f;

    private void Start()
    {
        GetComponent<Note>().Hit += _ =>
        {
            var text = GetComponentInChildren<TMP_Text>();
            DOTween.To(() => text.color, c => text.color = c, Color.white, blinkTime).SetLoops(1, LoopType.Yoyo).Play();
        };
    }
}
