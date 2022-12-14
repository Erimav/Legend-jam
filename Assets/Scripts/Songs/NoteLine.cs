using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteLine : MonoBehaviour
{
    private InputAction input;
    private bool isPlaying;

    public Note noteInstance;
    public List<Note> notes;
    public TimingSettingsSO timings;
    public NoteSoundsSetSO noteSounds;
    public event Action<NoteHitResult> Hit;

    public AudioSource noteAudioSource;

    public float speed;
    public bool playMissSoundOnIgnoreNote;

    public bool IsPlaying
    {
        get => isPlaying;
        set
        {
            isPlaying = value;

            if (value)
                input.Enable();
            else
                input.Disable();
        }
    }

    public void SetNoteInstance(Note noteInstance)
    {
        this.noteInstance = noteInstance;
    }

    public void CreateNote(float time)
    {
        var note = Instantiate(noteInstance, transform.TransformPoint(speed * time * Vector3.right) + Vector3.back, Quaternion.identity, transform);
        notes.Add(note);
    }

    public void SetInput(InputAction input)
    {
        if (this.input != null)
            input.started -= OnInput;

        this.input = input;
        input.started += OnInput;
    }

    public void OnInput(InputAction.CallbackContext ctx)
    {
        var firstNoteInRange = notes
            .Where(n => !n.isHit && Mathf.Abs(n.Position) <= timings.imperfectTimingThreshold)
            .FirstOrDefault();
        if (firstNoteInRange == null)
        {
            Hit?.Invoke(NoteHitResult.Miss);
            PlaySound(NoteHitResult.Miss);
            return;
        }

        var result = Mathf.Abs(firstNoteInRange.Position) <= timings.purrfectTimingThreshold
            ? NoteHitResult.Purrfect
            : NoteHitResult.Imperfect;
        HitNote(firstNoteInRange, result);
    }

    private void HitNote(Note note, NoteHitResult result)
    {
        Hit?.Invoke(result);
        note.OnHit(result);
        if (result != NoteHitResult.Miss || playMissSoundOnIgnoreNote)
            PlaySound(result);
    }

    private void PlaySound(NoteHitResult result)
    {
        noteAudioSource.clip = noteSounds?.GetSound(result);
        noteAudioSource.Play();
    }

    private void Update()
    {
        if (!IsPlaying)
            return;

        for (int i = 0; i < notes.Count; i++)
        {
            notes[i].Move(speed * Time.deltaTime);
            if (!notes[i].isHit && notes[i].Position < -timings.imperfectTimingThreshold)
                HitNote(notes[i], NoteHitResult.Miss);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, timings?.purrfectTimingThreshold ?? 0);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, timings?.imperfectTimingThreshold ?? 0);
    }
}
