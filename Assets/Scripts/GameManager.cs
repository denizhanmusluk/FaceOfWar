using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;

public class GameManager : MonoBehaviour, IWinObserver, ILoseObserver, IEndGameObserver
{
    public bool gameActive;
    public static GameManager Instance;
    [SerializeField] public GameObject startButton;
    [SerializeField] GameObject failPanel;
    [SerializeField] GameObject successPanel;
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject ProgressBar;
    public TextMeshProUGUI moneyLabel;

    [SerializeField] CinemachineVirtualCamera camFirst, camMain;
    //[SerializeField] GameObject confetti;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        endGameObservers = new List<IEndGameObserver>();
        winObservers = new List<IWinObserver>();
        loseObservers = new List<ILoseObserver>();
        startObservers = new List<IStartGameObserver>();
        //finishObservers = new List<IFinish>();
    }
    public void MoneyUpdate(int miktar)
    {
        int moneyOld = Globals.moneyAmount;
        Globals.moneyAmount = Globals.moneyAmount + miktar;
        LeanTween.value(moneyOld, Globals.moneyAmount, 0.2f).setOnUpdate((float val) =>
        {
            moneyLabel.text = "$" + val.ToString("N0");
        });//.setOnComplete(() =>{});
    }
    void Start()
    {
        Globals.moneyAmount = 0;
        camFirst.Priority = 1;
        camMain.Priority = 0;
        startButton.SetActive(true);
        successPanel.SetActive(false);
        failPanel.SetActive(false);
        Add_WinObserver(this);
        Add_LoseObserver(this);
        Add_EndObserver(this);
        moneyLabel.text = "$" + Globals.moneyAmount.ToString();

    }

    private void Update()
    {

    }


    public void StartButton()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(startDelay());
            camFirst.Priority = 0;
            camMain.Priority = 1;
            ProgressBar.SetActive(true);
        }
    }
    IEnumerator startDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Globals.isGameActive = true;
        Globals.finish = false;
        startButton.SetActive(false);
        Notify_GameStartObservers();
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Globals.isGameActive = true;
    }


    public void LoseScenario()
    {
        GameEvents.fightEvent.RemoveAllListeners();
        Globals.isGameActive = false;


        StartCoroutine(Fail_Delay());
    }
    IEnumerator Fail_Delay()
    {
        yield return new WaitForSeconds(2);

        failPanel.SetActive(true);

    }
    public void WinScenario()
    {
        GameEvents.fightEvent.RemoveAllListeners();

        Globals.isGameActive = false;

        StartCoroutine(win_Delay());

        Globals.currentLevel++;
        PlayerPrefs.SetInt("level", Globals.currentLevel);

    }
    IEnumerator win_Delay()
    {
        yield return new WaitForSeconds(0.1f);

        successPanel.SetActive(true);

    }
    public void GameEnd()
    {

    }





    #region Observer Funcs

    private List<IEndGameObserver> endGameObservers;
    private List<IWinObserver> winObservers;
    private List<ILoseObserver> loseObservers;
    private List<IStartGameObserver> startObservers;
    //private List<IFinish> finishObservers;
    //#region Finish Observer
    //public void Add_FinishObserver(IFinish observer)
    //{
    //    finishObservers.Add(observer);
    //}

    //public void Remove_FinishObserver(IFinish observer)
    //{
    //    finishObservers.Remove(observer);
    //}

    //public void Notify_GameFinishObservers()
    //{
    //    foreach (IFinish observer in finishObservers.ToArray())
    //    {
    //        if (finishObservers.Contains(observer))
    //            observer.finish();
    //    }
    //}
    //#endregion

    #region Start Observer
    public void Add_StartObserver(IStartGameObserver observer)
    {
        startObservers.Add(observer);
    }

    public void Remove_StartObserver(IStartGameObserver observer)
    {
        startObservers.Remove(observer);
    }

    public void Notify_GameStartObservers()
    {
        foreach (IStartGameObserver observer in startObservers.ToArray())
        {
            if (startObservers.Contains(observer))
                observer.StartGame();
        }
    }
    #endregion

    #region End Observer
    public void Add_EndObserver(IEndGameObserver observer)
    {
        endGameObservers.Add(observer);
    }

    public void Remove_EndObserver(IEndGameObserver observer)
    {
        endGameObservers.Remove(observer);
    }

    public void Notify_GameEndObservers()
    {
        foreach (IEndGameObserver observer in endGameObservers.ToArray())
        {
            if (endGameObservers.Contains(observer))
                observer.GameEnd();
        }
    }
    #endregion

    #region Win Observer
    public void Add_WinObserver(IWinObserver observer)
    {
        winObservers.Add(observer);
    }

    public void Remove_WinObserver(IWinObserver observer)
    {
        winObservers.Remove(observer);
    }

    public void Notify_WinObservers()
    {
        foreach (IWinObserver observer in winObservers.ToArray())
        {
            if (winObservers.Contains(observer))
                observer.WinScenario();
        }
    }
    #endregion

    #region Lose Observer
    public void Add_LoseObserver(ILoseObserver observer)
    {
        loseObservers.Add(observer);
    }

    public void Remove_LoseObserver(ILoseObserver observer)
    {
        loseObservers.Remove(observer);
    }

    public void Notify_LoseObservers()
    {
        foreach (ILoseObserver observer in loseObservers.ToArray())
        {
            if (loseObservers.Contains(observer))
                observer.LoseScenario();
        }
    }
    #endregion
    #endregion
}