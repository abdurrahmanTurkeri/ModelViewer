using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


	public static LevelManager instance;


	private AssetBundleCreateRequest bundleRequest;
	public GameObject targetModel;

	public static string modelName;
	public static GameObject selectedObject;

	private void Start()
	{
		instance = this;
		clearRoot ();
		if (modelName != null) {
			GameObject go = loadModel ("Products/" + modelName + "/" + modelName);
			go.name = modelName;
		}

	}

	public GameObject loadModel(string path){
		print ("model to load path is " + path); 
		Object modelToLoad = Resources.Load (path);
		GameObject model = null;
		if (modelToLoad != null) {
			
			model = (GameObject)Instantiate(Resources.Load(path));
			if (targetModel != null) {
				model.transform.parent = targetModel.transform;
			}
			model.transform.localPosition = Vector3.zero;
			model.transform.localRotation = Quaternion.identity;
			print ("modelName is " + modelName);
			if (modelName == "sofa_montel_alborg") {
				print ("y setlendi");
				model.transform.localPosition += Vector3.up * 30;
			}
			model.tag = "root";
		}

		if (targetModel != null) {
			for (int i = 0; i < targetModel.transform.childCount; i++) {
				addTagToTargetModel (targetModel.transform.GetChild(i));
			}
		}
		return model;
	}

	private void clearRoot(){
		if (targetModel != null) {
			for (int i = 0; i < targetModel.transform.childCount; i++) {
				if (targetModel.transform.GetChild (i).gameObject.tag == "root") {
					Destroy (targetModel.transform.GetChild (i).gameObject);
				}
			}
		}
	}

	public void addTagToTargetModel(Transform trans){
		for (int i = 0; i < trans.childCount; i++) {
			Transform transformChild = trans.GetChild (i);
			transformChild.tag = "select";
			//addTagToTargetModel (trans);
			transformChild.gameObject.AddComponent<MeshCollider>();
			//print (transformChild.name);
		}
	}

	private void Update()
	{

	}
		

}
