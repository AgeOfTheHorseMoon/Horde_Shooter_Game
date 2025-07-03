using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform _target;

    [SerializeField] Vector2 _mapX, _mapY;

    void Update()
    {
        // transform.position = _target.position + _offset;

        transform.position = new Vector3(Mathf.Clamp(_target.position.x, _mapX.x, _mapX.y), Mathf.Clamp(_target.position.y, _mapY.x, _mapY.y) ,transform.position.z);
    }
}
