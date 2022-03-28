using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraManager : MonoBehaviour, IStartGameObserver,IFightStart
{
    [SerializeField] CinemachineVirtualCamera camFirst, camMain;
    [SerializeField]
    GameObject battleCam, mainCam;
    [SerializeField] CinemachineVirtualCamera finCam2;

    //Vector3 battleCamNewPos = new Vector3(-1.88f, 13.42f, 6.69f);
    //Quaternion battleCamNewRot = Quaternion.Euler(44.119f, 21.605f, 0);
    //float cameraTargetSize = 18.1f;
    //// Start is called before the first frame update
    //float cameraFirstSize;

    void Start()
    {
        //cameraFirstSize = battleCam.GetComponent<Camera>().orthographicSize;
        FightManager.Instance.Add_fightStartObservers(this);

        GameManager.Instance.Add_StartObserver(this);
        camFirst.Priority = 1;
        camMain.Priority = 0;
    }
    public void StartGame()
    {
        camFirst.Priority = 0;
        camMain.Priority = 1;
        GameManager.Instance.Remove_StartObserver(this);

    }
    public void fightStart()
    {

        FightManager.Instance.Remove_fightStartObservers(this);
        battleCam.SetActive(false);
        mainCam.SetActive(true);
        StartCoroutine(camDelay());
    }

    IEnumerator camDelay()
    {
        yield return new WaitForSeconds(0.2f);
        finCam2.Priority = 50;

    }
    //IEnumerator setBattleCam()
    //{
    //    float counter = 0f;
    //    while (counter < 2f)
    //    {
    //        counter += Time.deltaTime;
    //        battleCam.transform.localPosition = Vector3.MoveTowards(battleCam.transform.localPosition, battleCamNewPos, 5*Time.deltaTime);
    //        battleCam.transform.localRotation = Quaternion.RotateTowards(battleCam.transform.localRotation, battleCamNewRot,15* Time.deltaTime);
    //        //DOTween.To(x => cameraFirstSize = x, cameraFirstSize, cameraTargetSize, 0.5f).SetLoops(-1, LoopType.Yoyo);
    //        DOTween.To(x => cameraFirstSize = x, cameraFirstSize, cameraTargetSize, 0.5f);
    //        battleCam.GetComponent<Camera>().orthographicSize = cameraFirstSize;
    //        yield return null;
    //    }
    //}
}
