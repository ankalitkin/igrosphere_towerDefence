using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PrimitiveTower : MonoBehaviour
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
        GetComponent<MeshRenderer>().material.color = _towerManager.LevelOneTowerColor;
    }

    private void FixedUpdate()
    {
        _time -= Time.fixedDeltaTime;
        GameObject target = GameManager.Instance.GetClosestEnemy(transform.position);
        if (target == null)
            return;
        Vector3 gunPosition = transform.GetChild(0).position;
        Vector3 direction = (target.GetComponent<Mob>().Forward * GameManager.Instance.MobSpeed - gunPosition +
                             target.transform.position).normalized;
        Vector3 point = gunPosition + direction;
        point.y = transform.position.y;
        transform.LookAt(point);
        if ((target.transform.position - transform.position).sqrMagnitude <= _towerManager.SqrRadius)
        {
            if (_time < 0)
            {
                var bullet = Instantiate(_towerManager.BulletPrefab, gunPosition,
                    transform.rotation);
                Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
                rigidbody.isKinematic = false;
                rigidbody.AddForce(_towerManager.BulletSpeed * direction, ForceMode.VelocityChange);
                _time = _towerManager.AttackSpeed;
            }
        }
    }

    private void OnMouseDown()
    {
        if (_towerManager.TowerPointAvailable)
        {
            gameObject.AddComponent<SelfDrivenBulletTower>();
            Destroy(this);
            _towerManager.ResetCoolDown();
        }
    }
}