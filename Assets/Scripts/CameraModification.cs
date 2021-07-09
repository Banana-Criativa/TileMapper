using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraModification : MonoBehaviour
{
    private Camera cam;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }


    // Update is called once per frame
    void Update()
    {
        float ax = Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize += ax * speed * Time.deltaTime;
    }
}
