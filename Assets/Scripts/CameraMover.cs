using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMover : MonoBehaviour {

	public GameObject targetObject;

	private float speed = 10;

	public Vector2 firstZeroTouch = Vector2.zero;
	public Vector2 firstOneTouch = Vector2.zero;
	public bool firstTouch = true;

	//Make sure to attach these Buttons in the Inspector
	public Button redButton, greenButton, blueButton;
	public Material[] materials;


	public Vector3 mousePositionOne;
	private Vector3 mousePositionOnePrev;
	public Vector3 mousePositionZero;
	public float angle = 0;

	public float cosResult;

	void Start()
	{
		//Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
		/*redButton.onClick.AddListener(() => ButtonClicked(0));
		greenButton.onClick.AddListener(() => ButtonClicked(1));
		blueButton.onClick.AddListener(() => ButtonClicked(2));*/

	}

	void ButtonClicked(int buttonNo)
	{
		//Output this to console when the Button3 is clicked
		Debug.Log("Button clicked = " + buttonNo);
		targetObject.GetComponentInChildren<Renderer> ().material = materials [buttonNo];
			
	}




	
	// Update is called once per frame
	void Update () {
			// If there are two touches on the device...

		checkRotateTouch ();

		checkZoomTouch ();

		checkMoveTouch ();

		// If there are two touches on the device...
		/*if (Input.touchCount == 2) {
				
			Touch touchZero = Input.GetTouch (0);
			Touch touchOne = Input.GetTouch (1);

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			Vector2 prevVector = touchZeroPrevPos - touchOnePrevPos;
			Vector2 currenVector = touchZero.position - touchOne.position;

			float currentAngle = Mathf.Atan2 (currenVector.x, currenVector.y);
			float oldAngle = Mathf.Atan2 (prevVector.x, prevVector.y);

			float angleDiff = (currentAngle - oldAngle) * 100;

			if (Mathf.Abs (angleDiff) > 0.3f) {
				targetObject.transform.Rotate (0, angleDiff , 0);
			}

		}else if (Input.touchCount == 1){
			
		}*/

	}




	private Vector3 prevMosePosition = Vector3.zero;
	private Vector2 prevTouchPosition = Vector2.zero;
	private bool firsTouch = true;
	public float rotationSpeed = 1f;
 	
	private void checkRotateTouch(){
		// If there are two touches on the device...
		if (Input.GetMouseButtonUp (0)) {
			prevMosePosition = Vector3.zero;
		}

		if (Input.touchCount == 0) {
			firsTouch = true;
		}

		if (Input.touchCount == 1 || (Input.GetMouseButton(0) && !Input.GetMouseButton(1))) {

			Vector2 rotateVector = Vector2.zero;
			if (Input.GetMouseButton (0)) {
				Vector3 currentMousePosition = Input.mousePosition;
				Vector3 deltaPosition = Vector3.zero;
				if (prevMosePosition != Vector3.zero) {
					deltaPosition = currentMousePosition - prevMosePosition;
				}
				prevMosePosition = currentMousePosition;
				//rotateVector = new Vector2 (Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
				rotateVector = new Vector2 (deltaPosition.x, deltaPosition.y);
			}else if(Input.touchCount == 1){
				if (firsTouch) {
					firsTouch = false;
					prevTouchPosition = Input.GetTouch (0).position;
				}

				rotateVector = Input.GetTouch (0).position - prevTouchPosition;
				prevTouchPosition = Input.GetTouch (0).position;

			}
				
			float firstDistance = (targetObject.transform.position - transform.position).magnitude;

			transform.position -= transform.up * rotationSpeed * rotateVector.y;
			transform.position -= transform.right * rotationSpeed * rotateVector.x;

			Vector3 newforward = (targetObject.transform.position - transform.position).normalized;
			transform.position = targetObject.transform.position - newforward * firstDistance;

			//Vector3 newUp = Vector3.Cross (newforward, transform.right);
			transform.rotation = Quaternion.LookRotation (newforward);

		}else if (Input.touchCount == 1){

		}


	
	}

	public float zoomSpeed = 0.1f;

	private void checkZoomTouch(){

		if (Input.touchCount == 2) {

			Touch touchZero = Input.GetTouch (0);
			Touch touchOne = Input.GetTouch (1);

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;


			Vector2 prevVector = firstZeroTouch - firstOneTouch;
			Vector2 currenVector = touchZero.position - touchOne.position;


			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			transform.position += transform.forward * deltaMagnitudeDiff * zoomSpeed * -1;
		}

	}

	public float moveSpeed = 0.01f;
	private Vector3 prevMouseButtonPosition = Vector3.zero;

	private void checkMoveTouch(){

		Vector3 moveVector = Vector3.zero;
		if (Input.touchCount == 2 || (Input.GetMouseButton (0) && Input.GetMouseButton (1))) {

			print ("giris ");
			if (Input.touchCount == 2) {

				print ("Input.touchCount 0 is " + Input.touchCount);
				Touch touchZero = Input.GetTouch (0);
				Touch touchOne = Input.GetTouch (1);

				// Find the position in the previous frame of each touch.
				float dot = Vector2.Dot(touchZero.deltaPosition.normalized, touchOne.deltaPosition.normalized);
				if (dot > 0 && 1 - dot < 0.01) {
					moveVector = new Vector3 (touchOne.deltaPosition.x, touchOne.deltaPosition.y, 0);
				}
			
			} else if (Input.GetMouseButton (0) && Input.GetMouseButton (1)) {
				Vector3 currentMouserButtonOnePosititon = Input.mousePosition;
				//print ("currentMouserButtonOnePosititon 0 is " + currentMouserButtonOnePosititon);
				//print ("prevMouseButtonPosition 0 is " + prevMouseButtonPosition);

				Vector3 deltaPosition = Vector3.zero;
				if (prevMouseButtonPosition != Vector3.zero) {
					deltaPosition = currentMouserButtonOnePosititon - prevMouseButtonPosition;
					//print ("deltaPosition 0 is " + deltaPosition);
				}
				prevMouseButtonPosition = currentMouserButtonOnePosititon;
				moveVector = deltaPosition;
			} 

			print ("cikis ");
			moveVector.y *= -1;
			transform.position += moveVector * moveSpeed;
		} else {
			prevMouseButtonPosition = Vector3.zero;
			//print ("prevMouseButtonPosition 0 sifirlandi ");
		}

	}
}
