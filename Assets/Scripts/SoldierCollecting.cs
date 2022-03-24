using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SoldierCollecting : MonoBehaviour
{
    [SerializeField] public List<GameObject> soldiers;
    [SerializeField] public List<TextMeshProUGUI> healthText;
    int soldierNum = 0;
    [SerializeField] public GameObject botSoldier;
    public void soldierDrop()
    {
        for (int i = 0; i < soldiers.Count; i++)
        {
            GameObject soldier = Instantiate(soldiers[i], transform.GetChild(i).transform.position, Quaternion.identity);
            soldier.transform.parent = transform.GetChild(i).transform;
            soldier.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //transform.GetChild(i).GetComponent<illusion>().hologramSoldier = soldier;
            //transform.GetChild(i).GetComponent<illusion>().tweenScale();
        }
        if (soldiers.Count < 5)
        {
            for (int i = soldiers.Count; i < 5; i++)
            {
                soldiers.Add(botSoldier);
                GameObject soldier = Instantiate(botSoldier, transform.GetChild(i).transform.position, Quaternion.identity);
                soldier.transform.parent = transform.GetChild(i).transform;
                soldier.transform.localRotation = Quaternion.Euler(0, 0, 0);

                healthText[i].text = soldier.GetComponent<SoldierDrag>().warriourPrefab.GetComponent<Fighter>().Maxhealth.ToString();

                //transform.GetChild(i).GetComponent<illusion>().hologramSoldier = soldier;
                //transform.GetChild(i).GetComponent<illusion>().tweenScale();
            }
        }
    }

    public void healtInit(float power)
    {
        healthText[soldierNum].text = power.ToString();
        soldierNum++;
    }
}