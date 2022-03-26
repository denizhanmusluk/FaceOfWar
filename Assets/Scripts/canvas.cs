using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvas : MonoBehaviour
{
   GameObject batCam;
    void Start()
    {
        batCam = GameObject.Find("battlecamera");

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(batCam.transform);
    }
}
