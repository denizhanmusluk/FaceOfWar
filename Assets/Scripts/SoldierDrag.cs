using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SoldierDrag : MonoBehaviour
{
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 firstPosition;
	public bool dragActive = true;
	public bool soldierDragging = true;
	[SerializeField] public GameObject warriourPrefab;
	[SerializeField] public TextMeshProUGUI powerText;
	Vector3 firstScale;
	Vector3 minScale;
	float firstY;
	[SerializeField] Transform powerCanvas;
	[SerializeField] GameObject baseSlots;
	private void Start()
	{
		baseSlots = GameObject.Find("mybase");
		powerCanvas.localScale = new Vector3(0, 0, 0);
		powerText.text = warriourPrefab.GetComponent<Fighter>().Maxhealth.ToString();
		firstY = transform.position.y;
		minScale = new Vector3(0.5f, 0.5f, 0.5f);
		firstScale = transform.localScale;
		firstPosition = Vector3.zero;
		//firstPosition = transform.position;
		//healthText.text = warriourPrefab.GetComponent<Fighter>().Maxhealth.ToString();
	}
	void OnMouseDown()
	{
		if (dragActive)
		{
			transform.parent.GetChild(0).GetChild(0).GetComponent<Light>().enabled = false;
			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
			StartCoroutine(setScale());
			StartCoroutine(setScalePowerCanvas(new Vector3(1, 1, 1)));
		}
	}

	void OnMouseDrag()
	{
		if (soldierDragging)
		{
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y * 1.2f, screenPoint.z);
			transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
			transform.GetComponent<illusion>().tweenStop();
			transform.position = new Vector3(transform.position.x, firstY, transform.position.z);
	
		}
	}
	private void OnMouseUp()
	{
		dragActive = false;
		if (soldierDragging)
		{
			moveFirst();

		}
		checkTargetSlot();

	}
	public void moveFirst()
	{
		StartCoroutine(moveToFirstPoint());
		StartCoroutine(setScalePowerCanvas(new Vector3(0, 0, 0)));
		transform.parent.GetChild(0).GetChild(0).GetComponent<Light>().enabled = true;
	}
	IEnumerator moveToFirstPoint()
	{
		while (Vector3.Distance(firstPosition, transform.localPosition) > 0.1f)
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, firstPosition, 10 * Time.deltaTime);
			transform.localScale = Vector3.MoveTowards(transform.localScale, firstScale, 15 * Time.deltaTime);
			//powerCanvas.localScale = Vector3.MoveTowards(powerCanvas.transform.localScale, new Vector3(0,0,0),5* Time.deltaTime);

			yield return null;
		}
		transform.localPosition = firstPosition;
		transform.localScale = firstScale;
		//powerCanvas.localScale = new Vector3(0, 0, 0);
		GetComponent<Collider>().enabled = true;
		dragActive = true;
		soldierDragging = true;
		transform.GetComponent<illusion>().tweenScale();

	}
	IEnumerator setScale()
	{
		while (transform.localScale.x > minScale.x && dragActive)
		{
			transform.localScale = Vector3.MoveTowards(transform.localScale, minScale, 5 * Time.deltaTime);
			//powerCanvas.localScale = Vector3.MoveTowards(powerCanvas.transform.localScale, new Vector3(1,1,1),5* Time.deltaTime);

			yield return null;
		}
		//powerCanvas.localScale = new Vector3(1, 1, 1);
	}
	IEnumerator setScalePowerCanvas(Vector3 targetScale)
	{
		float counter = 0;
		while (counter < 1)
		{
			counter += Time.deltaTime;
			powerCanvas.localScale = Vector3.MoveTowards(powerCanvas.transform.localScale, targetScale, 3 * Time.deltaTime);
			transform.parent.GetChild(0).transform.localScale = Vector3.MoveTowards(transform.parent.GetChild(0).transform.localScale, new Vector3(1, 1, 1) - targetScale, 10 * Time.deltaTime);
			transform.parent.GetChild(1).transform.localScale = Vector3.MoveTowards(transform.parent.GetChild(1).transform.localScale, new Vector3(1, 1, 1) - targetScale, 10 * Time.deltaTime);
			yield return null;
		}
		powerCanvas.localScale = targetScale;
		transform.parent.GetChild(0).transform.localScale = new Vector3(1, 1, 1) - targetScale;
		transform.parent.GetChild(1).transform.localScale = new Vector3(1, 1, 1) - targetScale;
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "point")
		{
			if (Input.GetMouseButtonUp(0))
			{
				//other.GetComponent<CapsuleCollider>().enabled = false;
				//other.GetComponent<BoxCollider>().enabled = false;
				//soldierDragging = false;
				//GameObject war = Instantiate(warriourPrefab, transform.position, Quaternion.identity);
				//war.transform.parent = other.transform;
				//war.transform.parent.parent.GetComponent<targetInitialize>().soldier.Add(war);
				//war.transform.parent.parent.GetComponent<targetInitialize>().isItFull();
				//war.GetComponent<Fighter>().targetInitialize = war.transform.parent.parent.GetComponent<targetInitialize>();
				//war.GetComponent<Fighter>().firstMove();
				//war.GetComponent<reverseDragSoldier>().transparentSoldier = gameObject;
				//gameObject.SetActive(false);
				//other.GetComponent<PowerCompare>().matSet();
				//transform.parent.parent.GetComponent<BaseSlots>().slots.Remove(transform.parent.gameObject);
				//transform.parent.GetComponent<slot>().active = false;
				//transform.parent.parent.GetComponent<BaseSlots>().listSet();
			}
		}
	}
	void checkTargetSlot()
	{
		Debug.Log("deneme2");

		if (!dragActive)
		{
			Debug.Log("deneme3");

			float distance = 5;
			GameObject targetPoint;

			for (int i = 0; i < baseSlots.transform.childCount; i++)
			{
				if (distance > Vector3.Distance(transform.position, baseSlots.transform.GetChild(i).transform.position) && baseSlots.transform.GetChild(i).GetComponent<PowerCompare>().sloatActive)
                {
					distance = Vector3.Distance(transform.position, baseSlots.transform.GetChild(i).transform.position);
					targetPoint = baseSlots.transform.GetChild(i).gameObject;
	
			Debug.Log("deneme");

			targetPoint.GetComponent<CapsuleCollider>().enabled = false;
			targetPoint.GetComponent<BoxCollider>().enabled = false;
			targetPoint.GetComponent<PowerCompare>().sloatActive = false;

			soldierDragging = false;
			GameObject war = Instantiate(warriourPrefab, transform.position, Quaternion.identity);
			war.transform.parent = targetPoint.transform;
			war.transform.parent.parent.GetComponent<targetInitialize>().soldier.Add(war);
			war.transform.parent.parent.GetComponent<targetInitialize>().isItFull();
			war.GetComponent<Fighter>().targetInitialize = war.transform.parent.parent.GetComponent<targetInitialize>();
			war.GetComponent<Fighter>().firstMove();
			war.GetComponent<reverseDragSoldier>().transparentSoldier = gameObject;
			gameObject.SetActive(false);
			targetPoint.GetComponent<PowerCompare>().matSet();
			transform.parent.parent.GetComponent<BaseSlots>().slots.Remove(transform.parent.gameObject);
			transform.parent.GetComponent<slot>().active = false;
			transform.parent.parent.GetComponent<BaseSlots>().listSet();
				}

			}
		}
	}
}
