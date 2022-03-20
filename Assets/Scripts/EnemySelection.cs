using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemySelection : MonoBehaviour
{
    [SerializeField] public GameObject[] warriorsPrefab;
    [SerializeField] public List<TextMeshProUGUI> healthText;

    int selection;
    targetInitialize init;
    void Start()
    {
        init = GetComponent<targetInitialize>();
        for (int i = 0; i < transform.childCount; i++)
        {
            selection = Random.Range(0, warriorsPrefab.Length);
            GameObject war = Instantiate(warriorsPrefab[selection], transform.GetChild(i).transform.position, Quaternion.identity);
            war.transform.parent = transform.GetChild(i).transform;
            init.soldier.Add(war);
            healthText[i].text = war.GetComponent<Fighter>().Maxhealth.ToString();
        }
    }
}
