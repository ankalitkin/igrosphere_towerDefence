using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerHealthSystem))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject waypoints;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject towers;
    [SerializeField] private GameObject mobs;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private float mobSpeed = 2;    
    
    [SerializeField, HideInInspector] private PathProcessor _pathProcessor;
    [SerializeField, HideInInspector] private TowerManager _towerManager;
    [SerializeField, HideInInspector] private Camera _camera;
    [SerializeField, HideInInspector] private PlayerHealthSystem _healthSystem;
    
    public PathProcessor PathProcessor => _pathProcessor;
    public TowerManager TowerManager=> _towerManager;

    public Camera Camera => _camera;

    public float MobSpeed => mobSpeed;

    public PlayerHealthSystem HealthSystem => _healthSystem;

    private List<GameObject> _enemies;

    public GameObject Mobs => mobs;

    private bool _waitingForAnyKey = false;
    private float _time = 0;

    private void OnValidate()
    {
        _pathProcessor = waypoints.GetComponent<PathProcessor>();
        _towerManager = towers.GetComponent<TowerManager>();
        _camera = camera.GetComponent<Camera>();
        _healthSystem = GetComponent<PlayerHealthSystem>();
        Instance = this;
    }

    private void Awake()
    {
        Instance = this;
        _enemies = new List<GameObject>();
    }

    public void AddEnemy(GameObject enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        _enemies.Remove(enemy);
    }

    public GameObject GetClosestEnemy(Vector3 position)
    {
        GameObject closest = null;
        float lastDist = -1;
        foreach (var obj in _enemies)
        {
            float dist = (obj.transform.position - position).sqrMagnitude;
            if (lastDist < 0 || dist < lastDist)
            {
                lastDist = dist;
                closest = obj;
            }
        }

        return closest;
    }

    public void GameOver()
    {
        mobs.SetActive(false);
        waypoints.SetActive(false);
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<Image>().DOFade(0, 1).From();
        gameOverScreen.transform.GetChild(0).GetComponent<Text>().DOFade(0, 1).From();
        _waitingForAnyKey = true;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        UIManager.Instance.TimeLabel.text = "Time: " + (int) _time;
        if (_waitingForAnyKey && Input.anyKey)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
