using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ActionController : MonoBehaviour , IPointerClickHandler{

	// Use this for initialization

	public Dictionary<string, string> partModels;
	public Dictionary<string, string> textures;


	void Start () {
		partModels = new Dictionary<string, string>(); 
		textures = new Dictionary<string, string> ();
	}
	
	// Update is called once per frame
	void Update () {
       // Debug.Log("ActionController " + gameObject.name);
	}

	public void OnPointerClick(UnityEngine.EventSystems.PointerEventData even){
            Debug.Log("Action Controller clicked");
		if (even.button == PointerEventData.InputButton.Left) {
			if (tag == "Texture") {
				OnClickTextureImage (name);
			} else if(tag == "Model") {
				OnClickModelImage (name);
			}
		}
			
	}

	public void OnClickTextureImage(string textureName){

		print ("OnClickTextureImage calisti");
		//MeshRenderer meshRenderer = LevelManager.selectedObject.GetComponent<MeshRenderer> ();
		//Material material = meshRenderer.material;

		Material material = getMaterialOfObject (LevelManager.selectedObject);
		Texture texture = Resources.Load<Texture> ("Textures/" + extractPathFromName(LevelManager.selectedObject.name) + "/" + textureName);

		print (LevelManager.selectedObject.name);
		print ("texture name is " + texture.name);
		print ("material name is " + material.name);

		material.SetTexture("Albedo", texture);
		material.mainTexture = texture;

		//print (material.GetTexture ("Albedo").name);
	}

	public void OnClickModelImage(string modelName){

		print ("OnClickModelImage calisti");
		//MeshRenderer meshRenderer = LevelManager.selectedObject.GetComponent<MeshRenderer> ();
		//Material material = meshRenderer.material;
		GameObject parentObject = getParent(LevelManager.selectedObject);
		string parentName = parentObject.name;
		string modelPath = "Models/" + extractPathFromName (parentName + "_" + LevelManager.selectedObject.name) + "/" + modelName;

		if (LevelManager.selectedObject.name == "sofa_seats") {
			
			GameObject newModel = LevelManager.instance.loadModel (modelPath);
			newModel.name = modelName;
			setTransformation (newModel);
			moveMaterials (parentObject, newModel);
			movePartModel (newModel);
			Destroy(parentObject);
			LevelManager.selectedObject = newModel;
		} else {
            if(!partModels.ContainsValue(LevelManager.selectedObject.name))
			    partModels.Add (LevelManager.selectedObject.name, modelName);
            Mesh model = Resources.Load<Mesh> (modelPath);
			LevelManager.selectedObject.GetComponent<MeshFilter> ().mesh = model;
		}
		//print (material.GetTexture ("Albedo").name);
	}

	private void setTransformation(GameObject gameObject){
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localPosition = Vector3.zero;
		if (gameObject.name == "sofa_montel_alborg") {
			print ("y setlendi");
			gameObject.transform.localPosition += Vector3.up * 30;
		}
	}

	private void moveMaterials(GameObject fromMaterials, GameObject toMaterials){
		for (int i = 0; i < toMaterials.transform.childCount; i++) {
			GameObject toObject = toMaterials.transform.GetChild (i).gameObject;
			for (int j = 0; j < fromMaterials.transform.childCount; j++) {
				GameObject fromObject = toMaterials.transform.GetChild (j).gameObject;
				if (toObject.name == fromObject.name) {
					//toObject.GetComponent<MeshRenderer> ().material.CopyPropertiesFromMaterial (fromMaterials.GetComponent<MeshRenderer> ().material);	
					break;
				}
			}
		}
	}

	private void movePartModel(GameObject gameObject){
		string modelPath = null;
		GameObject childeObject = null;
		for (int i = 0; i < gameObject.transform.childCount; i++) {
			childeObject = gameObject.transform.GetChild (i).gameObject;
			if( partModels.TryGetValue(childeObject.name,out modelPath)){
				loadPartModel (childeObject, gameObject.name, modelPath);
			}
		}
	}


	private void loadPartModel(GameObject gameObject, string parentModelName, string modelName){
		string modelPath = "Models/" + extractPathFromName (parentModelName + "_" + gameObject.name) + "/" + modelName;
		Mesh model = Resources.Load<Mesh> (modelPath);
		LevelManager.selectedObject.GetComponent<MeshFilter> ().mesh = model;
	}

	private Material getMaterialOfObject(GameObject gameObject){
		Material returnMaterial = null;
		try{
			returnMaterial = gameObject.GetComponent<MeshRenderer>().material;
		}catch(Exception ext){
			print (ext);
		}

		return returnMaterial;
	}

	private string extractPathFromName(string name){
		string[] paths = name.Split ('_');
		string path = paths [0];
		for (int i = 1; i < paths.Length; i++) {
			path += "/" + paths [i];
		}
		return path;
	}

	/**
	 * */
	private GameObject getParent(GameObject gameObject){
		if (gameObject == null) {
			return null;
		}
		if (gameObject.tag == "root") {
			return gameObject;
		}

		return getParent (gameObject.transform.parent.gameObject);
	}
}
