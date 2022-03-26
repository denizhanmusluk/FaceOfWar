using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Fighter : MonoBehaviour,IFightStart
{
    public bool attackActive = true;
    public bool alive = true;
    [SerializeField] public GameObject attackTarget;
    [SerializeField] float attackDistance;
    [SerializeField] float moveSpeed;
    [SerializeField] public float damage;
    [SerializeField] public float Maxhealth;
    public float currentHealth;
    public float maxSteerAngle = 10f;
    Slider healthBar;
   public targetInitialize targetInitialize;
    [SerializeField] ParticleSystem fire;
    Animator anim;
    [SerializeField] Transform powerCanvas;
    [SerializeField] public TextMeshProUGUI powerText;
    void Start()
    {
        powerCanvas.gameObject.SetActive(true);
        //powerCanvas.localScale = new Vector3(0, 0, 0);
        powerText.text = Maxhealth.ToString();
        FightManager.Instance.Add_fightStartObservers(this);
        anim = GetComponent<Animator>();
        //targetInitialize = transform.parent.parent.GetComponent<targetInitialize>();
        healthBar = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        healthBar.gameObject.SetActive(false);

        currentHealth = Maxhealth;
        healthBarSet();
        if (transform.parent.parent.GetComponent<EnemySelection>() == null)
        {
            StartCoroutine(setScalePowerCanvas(new Vector3(1, 1, 1)));
        }
    }
    IEnumerator setScalePowerCanvas(Vector3 targetScale)
    {
        float counter = 0;
        while (counter < 1)
        {
            counter += Time.deltaTime;
            powerCanvas.localScale = Vector3.MoveTowards(powerCanvas.transform.localScale, targetScale, 3 * Time.deltaTime);

            yield return null;
        }
        powerCanvas.localScale = targetScale;
    }
    public void firstMove()
    {
        StartCoroutine(firstPointMove());
    }
    IEnumerator firstPointMove()
    {
        while(transform.localPosition != Vector3.zero)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, 5 * Time.deltaTime);
            yield return null;
        }
        transform.localPosition = Vector3.zero;

    }
    public void fightStart()
    {
        transform.GetChild(0).GetChild(0).GetComponent<Slider>().gameObject.SetActive(true);
        powerCanvas.gameObject.SetActive(false);
        if (transform.parent.parent.GetComponent<EnemySelection>() == null)
        {
            StartCoroutine(setScalePowerCanvas(new Vector3(0, 0, 0)));
        }
        anim.SetTrigger("walk");
        GameEvents.fightEvent.AddListener(moveTarget);
        GameEvents.fightEvent.AddListener(ApplySteer);
    }
    public void healthBarSet()
    {
        healthBar.value = currentHealth / Maxhealth;
    }
    IEnumerator attack()
    {
        while (attackActive)
        {
            if (attackTarget.GetComponent<Fighter>().alive)
            {
                attackTarget.GetComponent<Fighter>().beDamage(damage);
                fire.Play();
                anim.SetTrigger("shoot");
                anim.SetBool("walking", false);
            }
            else
            {
                attackActive = false;
                setTarget();
                anim.SetBool("walking", true);

            }
            yield return new WaitForSeconds(0.3f);
        }
    }
    public void setTarget()
    {
        float distance = 50;
        for (int i = 0; i < targetInitialize.otherBase.soldier.Count; i++)
        {
            if (Vector3.Distance(targetInitialize.otherBase.soldier[i].transform.position, transform.position) < distance)
            {
                attackTarget = targetInitialize.otherBase.soldier[i];
            }
            distance = Vector3.Distance(targetInitialize.otherBase.soldier[i].transform.position, transform.position);
        }
        GameEvents.fightEvent.AddListener(moveTarget);
        GameEvents.fightEvent.AddListener(ApplySteer);
    }
    private void Update()
    {
        //moveTarget();
        //ApplySteer();
        //healthBar.transform.LookAt(battleCam.transform);
    }
    public void beDamage(float hitDamage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= hitDamage;
            healthBarSet();
        }
        else
        {
            currentHealth = 0;
            attackActive = false;
            alive = false;
            Destroy(healthBar.gameObject);
            targetInitialize.soldier.Remove(this.gameObject);
            GetComponent<Ragdoll>().RagdollActivate(true);
            targetInitialize.whoWon();
        }
        healthBarSet();
    }
    public void moveTarget()
    {
        if (Vector3.Distance(transform.position, attackTarget.transform.position) > attackDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, attackTarget.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            GameEvents.fightEvent.RemoveListener(moveTarget);
            GameEvents.fightEvent.RemoveListener(ApplySteer);
            attackActive = true;
            StartCoroutine(attack());
        }
    }

    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(attackTarget.transform.position);
        relativeVector /= relativeVector.magnitude;
        float newSteerY = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;

        transform.Rotate(0, newSteerY * Time.deltaTime * 100f, 0);

    }
}
