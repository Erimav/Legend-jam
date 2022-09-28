using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Songs/Timings", fileName = "Timings")]
public class TimingSettingsSO : ScriptableObject
{
    public float purrfectTimingThreshold = 0.05f;
    public float imperfectTimingThreshold = 0.1f;
}
