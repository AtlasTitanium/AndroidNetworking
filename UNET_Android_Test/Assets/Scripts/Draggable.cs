using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler {
	private Vector3 hitPoint;
	public LayerMask layerMask;
	public GameObject Child;
	public GameObject CorrectPiece;
	public bool isRight = false;
	public void OnDrag(PointerEventData eventData){
		if(!isRight){
			this.GetComponent<Collider>().enabled = false;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 50f, layerMask)) 
			{
				hitPoint = hit.point;
				transform.position = hitPoint;			
			}  
		} 
	}

	public void OnEndDrag(PointerEventData eventData){
		this.GetComponent<Collider>().enabled = true;
		transform.position = hitPoint;
	}

	void OnTriggerStay(Collider other){
		if(other.gameObject.name == Child.name){
			gameObject.GetComponent<Image>().enabled = false;
			Child.SetActive(false);
			CorrectPiece.SetActive(true);
			isRight = true;
		} else {
			isRight = false;
		}
	}

	void OnTriggerExit(Collider other){
		isRight = false;
	}
}
