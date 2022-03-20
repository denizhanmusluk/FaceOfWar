using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class finish : MonoBehaviour
{
    public SoldierCollecting baseSlot;
    [SerializeField] GameObject mainCam, battleCam;
    [SerializeField] CinemachineVirtualCamera finalCam;
    [SerializeField] GameObject battleArea;
    [SerializeField] GameObject finalParticles;
    [SerializeField] GameObject inGameCanvas;
    [SerializeField] LevelScore lvlScore;
    private void Start()
    {
        finalParticles.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            lvlScore.progressActive = false;
            finalParticles.SetActive(true);
            finalCam.Priority = 5;
            Destroy(this.GetComponent<Collider>());
            StartCoroutine(finalDelay(other.GetComponent<PlayerControl>()));
        }
    }
    IEnumerator finalDelay(PlayerControl player)
    {
        yield return new WaitForSeconds(3);
        baseSlot.soldierDrop();
        finalParticles.SetActive(false);
        inGameCanvas.SetActive(false);
        battleCam.SetActive(true);
        mainCam.SetActive(false);
        player.currentBehaviour = PlayerControl.States.idle;
        Destroy(player.transform.parent.gameObject);
        battleArea.SetActive(true);
    }
}
