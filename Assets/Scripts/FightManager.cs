using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour,IFinish
{
    [SerializeField] public GameObject fightStartButton;


    public static FightManager Instance;

    private List<IFightStart>fightStartObservers;
    private List<IFightEnd> fightEndObservers;
    private List<IFinish> finishObservers;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        fightStartObservers = new List<IFightStart>();
        fightEndObservers = new List<IFightEnd>();
        finishObservers = new List<IFinish>();

    }
    private void Start()
    {
        Add_FinishObserver(this);
    }
    public void fightPlayButton()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Notify_fightStartObservers();
            fightStartButton.SetActive(false);
        }
    }

    public void finish()
    {
        fightStartButton.SetActive(true);
    }



    public void Add_fightStartObservers(IFightStart observer)
    {
        fightStartObservers.Add(observer);
    }

    public void Remove_fightStartObservers(IFightStart observer)
    {
        fightStartObservers.Remove(observer);
    }

    public void Notify_fightStartObservers()
    {
        foreach (IFightStart observer in fightStartObservers.ToArray())
        {
            if (fightStartObservers.Contains(observer))
                observer.fightStart();
        }
    }

    public void Add_fightEndObservers(IFightEnd observer)
    {
        fightEndObservers.Add(observer);
    }

    public void Remove_fightEndObservers(IFightEnd observer)
    {
        fightEndObservers.Remove(observer);
    }

    public void Notify_fightEndObservers()
    {
        foreach (IFightEnd observer in fightEndObservers.ToArray())
        {
            if (fightEndObservers.Contains(observer))
                observer.fightEnd();
        }
    }

    #region Finish Observer
    public void Add_FinishObserver(IFinish observer)
    {
        finishObservers.Add(observer);
    }

    public void Remove_FinishObserver(IFinish observer)
    {
        finishObservers.Remove(observer);
    }

    public void Notify_GameFinishObservers()
    {
        foreach (IFinish observer in finishObservers.ToArray())
        {
            if (finishObservers.Contains(observer))
                observer.finish();
        }
    }
    #endregion
}
