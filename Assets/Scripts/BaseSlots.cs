using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSlots : MonoBehaviour
{
    public List<GameObject> slots;
    public GameObject placeLocation;
    GameObject placeDizilim;
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            slots.Add(transform.GetChild(i).gameObject);
        }
    }
    public void listSet()
    {
        slots.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).transform.GetComponent<slot>().active)
            {
                slots.Add(transform.GetChild(i).gameObject);
            }
        }
        if(slots.Count > 0)
        {
            positionSet();
        }
    }
    // Update is called once per frame
    public void positionSet()
    {
        placeDizilim = placeLocation.transform.GetChild(slots.Count - 1).gameObject;
        for (int i = 0; i < slots.Count; i++)
        {
            //slots[i].transform.position = placeLoc.transform.GetChild(i).transform.position;
            StartCoroutine(moving(slots[i].transform, placeDizilim.transform.GetChild(i).transform));
        }
    }
    IEnumerator moving(Transform hologramSlot,Transform target)
    {
        while (Vector3.Distance(hologramSlot.position, target.position) > 0.1f)
        {
            hologramSlot.position = Vector3.MoveTowards(hologramSlot.position, target.position, 10 * Time.deltaTime);
            yield return null;
        }
    }
}
