using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SelfDrivenBulletTower : MonoBehaviour
{
    private TowerManager _towerManager => GameManager.Instance.TowerManager;
    private float _time;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, _towerManager.Radius);
    }
#endif

    private void OnEnable()
    {
        GetComponent<MeshRenderer>().material.color = _towerManager.LevelTwoTowerColor;
    }

    void FixedUpdate()
    {
        _time -= Time.fixedDeltaTime;
        GameObject target = GameManager.Instance.GetClosestEnemy(transform.position);
        if (target != null)
        {
            Vector3 point = target.transform.position;
            point.y = transform.position.y;
            transform.LookAt(point);
            if (_time < 0 && (target.transform.position - transform.position).sqrMagnitude <= _towerManager.SqrRadius)
            {
                var bullet = Instantiate(_towerManager.BulletPrefab, transform.GetChild(0).position,
                    transform.rotation);
                bullet.GetComponent<Rigidbody>().isKinematic = true;
                SelfDrivenBullet sdb = bullet.AddComponent<SelfDrivenBullet>();
                sdb.goTo = target;
                sdb.duration = 1;
                _time = _towerManager.AttackSpeed;
            }
        }
    }
}