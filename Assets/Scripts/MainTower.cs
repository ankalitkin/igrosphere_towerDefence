using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTower : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Mob>() == null) return;
        Mob.Destroy(other.gameObject);
        GameManager.Instance.HealthSystem.RemoveLife();
    }

}
