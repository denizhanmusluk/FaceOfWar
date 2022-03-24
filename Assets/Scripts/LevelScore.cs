using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelScore : MonoBehaviour
{
    public static LevelScore Instance;
    public TextMeshProUGUI moneyLabel;
    [SerializeField] Transform currentPoint, finishPoint;
    [SerializeField] Slider slider;
    float totalDistance;
   public bool progressActive = true;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        totalDistance = Vector3.Distance(finishPoint.position, currentPoint.position);
        //slider.value = 0;
        //StartCoroutine(progressBar());
    }
    IEnumerator progressBar()
    {
        while (progressActive)
        {
            slider.value = 1 - Vector3.Distance(finishPoint.position, currentPoint.position) / totalDistance;
            yield return null;
        }
    }
 
  
    public void MoneyUpdate(int miktar)
    {
        int moneyOld = Globals.moneyAmount;
        Globals.moneyAmount = Globals.moneyAmount + miktar;
        if(Globals.moneyAmount < 0)
        {
            Globals.moneyAmount = 0;
        }
        LeanTween.value(moneyOld, Globals.moneyAmount, 0.2f).setOnUpdate((float val) =>
        {
            moneyLabel.text = "$" + val.ToString("N0");
        });//.setOnComplete(() =>{});
    }
}