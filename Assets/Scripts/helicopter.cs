using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helicopter : MonoBehaviour
{
    public Transform followTarget;
    float moveSpeed;
    bool isCollision = false;
    //Animator anim;
    private void Start()
    {
        //anim = GetComponent<Animator>();
        StartCoroutine(jumping());
    }
    IEnumerator jumping()
    {
        transform.parent = followTarget.transform.parent;
        Vector3 firstPos = transform.localPosition;

        float counter = 0f;

        float posX = followTarget.GetComponent<PlayerControl>().bounding;
        float posY = 0;
        while (counter < Mathf.PI)
        {
            counter += 5 * Time.deltaTime;
            posY = Mathf.Sin(counter);

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, firstPos.y + posY * 2, -3), 10 * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), 400 * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(following());
        //anim.SetTrigger("walk");
    }
    IEnumerator following()
    {
        moveSpeed = followTarget.GetComponent<PlayerControl>().acceleration;
        float xPosDelta = transform.position.x - followTarget.position.x;
        yield return new WaitForSeconds(2f);
        while (Globals.followActive && !isCollision)
        {
            //transform.Translate(transform.forward * Time.deltaTime * moveSpeed);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(followTarget.position.x + xPosDelta / 3, transform.position.y, transform.position.z), 0.15f * Time.deltaTime);
            //transform.position =new Vector3(followTarget.position.x + xPosDelta, transform.position.y, transform.position.z);
            yield return null;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "soldier")
        {
            isCollision = true;
            Vector3 direction = (transform.position - other.transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(direction.x, 0, direction.z / 5), 0.05f * Time.deltaTime);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "soldier")
        {
            isCollision = false;
            StartCoroutine(following());
        }
    }
}
