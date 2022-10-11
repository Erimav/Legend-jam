using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteParticle : MonoBehaviour
{
    public Note note;
    private ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();

        note.Hit += hit =>
        {
            if (hit != NoteHitResult.Miss)
                ps.Play();
        };
    }
}
