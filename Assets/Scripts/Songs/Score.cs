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

    public float neutralScoreLimit = 0.5f;
    public float purrfectScoreLimit = 0.75f;

    public int score;
    public int maxPossibleScore;

    public float ScorePercentage => (float)score / maxPossibleScore;

    private void Start()
    {
        songController.Hit += AddScore;
        songController.SongFinished += OnSongEnded;
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

        maxPossibleScore += scoreForPurrfect;

        if (display != null)
            display.text = $"Score: {score}";
    }

    private void OnSongEnded()
    {
        var ending = ScorePercentage >= purrfectScoreLimit
            ? 0
            : ScorePercentage >= neutralScoreLimit ? 1 : 2;

        FindObjectOfType<LevelCutsceneInteraction>()?.CompleteALevel(ending);
    }
}
