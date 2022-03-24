using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCompare : MonoBehaviour
{
    [SerializeField] GameObject enemySlot;
    [SerializeField] Material red, green;
    [SerializeField] MeshRenderer slotGround;
    [SerializeField] MeshRenderer EnemySlotGround;

    public void matSet()
    {
        if (enemySlot.transform.GetChild(1).GetComponent<Fighter>().Maxhealth >transform.GetChild(0).GetComponent<Fighter>().Maxhealth)
        {
            slotGround.material = red;
            EnemySlotGround.material = green;
        }
        else
        {
            slotGround.material = green;
            EnemySlotGround.material = red;
        }
    }
}