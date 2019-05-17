using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Mob : MonoBehaviour
{
    [SerializeField] private bool lookForward = true;
    private List<Vector3> _points;
    private int _index;
    private Vector3 _currPoint;
    private float _way;
    public Vector3 Forward { get; private set; }
    
    void Start()
    {
        _points = GameManager.Instance.PathProcessor.Points;
        _index = 0;
        _currPoint = _points[_index++];
    }

    void Update()
    {
        _way += GameManager.Instance.MobSpeed * Time.deltaTime;
        float mag = 0;
        while (_index < _points.Count && _way > (mag = (_points[_index] - _currPoint).magnitude))
        {
            _way -= mag;
            _currPoint = _points[_index++];
        }
        if (_index < _points.Count)
        {
            Forward = (_points[_index] - _currPoint).normalized;
            transform.position = Vector3.Lerp(_currPoint, _points[_index], _way / mag);
            if(lookForward)
                transform.LookAt(_points[_index]);
        }
    }

    public static void Destroy(GameObject gameObject)
    {
        GameManager.Instance.RemoveEnemy(gameObject);
        GameObject.Destroy(gameObject.GetComponent<MobHealthSystem>().HpBar.gameObject);
        GameObject.Destroy(gameObject);
    }
}