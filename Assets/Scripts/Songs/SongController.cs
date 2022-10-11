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

    public GameObject content;

    public event Action<NoteHitResult> Hit;
    public event Action SongFinished;

    void Start()
    {
        input = new SongInputs();
        NoteInputs = new[] { input.Notes.HitLine1, input.Notes.HitLine2, input.Notes.HitLine3 };
        lines = GetComponentsInChildren<NoteLine>();
        foreach (var line in lines)
        {
            line.gameObject.SetActive(false);
            line.Hit += OnNoteHit;
        }

        if (content == null)
            content = transform.Find("Content").gameObject;
    }

    private void StartSong(ISong song)
    {
        content.SetActive(true);

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
            line.gameObject.SetActive(true);
            line.SetInput(NoteInputs[i]);
            line.IsPlaying = true;
        }
    }

    private void EndSong()
    {
        foreach (var line in lines)
        {
            if (line.IsPlaying)
            {
                line.IsPlaying = false;
                line.gameObject.SetActive(false);
            }
        }

        input.Disable();
        SongFinished?.Invoke();
    }

    public async UniTask PlaySongAsync(ISong song)
    {
        StartSong(song);
        await UniTask.Delay(TimeSpan.FromSeconds(song.Track.length));
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
