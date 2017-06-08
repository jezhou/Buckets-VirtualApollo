using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public static string difficulty; 
	public static int current_level;
	public static int score;
	public static int num_balls;
	public static float radius = 3.0f;

	public static Color [] colors;
	public static int [] labels;
	public static Hashtable labels_map;
	public static int num_buckets;
	public static int buckets_instantiated;

	public static bool repeat_buckets;

	public Text uiText;
	public Button restartButton;

	public GameObject colorSphere;
	public GameObject bucket;
	public GameObject environment;

	private bool beat_game = false;

	// Use this for initialization
	void Start () {
		current_level = 1;
		difficulty = "hard";
		ConfigureDifficulty ();
		StartLevel (current_level);
	}

	// Update is called once per frame
	void Update () {
		if (num_balls <= 0 && !beat_game) {
			uiText.text = "You win!";
			restartButton.gameObject.SetActive (true);
			foreach (GameObject o in GameObject.FindGameObjectsWithTag("Bucket")) {
				Destroy (o);
			}
		}
			
	}

	public void NextLevel() {
		current_level++;
		StartLevel (current_level);
	}

	void StartLevel(int x) {
		
		// Todo: Should really be taking something like a JSON as an argument,
		// then extracting its parameters to initialize levels. Levels
		// are currently hardcoded below

		restartButton.gameObject.SetActive (false);

		switch (x) {
		case 1:
			InitGame (0, 3, new Color[] { Color.red });
			break;
		case 2:
			InitGame (0, 5, new Color[] { Color.red, Color.green});
			break;
		case 3: 
			InitGame (0, 8, new Color[] { Color.red, Color.green, Color.blue });
			break;
		case 4:
			InitGame (0, 10, new Color[] { Color.red, Color.green, Color.blue, Color.blue }, true);
			break;
		case 5:
			InitGame (0, 10, new Color[] { Color.red, Color.red, Color.green, Color.blue, Color.blue }, true);
			break;
		case 6:
			InitGame (0, 15, new Color[] { Color.red, Color.red, Color.green, Color.green, Color.blue, Color.blue }, true);
			break;
		default:
			uiText.text = "You beat the game! Congratulations.";
			restartButton.gameObject.SetActive (false);
			beat_game = true;
			return;
		}

		this.Spawn ();			
	}

	void InitGame(int s, int nb, Color [] col_arr, bool rb = false) {
		score = s;
		num_balls = nb;
		colors = col_arr;
		num_buckets = colors.Length;
		buckets_instantiated = 0;
		uiText.text = "Put the balls into the buckets!";
		repeat_buckets = rb;

		if (repeat_buckets) {
			labels_map = new Hashtable ();
			labels = new int[num_buckets];
			int i = 0;

			foreach (Color c in colors) {
				if (!labels_map.ContainsKey (c)) {
					int rand = Random.Range (1, 100);
					while (labels_map.ContainsKey (rand))
						rand = Random.Range (1, 100);

					ArrayList ar = new ArrayList ();
					ar.Add (rand);
					labels_map.Add (c, ar);
					labels[i] = rand;
				} else {
					int rand = Random.Range (1, 100);
					while (labels_map.ContainsKey (rand))
						rand = Random.Range (1, 100);

					ArrayList ar = (ArrayList)labels_map[c];
					ar.Add(rand);
					labels[i] = rand;
				}
				i++;
			}
		}
	}

	void ConfigureDifficulty() {
		Component[] mesh_colliders = environment.GetComponentsInChildren<MeshCollider> ();

		switch (difficulty) {
		case "easy":
			foreach (MeshCollider mc in mesh_colliders) {
				mc.material.bounciness = 0.0f;
				mc.material.dynamicFriction = 0.0f;
				mc.material.staticFriction = 0.0f;
				mc.material.bounceCombine = PhysicMaterialCombine.Average;
				mc.material.frictionCombine = PhysicMaterialCombine.Average;
			}
			break;
		case "medium":
			foreach (MeshCollider mc in mesh_colliders) {
				mc.material.bounciness = 1.0f;
				mc.material.dynamicFriction = 0.25f;
				mc.material.staticFriction = 0.1f;
				mc.material.bounceCombine = PhysicMaterialCombine.Maximum;
				mc.material.frictionCombine = PhysicMaterialCombine.Minimum;
			}
			break;
		case "hard":
			foreach (MeshCollider mc in mesh_colliders) {
				mc.material.bounciness = 1.0f;
				mc.material.dynamicFriction = 0.25f;
				mc.material.staticFriction = 0.1f;
				mc.material.bounceCombine = PhysicMaterialCombine.Maximum;
				mc.material.frictionCombine = PhysicMaterialCombine.Minimum;
			}
			break;
		}
	}

	void Spawn() {

		for (int i = 0; i < LevelManager.num_balls; i++) {
			Vector3 pos;
			pos.x = Random.Range (-10, 10);
			pos.y = Random.Range (2, 5);
			pos.z = Random.Range (-10, 10);
			Instantiate (colorSphere, pos, Quaternion.identity);
		}

		for (int i = 0; i < LevelManager.num_buckets; i++) {

			if (repeat_buckets)
				bucket.GetComponentInChildren<TextMesh> ().text = labels [i].ToString ();
			else
				bucket.GetComponentInChildren<TextMesh> ().text = "";

			float placement = (float)i / LevelManager.num_buckets;
			Vector3 position2 = RandomCircle (this.transform.position, radius, 0.0f, placement);

			Quaternion q = Quaternion.LookRotation (new Vector3(0, 0, 0) - position2);
			q *= Quaternion.Euler (0, 180f, 0);
			Instantiate (bucket, position2, q);
		}
	}

	public Vector3 RandomCircle ( Vector3 center , float radius, float height, float placement){
		float ang = placement * 360;
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.y = center.y + height;
		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		return pos;
	}

}
