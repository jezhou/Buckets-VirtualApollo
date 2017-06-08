using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatBehavior : MonoBehaviour
{
	//Private variables.
	private float originalHeight;

	//Public variables.
	public float floatStrength = 0.1f;
	public float floatSpeed = 3f;

	// Use this for initialization
	void Start()
	{
		//Set the original height to oscillate from.
		originalHeight = this.transform.position.y;
	}

	// Update is called once per frame
	void Update()
	{
		//Allows the object to float in place in respect to it's original y position.
		float deltaHeight = originalHeight + ((float)Mathf.Sin (Time.time * floatSpeed) * floatStrength);
		transform.position = new Vector3(transform.position.x, deltaHeight, transform.position.z);
	}
}
