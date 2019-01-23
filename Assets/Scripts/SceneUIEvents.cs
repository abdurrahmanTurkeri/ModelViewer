using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneUIEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClickTextureImage(string textureName){
	
		print ("OnClickTextureImage calisti");
		//MeshRenderer meshRenderer = LevelManager.selectedObject.GetComponent<MeshRenderer> ();
		//Material material = meshRenderer.material;

		Material material = getMaterialOfObject (LevelManager.selectedObject);
		Texture texture = Resources.Load<Texture> ("Textures/" + textureName);



		print (LevelManager.selectedObject.name);
		print ("texture name is " + texture.name);
		print ("material name is " + material.name);

		material.SetTexture("Albedo", texture);
		material.mainTexture = texture;

		//print (material.GetTexture ("Albedo").name);
	}

	private Material getMaterialOfObject(GameObject gameObject){
		Material returnMaterial = null;
		try{
			returnMaterial = gameObject.GetComponent<MeshRenderer>().material;
		}catch(Exception ext){
			print (ext);
		}

		//if (returnMaterial == null) {
		//returnMaterial = Resources.Load<Material> ("Materials/" + "material_pbr");
		//gameObject.GetComponent<MeshRenderer> ().material = returnMaterial;
		//}

		return returnMaterial;
	}
}
