using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Songs/Note Sounds Set", fileName = "NoteSoundsSet")]
public class NoteSoundsSetSO : ScriptableObject
{
    public AudioClip missSound;
    public AudioClip imperfectSound;
    public AudioClip purrfectSound;

    public AudioClip GetSound(NoteHitResult hit) => hit switch
    {
        NoteHitResult.Miss => missSound,
        NoteHitResult.Imperfect => imperfectSound,
        NoteHitResult.Purrfect => purrfectSound,
        _ => throw new System.NotImplementedException(),
    };
}
