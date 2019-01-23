using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BackButton : MonoBehaviour, IPointerClickHandler  {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerClick(UnityEngine.EventSystems.PointerEventData even){
		if (even.button == PointerEventData.InputButton.Left) {
			LevelManager.modelName = null;
			SceneManager.LoadScene (0);
		}
	}
}
