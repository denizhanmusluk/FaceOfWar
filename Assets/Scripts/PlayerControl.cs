using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerControl : MonoBehaviour, IStartGameObserver
{

    private float m_previousX;
    private float dX;
    private float dX_Sum;
    [Range(0.0f, 10.0f)]
    [SerializeField] float Controlsensivity;

    //[Range(0.0f, 50.0f)]
    //[SerializeField] float Steeringsensivity;

    //[Range(0.0f, 50.0f)]
    //[SerializeField] public float HorizontalSens;

    public float acceleration = 15;
    public float backSpeed = 10;
    //public BullControl cn;
    [SerializeField] public float steeringSpeed = 180;

    public float Xmove, Steer, Speed;
    [SerializeField] public float bounding;
    [SerializeField] GameObject playerParents;
    [SerializeField] public GameObject moneyTarget;
    [SerializeField] public SoldierCollecting soldierCollect;
    //[SerializeField] CinemachineVirtualCamera cam;
    Animator anim;
    public enum States { idle, forward, backward }
    public States currentBehaviour;
    private void Start()
    {
        currentBehaviour = States.idle;
        GameManager.Instance.Add_StartObserver(this);
        anim = GetComponent<Animator>();
    }
    public void StartGame()
    {
        currentBehaviour = States.forward;
        anim.SetTrigger("walk");
    }
    void forward()
    {
        transform.parent.transform.Translate(transform.parent.transform.forward * Time.deltaTime * acceleration);
    }
    void backward()
    {
        backSpeed -= 10 * Time.deltaTime;
        transform.parent.transform.Translate(-transform.parent.transform.forward * Time.deltaTime * backSpeed);
        if (backSpeed <= 0)
        {
            backSpeed = 0;
            currentBehaviour = States.forward;
        }
    }
    private void Awake()
    {

    }
    public void moneyCollcet()
    {

    }

    private void Update()
    {
        switch (currentBehaviour)
        {
            case States.idle:
                {
                }
                break;
            case States.forward:
                {
                    forward();
                    gameUpdate();
                }
                break;
            case States.backward:
                {
                    backward();
                }
                break;


        }
        //if (Globals.isGameActive && !Globals.finish)
        //{
        //    gameUpdate();
        //}

    }

    private void gameUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_previousX = Input.mousePosition.x;
            dX = 0f;
            dX_Sum = 0f;
        }
        if (Input.GetMouseButton(0))
        {
            dX = (Input.mousePosition.x - m_previousX) / 10f;
            dX_Sum += dX;
            m_previousX = Input.mousePosition.x;
        }
        if (Input.GetMouseButtonUp(0))
        {
            dX_Sum = 0f;
            dX = 0f;
        }
        Xmove = Controlsensivity * dX / (Time.deltaTime * 25);
        Move(Xmove, Steer, acceleration);
    }
    public void moveReset()
    {
        Xmove = 0;
        Steer = 0;
    }
    public void Move(float _swipe, float _steering, float _speed)
    {
        if (_swipe > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(bounding, transform.position.y, transform.position.z), Time.deltaTime * Mathf.Abs(_swipe));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, 45, transform.eulerAngles.z), steeringSpeed * Time.deltaTime);
        }
        if (_swipe < 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-bounding, transform.position.y, transform.position.z), Time.deltaTime * Mathf.Abs(_swipe));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, -45, transform.eulerAngles.z), steeringSpeed * Time.deltaTime);
        }
        if (_swipe == 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, 0, transform.eulerAngles.z), 2 * steeringSpeed * Time.deltaTime);
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "money")
        {
            //other.gameObject.GetComponent<Player>().MoneyUpdate(30);
            LevelScore.Instance.MoneyUpdate(other.transform.GetComponent<MoneyCollecting>().moneyValue);
            //Score.Instance.scoreUp();
            StartCoroutine(targetMotion(other.gameObject));
        }
    }

    IEnumerator targetMotion(GameObject money)
    {
        while (Vector3.Distance(money.transform.position, moneyTarget.transform.position) > 0.3f)
        {
            money.transform.position = Vector3.MoveTowards(money.transform.position, moneyTarget.transform.position, (2 / Vector3.Distance(money.transform.position, moneyTarget.transform.position)) * acceleration * Time.deltaTime);
            money.transform.localScale = Vector3.Lerp(money.transform.localScale, moneyTarget.transform.localScale, acceleration * 0.4f * Time.deltaTime);
            yield return null;
        }

        money.transform.parent = null;
        Destroy(money);
    }
}
