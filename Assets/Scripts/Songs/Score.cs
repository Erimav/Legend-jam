using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public SongController songController;
    public TMP_Text display;

    public int scoreForPurrfect;
    public int scoreForImperfect;
    public int scoreForMiss;

    public int score;

    private void Start()
    {
        songController.Hit += AddScore;
    }

    private void AddScore(NoteHitResult hit)
    {
        score += hit switch
        {
            NoteHitResult.Miss => scoreForMiss,
            NoteHitResult.Imperfect => scoreForImperfect,
            NoteHitResult.Purrfect => scoreForPurrfect,
            _ => throw new NotImplementedException(),
        };

        display.text = $"Score: {score}";
    }
}
