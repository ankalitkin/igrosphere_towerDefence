using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SelfDrivenBullet : MonoBehaviour
{
    [HideInInspector] public GameObject goTo;
    [HideInInspector] public float duration;
    private Vector3 _oldPos;
    private float _time;

    private void Start()
    {
        _oldPos = transform.position;
    }

    void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;
        transform.position = Vector3.Lerp(_oldPos, goTo.transform.position, _time / duration);
    }
}