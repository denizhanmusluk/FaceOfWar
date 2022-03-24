using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

    public class SoldierHit : MonoBehaviour
    {
        [SerializeField] GameObject soldierPrefab;
        protected Soldier[] childrenSoldier;
        [SerializeField] public GameObject hologramSoldier;
        [SerializeField] public GameObject prefabHologramSoldier;
        Sequence sequence;
        Sequence sequence2;
        [SerializeField] public int soldierCost;
        [SerializeField] TextMeshProUGUI costText;
        //[SerializeField] List<Material> material;
        [SerializeField] GameObject splashEffect;
        SoldierCollecting _soldier;
    public TextMeshProUGUI power;
    public float soldierPower;
    [SerializeField] Material greenMat;
    [SerializeField] MeshRenderer kurdele;
    [SerializeField] MeshRenderer hologramGround;

    private void Start()
    {
        soldierPower = prefabHologramSoldier.GetComponent<SoldierDrag>().warriourPrefab.GetComponent<Fighter>().Maxhealth;
        costText.text = soldierCost.ToString();
        tweenScale();
        power.text = soldierPower.ToString();
    }
        void tweenScale()
        {
            //foreach (var mtrl in material)
            //{
            //    Color32 clr = mtrl.GetColor("_Color");
            //    mtrl.DOColor(clr + new Color(0,0,0,0.2f), 0.5f).SetLoops(-1, LoopType.Yoyo);
            //}

            sequence = DOTween.Sequence();
            sequence2 = DOTween.Sequence();
            sequence.Append(hologramSoldier.transform.DOMoveY(0.05f, 0.4f).SetLoops(-1, LoopType.Yoyo));
            sequence2.Append(hologramSoldier.transform.DORotate(new Vector3(0, 45, 0), 0.8f).SetLoops(-1, LoopType.Yoyo));

            sequence.AppendInterval(0f);
            sequence.SetLoops(-1, LoopType.Yoyo);
            sequence.SetRelative(true);
            sequence2.AppendInterval(0f);
            sequence2.SetLoops(-1, LoopType.Yoyo);
            sequence2.SetRelative(true);
        }
    private void Update()
    {
        //hologramSoldier.transform.Rotate(0, 50 * Time.deltaTime, 0);
        if (soldierCost <= Globals.moneyAmount)
        {
            costText.color = Color.white;
        }
        else
        {
            costText.color = Color.red;
        }
    }

        private void OnCollisionEnter(Collision other)
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                sequence.Kill(this);
                sequence2.Kill(this);

                if (soldierCost <= Globals.moneyAmount)
                {
                other.GetComponent<PlayerControl>().slotNum++;
                LevelScore.Instance.MoneyUpdate(-soldierCost);
                    transform.parent.GetChild(0).GetComponent<Collider>().enabled = false;
                    transform.parent.GetChild(1).GetComponent<Collider>().enabled = false;

                    GameObject _soldier = Instantiate(soldierPrefab, new Vector3(hologramSoldier.transform.position.x, transform.position.y, hologramSoldier.transform.position.z), hologramSoldier.transform.rotation);
                    hologramSoldier.transform.parent = null;
                    Destroy(hologramSoldier);
                    _soldier.GetComponent<Soldier>().followTarget = other.transform;
                _soldier.GetComponent<Soldier>().slotNum = other.GetComponent<PlayerControl>().slotNum;
               GameObject particle = Instantiate(splashEffect, transform.position, Quaternion.identity);
                particle.transform.rotation = Quaternion.Euler(-90, 0, 0);
                particle.transform.localScale = new Vector3(0.16f, 0.16f, 0.16f);
                other.GetComponent<PlayerControl>().soldierCollect.soldiers.Add(prefabHologramSoldier);
                other.GetComponent<PlayerControl>().soldierCollect.healtInit(soldierPower);
                other.GetComponent<PlayerControl>().setCam();
                //
                kurdele.material = greenMat;
                hologramGround.material = greenMat;
                }
                else
                {
                    GetComponent<Collider>().enabled = false;
                    other.GetComponent<PlayerControl>().currentBehaviour = PlayerControl.States.backward;
                    other.GetComponent<PlayerControl>().backSpeed = 10;
                    hologramSoldier.GetComponent<Ragdoll>().RagdollActivate(true);
                }
            }
        }
    }
