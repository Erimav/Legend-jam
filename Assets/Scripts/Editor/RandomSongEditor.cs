using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RandomSong))]
public class RandomSongEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Start song"))
        {
            FindObjectOfType<SongController>().PlaySongAsync((ISong)target).Forget();
        }
    }

}
