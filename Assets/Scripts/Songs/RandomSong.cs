using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Random song", menuName = "Songs/RandomSong")]
public class RandomSong : ScriptableObject, ISong
{
    [field: SerializeField]
    public AudioClip Track { get; set; }

    [field: SerializeField]
    public AudioClip ForegroundTrack { get; set; }

    [field: SerializeField]
    public int NumberOfLines { get; set; }

    public float minSpawnRate = .5f;
    public float maxSpawnRate = .5f;
    public float startTime = 2;

    //Higher value means lower chance of spawning notes on multible lines
    public float multipleLinesExponentiation = 3;

    public void SpawnNotes(IReadOnlyList<NoteLine> lines)
    {
        for (float t = startTime; t < Track.length; t += Random.Range(minSpawnRate, maxSpawnRate))
        {
            var randomLineIndex = Random.Range(0, NumberOfLines);
            var numOfLinesToSpawn = 1 + Mathf.FloorToInt(Mathf.Pow(Random.value, multipleLinesExponentiation) * NumberOfLines);

            for (int i = 0; i < numOfLinesToSpawn; i++)
            {
                var lineIndex = (randomLineIndex + i) % lines.Count;
                lines[lineIndex].CreateNote(t);
            }
        }
    }
}
