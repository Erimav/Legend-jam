using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SongController : MonoBehaviour
{
    private NoteLine[] lines;
    private bool foregroundMusicOn;

    public float musicFadeDuration = .5f;

    public SongInputs input;
    public InputAction[] NoteInputs { get; private set; }

    public AudioSource backgroundMusicSource;
    public AudioSource foregroundMusicSource;



    public event Action<NoteHitResult> Hit;
    

    void Start()
    {
        input = new SongInputs();
        NoteInputs = new[] { input.Notes.HitLine1, input.Notes.HitLine2, input.Notes.HitLine3 };
        lines = GetComponentsInChildren<NoteLine>();
        foreach (var line in lines)
            line.Hit += OnNoteHit;
    }

    public void StartSong(ISong song)
    {
        backgroundMusicSource.clip = song.Track;
        foregroundMusicSource.clip = song.ForegroundTrack;
        backgroundMusicSource.Play();
        foregroundMusicSource.Play();
        foregroundMusicSource.volume = 1;
        foregroundMusicOn = true;

        var lines = this.lines.Take(song.NumberOfLines).ToArray();
        song.SpawnNotes(lines);

        input.Enable();
        for (int i = 0; i < lines.Length; i++)
        {
            NoteLine line = lines[i];
            line.SetInput(NoteInputs[i]);
            line.IsPlaying = true;
        }
    }

    public void EndSong()
    {
        foreach (var line in lines)
        {
            line.IsPlaying = false;
        }
        input.Disable();
    }

    public async UniTask PlaySongAsync(ISong song)
    {
        StartSong(song);
        //await UniTask.Delay(TimeSpan.FromSeconds(song.Track.length));
        await UniTask.Delay(TimeSpan.FromSeconds(30));
        EndSong();
    }

    private void OnNoteHit(NoteHitResult hit)
    {
        Hit?.Invoke(hit);
        var shouldPlayForeground = hit != NoteHitResult.Miss;
        if (foregroundMusicOn != shouldPlayForeground)
            TurnForeground(shouldPlayForeground);
    }

    private void TurnForeground(bool on)
    {
        foregroundMusicSource.DOKill();
        foregroundMusicSource.DOFade(on ? 1 : 0, musicFadeDuration).Play();
        foregroundMusicOn = on;
    }
}
