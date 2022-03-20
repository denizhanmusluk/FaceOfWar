using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    
    void Start()
    {
        FightManager.Instance.Add_fightStartObservers(this);
        anim = GetComponent<Animator>();
        //targetInitialize = transform.parent.parent.GetComponent<targetInitialize>();
        healthBar = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        currentHealth = Maxhealth;
        healthBarSet();

 
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
