using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public GameObject CannonBallPrefab;

	private List<Transform> targets = new List<Transform> ();
	private int targetIndex = 0;

	private int maxPower = 1000;
	private int minPower = 1;

	private float currentPower = 1;
	public float shotIntensity = 10f;

	private bool isBoosting = false;
	private bool isIncreasing = true;

	public Transform firePosition;

	// Use this for initialization
	void Start () {
		GameObject[] targetGOs = GameObject.FindGameObjectsWithTag ("CannonTarget");

		foreach (GameObject go in targetGOs) {
			targets.Add (go.transform);
		}

		Physics.gravity = Physics.gravity * 0.4f;
	}
	
	// Update is called once per frame
	void Update () {
		if (targets.Count < 1) {
			return;
		}

		/*
		if (isBoosting) {
			currentPower = currentPower + (Time.deltaTime * shotIntensity * (isIncreasing ? 1 : -1));

			if (currentPower >= maxPower) {
				isIncreasing = false;
			}
			if (currentPower <= minPower) {
				isIncreasing = true;
			}
		}
		*/

		Vector3 lookTarget = new Vector3 (targets[targetIndex].position.x, transform.position.y, targets[targetIndex].position.z);
		transform.LookAt (lookTarget);

		if (Input.GetKeyDown (KeyCode.Space)) {
			targetIndex++;
			if (targetIndex == targets.Count) {
				targetIndex = 0;
			}
		}

		if (Input.GetKeyUp (KeyCode.Return)) {
			currentPower = 300f;
			Fire ();
		}
	}

	void Fire(){
		GameObject tempGO = Instantiate (CannonBallPrefab, firePosition.position, CannonBallPrefab.transform.rotation) as GameObject;
		Vector3 fireForce = firePosition.forward * currentPower;
		tempGO.GetComponent<Rigidbody> ().AddForce (fireForce);

		//ResetFire ();
	}

	void ResetFire(){
		currentPower = minPower;
	}

	void OnGUI(){
		GUI.Box (new Rect(10, 10, Screen.width - 10, 30), "");
		GUI.Box (new Rect(10, 10, ((float)Screen.width - 15) * ((currentPower)/(float)maxPower), 30), "");
	}

}
