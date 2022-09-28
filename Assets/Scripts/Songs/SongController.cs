using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SongController : MonoBehaviour
{
    public SongInputs input;
    public InputAction[] NoteInputs { get; private set; }

    public AudioSource musicSource;

    private NoteLine[] lines;

    public event Action<NoteHitResult> Hit;
    

    void Start()
    {
        input = new SongInputs();
        NoteInputs = new[] { input.Notes.HitLine1, input.Notes.HitLine2, input.Notes.HitLine3 };
        lines = GetComponentsInChildren<NoteLine>();
        foreach (var line in lines)
            line.Hit += hit => Hit?.Invoke(hit);
    }

    public void StartSong(ISong song)
    {
        musicSource.clip = song.Track;
        musicSource.Play();

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
}
