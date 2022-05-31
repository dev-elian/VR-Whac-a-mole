using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] Transform _camera;

    void LateUpdate() {
        transform.LookAt(transform.position+ Vector3.Reflect(transform.position-_camera.position, Vector3.forward));
    }
}
