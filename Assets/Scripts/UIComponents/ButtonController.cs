using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

public class ButtonController : MonoBehaviour, IPointerEnterHandler {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OnPointerEnter(UnityEngine.EventSystems.PointerEventData even){
		if (even.button == PointerEventData.InputButton.Left) {
			PanelManager.instance.SetContentType (gameObject.name);
		}
	}
}
