using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private Transform _transform;

    public float Position => _transform.localPosition.x;

    public bool isHit;

    public event Action<NoteHitResult> Hit;

    void Start()
    {
        _transform = transform;
    }

    public void Move(float speed)
    {
        _transform.position -= _transform.right * speed;
    }

    public void OnHit(NoteHitResult result)
    {
        isHit = true;
        Hit?.Invoke(result);
    }
}
