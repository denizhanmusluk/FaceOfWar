using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reverseDragSoldier : MonoBehaviour
{
	private Vector3 screenPoint;
	private Vector3 offset;
public	bool drag = true;
	public GameObject transparentSoldier;
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && drag)
		{

			Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(raycast, out raycastHit))
			{
				if (raycastHit.collider.tag == "soldier")
				{
					Debug.Log("soldier");
					raycastHit.collider.GetComponent<reverseDragSoldier>().transparentSoldier.SetActive(true);
					raycastHit.collider.GetComponent<reverseDragSoldier>().transparentSoldier.GetComponent<Collider>().enabled = false;

					//transparentSoldier.GetComponent<SoldierDrag>().soldierDragging = true;
					raycastHit.collider.GetComponent<reverseDragSoldier>().transparentSoldier.GetComponent<SoldierDrag>().moveFirst();
					raycastHit.collider.GetComponent<reverseDragSoldier>().transparentSoldier.transform.parent.GetComponent<slot>().active = true;
				//raycastHit.collider.GetComponent<reverseDragSoldier>().transparentSoldier.transform.parent.parent.GetComponent<BaseSlots>().slots.Add(raycastHit.collider.GetComponent<reverseDragSoldier>().transparentSoldier.transform.parent.gameObject);
					raycastHit.collider.GetComponent<reverseDragSoldier>().transparentSoldier.transform.parent.parent.GetComponent<BaseSlots>().listSet();
					//raycastHit.collider.GetComponent<reverseDragSoldier>().transparentSoldier.transform.parent.parent.GetComponent<BaseSlots>().positionSet();

					//dragActive = false;
					raycastHit.collider.GetComponent<reverseDragSoldier>().drag = false;

					raycastHit.collider.transform.parent.GetComponent<CapsuleCollider>().enabled = true;
					raycastHit.collider.transform.parent.GetComponent<BoxCollider>().enabled = true;
					raycastHit.collider.transform.parent.GetComponent<PowerCompare>().firstMatInit();

					raycastHit.collider.transform.parent.parent.GetComponent<targetInitialize>().soldier.Remove(raycastHit.collider.gameObject);

					raycastHit.collider.transform.parent = null;
					FightManager.Instance.Remove_fightStartObservers(this.GetComponent<Fighter>());

					Destroy(raycastHit.collider.gameObject,0.1f);

			

					//transform.parent.parent.GetComponent<targetInitialize>().soldier = new List<GameObject>(transform.parent.parent.GetComponent<targetInitialize>().soldier.Count - 1);
					//transform.parent.parent.GetComponent<targetInitialize>().soldier = new List<GameObject>(0);
				
				}
			}
		}
		//if (dragActive)
		//{
		//	screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		//	offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		//}
	}

	//void OnMouseDrag()
	//{
	//	if (soldierDragging)
	//	{
	//		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y * 1.2f, screenPoint.z);
	//		transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
	//		if(Vector3.Distance(transform.localPosition, Vector3.zero)> 1)
	//           {
	//			dragActive = false;
	//			soldierDragging = false;

	//			transform.parent.parent.GetComponent<targetInitialize>().soldier.Remove(gameObject);
	//			transparentSoldier.SetActive(true);
	//			Destroy(gameObject);

	//		}
	//	}
	//}
	//private void OnMouseUp()
	//{

	//	if (soldierDragging)
	//	{
	//		GetComponent<Fighter>().firstMove();


	//	}
	//}
}
