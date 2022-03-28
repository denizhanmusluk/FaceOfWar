using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemySelection : MonoBehaviour, IFightStart
{
    [SerializeField] public GameObject[] warriorsPrefab;
    [SerializeField] public List<TextMeshProUGUI> healthText;

    int selection;
    targetInitialize init;
    void Start()
    {
        FightManager.Instance.Add_fightStartObservers(this);
        init = GetComponent<targetInitialize>();
        for (int i = 0; i < transform.childCount; i++)
        {
            selection = Random.Range(0, warriorsPrefab.Length);
            GameObject war = Instantiate(warriorsPrefab[selection], transform.GetChild(i).transform.position, Quaternion.identity);
            war.transform.parent = transform.GetChild(i).transform;
            war.transform.localRotation = Quaternion.Euler(0, 180, 0);
            init.soldier.Add(war);
            healthText[i].text = war.GetComponent<Fighter>().Maxhealth.ToString();
        }
    }
    public void fightStart()
    {
        for (int i = 0; i < healthText.Count; i++)
        {
            healthText[i].transform.parent.parent.parent.gameObject.SetActive(false);
        }
        FightManager.Instance.Remove_fightStartObservers(this);

    }
}
