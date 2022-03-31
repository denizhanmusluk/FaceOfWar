using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvas : MonoBehaviour,IFightStart
{
   GameObject batCam;
    bool lookCamera = false;
    void Start()
    {
        FightManager.Instance.Add_fightStartObservers(this);
    }
 public void fightStart()
    {
        FightManager.Instance.Remove_fightStartObservers(this);

        batCam = GameObject.Find("maincamera");
        lookCamera = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookCamera)
        {
            transform.LookAt(batCam.transform);
        }
    }
}
