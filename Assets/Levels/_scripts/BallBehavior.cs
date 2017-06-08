using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour {

	private Rigidbody rb;
	private bool pickedUp;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		GetComponent<Renderer>().material.color = LevelManager.colors[Random.Range(0, LevelManager.colors.Length)];

		SetBouncinessAndMovement ();

		if (LevelManager.repeat_buckets) {
			Color c = GetComponent<Renderer>().material.color;
			ArrayList ar = (ArrayList)LevelManager.labels_map [c];
			object [] labels = ar.ToArray();

			int desired_label = 0;
			if (labels.Length == 1) {
				desired_label = (int)labels [0];
			} else {
				desired_label = Random.Range (0.0f, 1.0f) > 0.5f ? (int)labels [0] : (int)labels [1];
			}

			GetComponentInChildren<TextMesh> ().text = desired_label.ToString();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (pickedUp) {
			Vector3 direction = this.transform.position - Camera.main.transform.position;
			direction.Normalize ();
			this.transform.position = (direction * LevelManager.radius) + Camera.main.transform.position;
		}
	}

	void FixedUpdate () {
		if (pickedUp) {
			Quaternion q = Quaternion.identity * Quaternion.Inverse (Camera.main.transform.rotation);
			rb.MoveRotation (q);
		}
	}

	void SetBouncinessAndMovement() {			
		Component[] mesh_colliders = GetComponents<SphereCollider> ();

		switch (LevelManager.difficulty) {
		case "easy":
			foreach (SphereCollider mc in mesh_colliders) {
				mc.material.bounciness = 1.0f;
				mc.material.dynamicFriction = 0.0f;
				mc.material.staticFriction = 0.0f;
				mc.material.bounceCombine = PhysicMaterialCombine.Average;
				mc.material.frictionCombine = PhysicMaterialCombine.Average;
			}
			break;
		case "medium":
			foreach (SphereCollider mc in mesh_colliders) {
				mc.material.bounciness = 1.0f;
				mc.material.dynamicFriction = 0.25f;
				mc.material.staticFriction = 0.1f;
				mc.material.bounceCombine = PhysicMaterialCombine.Maximum;
				mc.material.frictionCombine = PhysicMaterialCombine.Minimum;
			}
			break;
		case "hard":
			foreach (SphereCollider mc in mesh_colliders) {
				mc.material.bounciness = 1.0f;
				mc.material.dynamicFriction = 0.25f;
				mc.material.staticFriction = 0.1f;
				mc.material.bounceCombine = PhysicMaterialCombine.Maximum;
				mc.material.frictionCombine = PhysicMaterialCombine.Minimum;
			}
			rb.velocity = (new Vector3 (Random.Range (-5, 5), Random.Range (-5, 5), Random.Range (-5, 5)));
			break;
		}
	}

	public void PickedUp() {

		this.transform.parent = Camera.main.transform;
		pickedUp = true;
		rb.useGravity = false;
		rb.velocity = new Vector3 (0, 0, 0);
		rb.freezeRotation = true;

	}

	public void LetGo() {
		this.transform.parent = null;
		rb.useGravity = true;
		rb.freezeRotation = false;
		pickedUp = false;
	}
}
