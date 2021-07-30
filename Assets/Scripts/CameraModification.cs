using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraModification : MonoBehaviour
{
    private Camera cam;
    public float speedScrl, speedMvmt;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }


    // Update is called once per frame
    void Update()
    {
        float ax = Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize += ax * speedScrl * Time.deltaTime;
        if (Input.GetMouseButton(2))
        {
            Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
            cam.transform.position += mouseDelta * speedMvmt * Time.deltaTime;
        }
    }
}
