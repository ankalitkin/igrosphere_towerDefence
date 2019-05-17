using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera _camera => GameManager.Instance.Camera;
    
    void Update()
    {
        if(_camera.orthographic)
            transform.rotation = _camera.transform.rotation;
        else
            transform.LookAt(transform.position - _camera.transform.position, Vector3.up);
    }
}
