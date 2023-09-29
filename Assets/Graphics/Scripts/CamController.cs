using stateman;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CamController : MonoBehaviour
{        
    public static CamController i { get; private set; }

    float zoomspeed = 5.0f;

    public Camera zoomCam;

    private void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Main Camera");
    }

    public void HandleUpdate()
    {
        Vector3 vec;
        var tempcamsize = zoomCam.orthographicSize;
        zoomCam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * ((zoomspeed * Time.deltaTime) / .05f);
        if (GameController.i.statemachine.CurrentState == DiplomacyStateMain.i)
        {
            zoomCam.orthographicSize = tempcamsize;
        }

        if (zoomCam.orthographicSize <= 1)
        { zoomCam.orthographicSize = 1; }
        if(zoomCam.orthographicSize >= 11)
        { zoomCam.orthographicSize = 11; }
        zoomCam.transform.Translate(Input.GetAxisRaw("Horizontal") * 0.01f, Input.GetAxisRaw("Vertical") * 0.01f, -10);

        vec.x = Mathf.Clamp(zoomCam.transform.position.x, 106f, 128f); 
        vec.y = Mathf.Clamp(zoomCam.transform.position.y, 64.5f, 79.5f); 
        vec.z = -10;
        zoomCam.transform.position = vec;
    }

    // Update is called once per frame
    void Update()
    {
        HandleUpdate();
    }
}
