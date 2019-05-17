using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject mobPrefab;
    [SerializeField, Min(0)] private float delay = 2;
    private float _time;

    void FixedUpdate()
    {
        _time -= Time.fixedDeltaTime;
        if (_time <= 0)
        {
            var mob = Instantiate(mobPrefab, transform.position, Quaternion.identity,
                GameManager.Instance.Mobs.transform);
            GameManager.Instance.AddEnemy(mob);
            _time = delay;
        }
    }
}