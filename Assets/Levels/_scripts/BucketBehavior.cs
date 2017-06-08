using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Renderer [] renderers = this.GetComponentsInChildren<Renderer> ();
		foreach(Renderer r in renderers)
			if(!r.gameObject.tag.Equals("NumberLabel"))
				r.material.color = LevelManager.colors[LevelManager.buckets_instantiated];
		LevelManager.buckets_instantiated++;
	}

}
