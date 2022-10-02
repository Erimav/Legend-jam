using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISong
{
    AudioClip Track { get; }
    AudioClip ForegroundTrack { get; }

    int NumberOfLines { get; }

    void SpawnNotes(IReadOnlyList<NoteLine> lines);
}
