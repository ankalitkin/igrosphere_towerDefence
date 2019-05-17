using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private GameObject healthBarContainer;
    [SerializeField] private GameObject timeLabel;
    [SerializeField] private Vector3 healthBarWorldOffset = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 healthBarScreenOffset = new Vector3(0, 50, 0);
    [SerializeField, HideInInspector] private Text _timeLabel;
    public GameObject HealthBarPrefab => healthBarPrefab;

    public GameObject HealthBarContainer => healthBarContainer;

    public Vector3 HealthBarOffset => healthBarWorldOffset;
    public Vector3 HealthScreenOffset => healthBarScreenOffset;

    public Text TimeLabel => _timeLabel;

    private void OnValidate()
    {
        Instance = this;
        _timeLabel = timeLabel.GetComponent<Text>();
    }

    private void Awake()
    {
        Instance = this;
    }
}