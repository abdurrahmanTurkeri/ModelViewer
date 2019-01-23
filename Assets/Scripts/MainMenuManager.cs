using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

	public GameObject bigImageButton;
	public GameObject panel;
	// Use this for initialization
	void Start () {
		clearContent ();
		populateContent ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void populateContent(){
		if (bigImageButton == null)
			return;

		string path = "Sprites/Products/";
		Object[] textures = Resources.LoadAll(path, typeof(Sprite));

		int k = 0;
		foreach (var t in textures)
		{
			GameObject imagebt = Instantiate (bigImageButton, panel.transform ); 
			//imagebt.GetComponent<GUITexture>().texture = (Texture)t;
			int i = k / 2;
			int j = k % 2;
			Vector3 diffPos = new Vector3((i * 1250), (j * -1050) ,0);
			imagebt.GetComponent<RectTransform> ().position += diffPos;
			imagebt.GetComponent<UnityEngine.UI.Image> ().sprite = ((Sprite)t);
			imagebt.name = t.name;
			k++;
		}
	}

	private void clearContent(){
		Transform contentTranform = panel.transform;
		for (int i = 0; i < contentTranform.childCount; i++) {
			Destroy (contentTranform.GetChild (i).gameObject);
			//print ("Destroyed");
		}
		contentTranform.DetachChildren ();
	}
}
