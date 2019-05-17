using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MobHealthSystem : MonoBehaviour
{
    public Vector3 HealthBarPoint => transform.position + UIManager.Instance.HealthBarOffset;
    public float Health => _health;
    private MobHealthBar _hpBar;
    private float _health = 1;

    public MobHealthBar HpBar => _hpBar;

    private void Awake()
    {
        _hpBar = Instantiate(UIManager.Instance.HealthBarPrefab, UIManager.Instance.HealthBarContainer.transform).GetComponent<MobHealthBar>();
        _hpBar.Parent = this;
        _hpBar.ProcessPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            Destroy(other.gameObject);

            _health -= GameManager.Instance.TowerManager.Damage;
            if (_health <= Mathf.Epsilon)
            {
                Mob.Destroy(gameObject);
            }
        }
    }
}