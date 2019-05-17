using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathProcessor : MonoBehaviour
{
    public List<Vector3> Waypoints { get; private set; }
    public List<Vector3> Points { get; private set; }

    private void UpdatePointLists()
    {
        Waypoints = PathUtils.GetPoints(gameObject);
        Points = PathUtils.GetBezierWay(Waypoints);
    }
    private void OnValidate()
    {
        UpdatePointLists();
    }
    
    private void OnEnable()
    {
        UpdatePointLists();
    }


    private void OnDrawGizmos()
    {
        UpdatePointLists();
        for (int i = 0; i < Points.Count - 1; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(Points[i], Vector3.one / 25);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(Points[i], Points[i + 1]);
        }
    }
}
