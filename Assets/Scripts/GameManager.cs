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
    [SerializeField] GameObject gamePanel;
    public TextMeshProUGUI moneyLabel;
    

    [SerializeField] RectTransform successImage, failImage;
    float firstImageScale = 10;
    float lastImageScale = 0.7f;
    public LevelManager lvlManager;
    //[SerializeField] CinemachineVirtualCamera camFirst, camMain;
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
    //public void MoneyUpdate(int miktar)
    //{
    //    int moneyOld = Globals.moneyAmount;
    //    Globals.moneyAmount = Globals.moneyAmount + miktar;
    //    LeanTween.value(moneyOld, Globals.moneyAmount, 0.2f).setOnUpdate((float val) =>
    //    {
    //        moneyLabel.text = "$" + val.ToString("N0");
    //    });//.setOnComplete(() =>{});
    //}
    void Start()
    {
        Globals.moneyAmount = 0;
    
        startButton.SetActive(true);
        successPanel.SetActive(false);
        failPanel.SetActive(false);
        gamePanel.SetActive(true);
        Add_WinObserver(this);
        Add_LoseObserver(this);
        Add_EndObserver(this);
        moneyLabel.text =Globals.moneyAmount.ToString();

    }

    private void Update()
    {

    }


    public void StartButton()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(startDelay());

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
        Globals.currentLevelIndex = 0;
        PlayerPrefs.SetInt("level", 0);


        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevelbutton()
    {
        Globals.currentLevelIndex++;
        if (Globals.LevelCount - 1< Globals.currentLevelIndex)
        {
            Globals.currentLevelIndex = 0;
            PlayerPrefs.SetInt("level", 0);

        }
        else
        {
            PlayerPrefs.SetInt("level", Globals.currentLevelIndex);

        }
        Start();
        Destroy(lvlManager.loadedLevel);
        lvlManager.levelLoad();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Globals.isGameActive = true;
    }
    public void failLevelbutton()
    {
        PlayerPrefs.SetInt("level", Globals.currentLevelIndex);

        Start();
        Destroy(lvlManager.loadedLevel);
        lvlManager.levelLoad();
        Globals.isGameActive = true;
    }

    public void LoseScenario()
    {
        GameEvents.fightEvent.RemoveAllListeners();
        Globals.isGameActive = false;


        StartCoroutine(Fail_Delay());
    }
    IEnumerator Fail_Delay()
    {
        yield return new WaitForSeconds(3f);

        failPanel.SetActive(true);
        failImage.localScale = new Vector3(firstImageScale, firstImageScale, firstImageScale);
        StartCoroutine(panelScaleSet(failImage));

    }
    public void WinScenario()
    {
        GameEvents.fightEvent.RemoveAllListeners();

        Globals.isGameActive = false;

        StartCoroutine(win_Delay());

        //Globals.currentLevel++;
        //PlayerPrefs.SetInt("level", Globals.currentLevel);

    }
    IEnumerator win_Delay()
    {
        yield return new WaitForSeconds(3f);

        successPanel.SetActive(true);
        successImage.localScale = new Vector3(firstImageScale, firstImageScale, firstImageScale); 
        StartCoroutine(panelScaleSet(successImage));
    }
    IEnumerator panelScaleSet(RectTransform image)
    {
        float counter = firstImageScale;
        while (counter > lastImageScale)
        {
            counter -= 20 * Time.deltaTime;
            image.localScale = new Vector3(counter, counter, counter);
            yield return null;
        }
        image.localScale = new Vector3(lastImageScale, lastImageScale, lastImageScale);
        counter = 0f;
        float scale = 0;
        while (counter < Mathf.PI)
        {
            counter += 10 * Time.deltaTime;
            scale = Mathf.Sin(counter);
            scale *= 0.3f;
            image.localScale = new Vector3(lastImageScale - scale, lastImageScale - scale, lastImageScale - scale);
            yield return null;
        }
        image.localScale = new Vector3(lastImageScale, lastImageScale, lastImageScale);

    }
    public void GameEnd()
    {
        gamePanel.SetActive(false);

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
