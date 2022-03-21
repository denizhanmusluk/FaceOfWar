using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HologramLight : MonoBehaviour, IFightStart
{
    [SerializeField] float intensity;
    void Start()
    {
        FightManager.Instance.Add_fightStartObservers(this);

        //transform.DOScale(Vector3.one * 1.1f, 0.3f).SetLoops(-1, LoopType.Yoyo);
        DOTween.To(x => intensity = x, intensity, 0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
    private void Update()
    {
        transform.GetComponent<Light>().intensity = intensity;
    }
    public void fightStart()
    {
        Destroy(this.gameObject);
    }
}
