using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPoint : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private TowerManager _towerManager => GameManager.Instance.TowerManager;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, _towerManager.Radius);
    }
#endif
    private void OnMouseDown()
    {
        if (_towerManager.TowerPointAvailable)
        {
            Instantiate(_towerManager.TowerPrefab, transform.position, Quaternion.identity,
                transform.parent);
            _towerManager.ResetCoolDown();
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        _renderer.color = _towerManager.TowerPointColor;
    }
}