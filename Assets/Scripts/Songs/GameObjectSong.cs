using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSong : MonoBehaviour, ISong
{
    [field: SerializeField]
    public AudioClip Track { get; set; }

    [field:SerializeField]
    public AudioClip ForegroundTrack { get; set; }

    public NoteSoundsSetSO[] noteSounds;

    public int NumberOfLines => transform.childCount;

    public void SpawnNotes(IReadOnlyList<NoteLine> lines)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            var lineToCopy = transform.GetChild(i);
            var noteInstance = lineToCopy.GetChild(0).GetComponent<Note>();
            if (noteInstance != null)
            {
                lines[i].SetNoteInstance(noteInstance);
                lines[i].noteSounds = noteSounds[i];
            }

            for (int j = 0; j < lineToCopy.childCount; j++)
            {
                var note = lineToCopy.GetChild(j);
                lines[i].CreateNote(note.localPosition.x);
            }
        }
    }

    [ContextMenu("Start")]
    public void StartSong()
    {
        var songController = FindObjectOfType<SongController>();
        songController.PlaySongAsync(this).Forget();
    }
}
