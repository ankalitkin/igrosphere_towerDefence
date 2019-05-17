using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class PathUtils
{
    private const float Smoothness = 0.9999f;
    private const float LenThresholdSqr = 1f;

    public static List<Vector3> GetPoints(GameObject obj)
    {
        Transform transform = obj.transform;
        int count = transform.childCount;
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < count; i++)
            points.Add(transform.GetChild(i).position);
        return points;
    }

    public static float Dot(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 v1 = (b - a).normalized;
        Vector3 v2 = (c - b).normalized;
        return Vector3.Dot(v1, v2);
    }

    public static Vector3 Bezier(Vector3 a, Vector3 b, Vector3 c, float amount)
    {
        Vector3 posA = Vector3.Lerp(a, b, amount);
        Vector3 posB = Vector3.Lerp(b, c, amount);
        return Vector3.Lerp(posA, posB, amount);
    }

    public static List<Vector3> GetBezierWay(IList<Vector3> wayPoints)
    {
        List<Vector3> points = new List<Vector3>();
        int count = wayPoints.Count;
        if (count <= 2)
            foreach (var point in wayPoints)
                points.Add(point);
        else
        {
            Vector3 a, b, c;
            a = wayPoints[0];
            for (int i = 1; i < count - 1; i++)
            {
                b = wayPoints[i];
                c = (b + wayPoints[i + 1]) / 2;
                IList<Vector3> currPoints = BezierToPoints(a, b, c);
                points.AddRange(currPoints);
                points.RemoveAt(points.Count - 1);
                a = c;
            }

            b = wayPoints[count - 1];
            points.Add(a);
            points.Add(b);
        }

        return points;
    }

    public static IList<Vector3> BezierToPoints(Vector3 a, Vector3 b, Vector3 c)
    {
        SortedList<float, Vector3> points = new SortedList<float, Vector3> {{0, a}, {1, c}};
        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector3 v1 = points.Values[i];
            Vector3 v2 = points.Values[i + 1];
            if (v1 == v2)
                continue;
            float z = (points.Keys[i] + points.Keys[i + 1]) / 2;
            var m = Bezier(a, b, c, z);
            if ((v2 - v1).sqrMagnitude > LenThresholdSqr || Dot(v1, m, v2) < Smoothness)
            {
                points.Add(z, m);
                i--;
            }
        }

        return points.Values;
    }
}