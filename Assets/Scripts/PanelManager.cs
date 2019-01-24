using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{

	public GameObject panel;
	public GameObject imageButton;
	public string contentType = "texture";

	private  static PanelManager panelManager;

	public static PanelManager instance {
		get 
		{
			return panelManager;
		}
	}


	// Use this for initialization
	void Start ()
	{
		panelManager = this;
		contentType = "Texture";
		SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			bool mouseInPanel = CheckMouseInPanel ();
			bool mouseHitObject = CheckMouseHitAnObject ();

            if (mouseInPanel)
            { 
                SetActive(true);
            }else if( mouseHitObject) {
				SetActive (true);
		    	PopulateContent ();
			} else {
				SetActive (false);
			}
		}
	}

	private bool isActive = true;

	public void SetActive(bool b){
		panel.SetActive (b);
		isActive = b;
	}

	public bool IsActive(){
		return isActive;
	}
		
	public void SetContentType(string type){
		contentType = type;
		PopulateContent ();
	}

	private bool CheckMouseInPanel(){

		if (!IsActive()) {
			return false;
		}

		RectTransform rectTransform = panel.GetComponent<RectTransform>();
		Vector3 transformedPoint = rectTransform.InverseTransformPoint (Input.mousePosition);

		if (transformedPoint.x > 0 && transformedPoint.x < rectTransform.rect.width) {
			if (transformedPoint.y > 0 && transformedPoint.y < rectTransform.rect.height) {
				return true;
			}
		}
		return false;
	}

	private bool CheckMouseHitAnObject(){
		//PanelManager.getInstance().SetActive (false);
		RaycastHit hitInfo = new RaycastHit();
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo)) {
			//print ("It's working");
			print (hitInfo.transform.gameObject.name.ToString ());
			if (hitInfo.transform.tag == "select") {
				//aslinda bu burada set edilmemeli
				LevelManager.selectedObject = hitInfo.transform.gameObject;
				return true;
			}
		}

		return false;
	}


	private void PopulateContent(){
		if (imageButton == null)
			return;

		ClearContent ();

		string path = extractPathFromName (LevelManager.selectedObject.name);
		path = "Sprites/" + contentType + "/" + path;
		print ("path is " + path);
		Object[] textures = Resources.LoadAll(path, typeof(Sprite));

		int i = 0;
		foreach (var t in textures)
		{
			GameObject imagebt = Instantiate (imageButton, panel.transform.GetChild (0) ); 
			//imagebt.GetComponent<GUITexture>().texture = (Texture)t;
			Vector3 diffPos = new Vector3((i * 250), 0 ,0);
			imagebt.GetComponent<RectTransform> ().position += diffPos;
			imagebt.GetComponent<UnityEngine.UI.Image> ().sprite = ((Sprite)t);
			imagebt.name = t.name;
			imagebt.tag = contentType;
			i++;
		}
	}
		
	private void ClearContent(){
		Transform contentTranform = panel.transform.GetChild (0);
		for (int i = 0; i < contentTranform.childCount; i++) {
			Destroy (contentTranform.GetChild (i).gameObject);
			//print ("Destroyed");
		}
		contentTranform.DetachChildren ();
	}

	private string extractPathFromName(string name){
		string[] paths = name.Split ('_');
		string path = paths [0];
		for (int i = 1; i < paths.Length; i++) {
			path += "/" + paths [i];
		}
		return path;
	}

}

