using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

public class ResetButton : MonoBehaviour, IPointerEnterHandler {


	public GameObject targetModel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(UnityEngine.EventSystems.PointerEventData even){
		if (even.button == PointerEventData.InputButton.Left) {
			targetModel.transform.localPosition = Vector3.zero;
			targetModel.transform.localRotation = Quaternion.identity;
		}
	}
}
