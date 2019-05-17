using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [Header("Tower settings")] [SerializeField]
    private float attackSpeed = 1;

    [SerializeField] private float damage = 0.2f;
    [SerializeField] private float radius = 4;
    [SerializeField] private float coolDown = 5;
    [SerializeField] private float bulletSpeed = 2;
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Color towerPointAvailableColor = Color.red;
    [SerializeField] private Color towerPointNotAvailableColor = Color.black;
    [SerializeField] private Color levelOneTowerColor = Color.gray;
    [SerializeField] private Color levelTwoTowerColor = Color.yellow;

    private float _cdTime = 0;
    public GameObject TowerPrefab => towerPrefab;
    public GameObject BulletPrefab => bulletPrefab;

    public float AttackSpeed => attackSpeed;
    public float Damage => damage;
    public float Radius => radius;
    public float SqrRadius => radius * radius;

    public bool TowerPointAvailable => _cdTime < 0;
    public Color TowerPointColor => _cdTime < 0 ? towerPointAvailableColor : towerPointNotAvailableColor;

    public Color LevelOneTowerColor => levelOneTowerColor;

    public Color LevelTwoTowerColor => levelTwoTowerColor;

    public float BulletSpeed => bulletSpeed;

    private void Update()
    {
        _cdTime -= Time.deltaTime;
    }

    public void ResetCoolDown()
    {
        _cdTime = coolDown;
    }
}