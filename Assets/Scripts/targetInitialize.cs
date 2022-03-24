using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetInitialize : MonoBehaviour, IFightStart
{
    [SerializeField] public targetInitialize otherBase;
    [SerializeField] public List<GameObject> soldier;
    private void Start()
    {
        FightManager.Instance.Add_fightStartObservers(this);

    }
    private void Awake()
    {

        for (int i = 0; i < soldier.Count; i++)
        {
            //soldier[i].GetComponent<Fighter>().targetInitialize = this;
        }
    }
    public void fightStart()
    {
        for (int i = 0; i < soldier.Count; i++)
        {
            soldier[i].GetComponent<Fighter>().targetInitialize = this;

            soldier[i].GetComponent<Fighter>().attackTarget = otherBase.soldier[i];
        }
    }
    public void isItFull()
    {
        Debug.Log(soldier.Count);
        if (soldier.Count == 5)
        {
            FightManager.Instance.Notify_GameFinishObservers();
            Debug.Log("full");
        }
    }
    public void whoWon()
    {
        if (soldier.Count == 0)
        {
            if (transform.tag == "enemy")
            {
                Debug.Log("enemy won");
                GameManager.Instance.Notify_WinObservers();
                for (int i = 0; i < soldier.Count; i++)
                {
                    soldier[i].GetComponent<Animator>().SetTrigger("win");
                }
            }
            if (transform.tag == "base")
            {
                Debug.Log("you won");
                GameManager.Instance.Notify_LoseObservers();
            }
        }
    }
}