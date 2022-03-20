using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public static UnityEvent fightEvent;

    private void Awake()
    {
        if(fightEvent == null)
        {
            fightEvent = new UnityEvent();
        }
    }

    private void LateUpdate()
    {
        fightEvent.Invoke();
    }
}
