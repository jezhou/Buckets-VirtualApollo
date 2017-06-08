using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour {
	private int colorIndex;
	private int maxColors;
	private Color[] colorArray;

	// Use this for initialization
	void Start () {
		colorArray = new Color[] {Color.red, Color.green, Color.blue};
		GetComponent<Renderer>().material.color = colorArray[Random.Range(0, 3)];
	}
}
