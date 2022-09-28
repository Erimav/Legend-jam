using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISong
{
    AudioClip Track { get; }

    int NumberOfLines { get; }

    void SpawnNotes(IReadOnlyList<NoteLine> lines);
}
