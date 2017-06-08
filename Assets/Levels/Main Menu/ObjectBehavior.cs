using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectBehavior : MonoBehaviour {

	//Private Variables.
	private float timer;
	private bool focused;

	//Public Variables.
	public float gazeTime = 2.0f;
	public bool hasFocus = true; //Bool to determine if gaze works on this object.

	// Use this for initialization
	void Start () {
		timer = 0.0f;
		focused = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		//If the object is focused on...
		if (focused) {
			GazeFocus();
		}
	}

	// Function to handle Gaze for a certain amount of time. For now, we set gazeTime to be 2.0f.
	private void GazeFocus() {
		timer += Time.deltaTime;//Increment the timer.
		if (timer >= gazeTime) { //After the timer reaches gazeTime.
			//Execute click!
			ExecuteEvents.Execute(gameObject, new PointerEventData (EventSystem.current), ExecuteEvents.pointerDownHandler);
			//Reset or Disable variables here.
			timer = 0.0f;
			focused = false;
		}
	}

	// Object performs the action. This might be virtual so other objects can abstract from this class later on.
	private void Action() {
		Debug.Log ("Clicked");
	}

	// Function to call when event triggered is PointerEnter.
	public void PointerEnter(){
		//Sets focus to true only when focus action is enabled.
		if (hasFocus) focused = true;
	}

	// Function to call when event triggered is PointerExit.
	public void PointerExit() {
		//Sets focus to false only when focus action is enabled.
		if (hasFocus) focused = false;
	}

	// Function to call when event triggered is PointerDown or PointerClick.
	public void PointerDown() {
		this.Action();
	}
		
}
