using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollecting : MonoBehaviour
{
    GameObject target;
    //[SerializeField] Material firstMat;
    //Color FirstColour;
    //[SerializeField] Material mat;
    public enum func { rotate, collecting, none }
    public func selecting;
    bool collectActive = false;
    public int moneyValue;
    float motionSpeed;
    //void Start()
    //{
    //    //FirstColour = firstMat.GetColor("_EmissionColor");
    //    selecting = func.rotate;
    //    StartCoroutine(collectActivator());
    //}
    //IEnumerator collectActivator()
    //{
    //    yield return new WaitForSeconds(1);
    //    collectActive = true;
    //}
    //// Update is called once per frame
    //private void Update()
    //{
    //    switch (selecting)
    //    {
    //        case func.rotate:
    //            {
    //                //transform.Rotate(50 * Time.deltaTime, 0,0);
    //            }
    //            break;
    //        case func.collecting:
    //            {
    //                transform.Rotate(0, 1000 * Time.deltaTime,0);
    //                targetMotion();
    //            }
    //            break;
    //        case func.none:
    //            {

    //            }
    //            break;

    //    }
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.GetComponent<IDamagable>() != null && collectActive)
    //    {
    //        //other.gameObject.GetComponent<Player>().MoneyUpdate(30);

    //        motionSpeed = other.GetComponent<PlayerControl>().acceleration;
    //        LevelScore.Instance.MoneyUpdate(moneyValue);
    //        target = other.GetComponent<PlayerControl>().moneyTarget;
    //        selecting = func.collecting;
    //        Destroy(GetComponent<Rigidbody>());
    //        //Score.Instance.scoreUp();

    //    }
    //}
    //void targetMotion()
    //{
    //    if (Vector3.Distance(transform.position, target.transform.position) > 0.3f)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, (2 / Vector3.Distance(transform.position, target.transform.position))* motionSpeed *  Time.deltaTime);
    //        transform.localScale = Vector3.Lerp(transform.localScale, target.transform.localScale, motionSpeed* 1f * Time.deltaTime);
    //    }
    //    else
    //    {
    //        selecting = func.none;
    //        GameObject money = gameObject;
    //        money.transform.parent = null;
    //        Destroy(money);
    //    }
    //}
}
