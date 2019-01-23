using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class BigImageButton : MonoBehaviour, IPointerClickHandler {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerClick(UnityEngine.EventSystems.PointerEventData even){
		print ("event calisti");
		if (even.button == PointerEventData.InputButton.Left) {
			LevelManager.modelName = name;
			SceneManager.LoadScene (1);
		}

	}
}
