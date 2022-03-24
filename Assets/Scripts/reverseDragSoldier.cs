using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reverseDragSoldier : MonoBehaviour
{
	private Vector3 screenPoint;
	private Vector3 offset;
	bool dragActive = true;
	bool soldierDragging = true;
	public GameObject transparentSoldier;
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("soldier");

			Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit raycastHit;
			if (Physics.Raycast(raycast, out raycastHit))
			{
				if (raycastHit.collider.tag == "soldier")
				{
					transform.parent.GetComponent<Collider>().enabled = true;

					transparentSoldier.SetActive(true);
					Destroy(gameObject);
					dragActive = false;
					soldierDragging = false;

					transform.parent.parent.GetComponent<targetInitialize>().soldier.Remove(gameObject);
				
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
