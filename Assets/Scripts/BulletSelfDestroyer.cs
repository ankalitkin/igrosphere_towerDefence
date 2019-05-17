using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSelfDestroyer : MonoBehaviour
{
    [SerializeField] private float time = 10f;

    void Update()
    {
        time -= Time.deltaTime;
        if(time < 0)
            Destroy(gameObject);
    }
}