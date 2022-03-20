using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SoldierDrag : MonoBehaviour
{
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 firstPosition;
	bool dragActive = true;
	bool soldierDragging = true;
	[SerializeField] public GameObject warriourPrefab;
	//[SerializeField] public TextMeshProUGUI healthText;
	private void Start()
    {
		firstPosition = transform.position;
		//healthText.text = warriourPrefab.GetComponent<Fighter>().Maxhealth.ToString();
    }
	void OnMouseDown()
	{
		if (dragActive)
		{
			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}
	}

	void OnMouseDrag()
	{
		if (soldierDragging)
		{
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
			transform.GetComponent<illusion>().tweenStop();
		}
	}
    private void OnMouseUp()
    {
		dragActive = false;
		if (soldierDragging)
		{
			transform.GetComponent<illusion>().tweenScale();
			StartCoroutine(moveToFirstPoint());
		}
    }
	IEnumerator moveToFirstPoint()
    {
		while( Vector3.Distance(firstPosition, transform.position) > 0.1f)
        {
			transform.position = Vector3.MoveTowards(transform.position, firstPosition, 20 * Time.deltaTime);
			yield return null;
        }
		dragActive = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "point")
        {
			Destroy(other.GetComponent<Collider>());
			soldierDragging = false;
			GameObject war = Instantiate(warriourPrefab, transform.position, Quaternion.identity);
			war.transform.parent = other.transform;
			war.transform.parent.parent.GetComponent<targetInitialize>().soldier.Add(war);
			war.transform.parent.parent.GetComponent<targetInitialize>().isItFull();
			war.GetComponent<Fighter>().targetInitialize = war.transform.parent.parent.GetComponent<targetInitialize>();
			war.GetComponent<Fighter>().firstMove();
			Destroy(gameObject);
		}
	}
}
