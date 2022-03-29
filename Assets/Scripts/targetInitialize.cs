using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class targetInitialize : MonoBehaviour, IFightStart
{
    [SerializeField] public targetInitialize otherBase;
    [SerializeField] public List<GameObject> soldier;
    //[SerializeField] GameObject battleCam, mainCam;
    //[SerializeField] CinemachineVirtualCamera  finCam2;
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

        FightManager.Instance.Remove_fightStartObservers(this);

    }
    public void isItFull()
    {
        if (soldier.Count == 5)
        {
            //FightManager.Instance.Notify_GameFinishObservers();
            //for(int i = 0; i< soldier.Count; i++)
            //{
            //    soldier[i].GetComponent<Collider>().enabled = false;
            //}
            StartCoroutine(setColliderDelay());
        }
    }
    IEnumerator setColliderDelay()
    {
        yield return null;
        FightManager.Instance.Notify_GameFinishObservers();
        for (int i = 0; i < soldier.Count; i++)
        {
            soldier[i].GetComponent<Collider>().enabled = false;
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

            }
            if (transform.tag == "base")
            {
                Debug.Log("you won");
                GameManager.Instance.Notify_LoseObservers();
            }

            for (int i = 0; i < otherBase.soldier.Count; i++)
            {
                otherBase.soldier[i].GetComponent<Animator>().SetTrigger("win");
            }
            //battleCam.SetActive(false);
            //mainCam.SetActive(true);
            //StartCoroutine(camDelay());
        }
    }
    //IEnumerator camDelay()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    finCam2.Priority = 50;

    //}
}