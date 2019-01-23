using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;

public class FingerStart : MonoBehaviour {

	// Use this for initialization
	private RotateGestureRecognizer rotateGesture;
	private PanGestureRecognizer panGesture;

	void Start () {
		CreateRotateGesture();
		CreatePanGesture ();
	}

	private void PanGestureCallback(GestureRecognizer gesture)
	{
		if (gesture.State == GestureRecognizerState.Executing)
		{
			//DebugText("Panned, Location: {0}, {1}, Delta: {2}, {3}", gesture.FocusX, gesture.FocusY, gesture.DeltaX, gesture.DeltaY);
			float deltaX = panGesture.DeltaX / 25.0f;
			float deltaY = panGesture.DeltaY / 25.0f;
			Vector3 pos = transform.position;
			pos.x += deltaX;
			pos.y += deltaY;
			transform.position = pos;
		}
	}

	private void CreatePanGesture()
	{
		panGesture = new PanGestureRecognizer();
		panGesture.MinimumNumberOfTouchesToTrack = 2;
		panGesture.StateUpdated += PanGestureCallback;
		FingersScript.Instance.AddGesture(panGesture);
	}

	private void RotateGestureCallback(GestureRecognizer gesture)
	{
		if (gesture.State == GestureRecognizerState.Executing)
		{
			transform.Rotate(0.0f, rotateGesture.RotationRadiansDelta * Mathf.Rad2Deg, 0.0f);
		}
	}

	private void CreateRotateGesture()
	{
		rotateGesture = new RotateGestureRecognizer();
		rotateGesture.StateUpdated += RotateGestureCallback;
		FingersScript.Instance.AddGesture(rotateGesture);
	}
		
	// Update is called once per frame
	void Update () {
		
	}
}
