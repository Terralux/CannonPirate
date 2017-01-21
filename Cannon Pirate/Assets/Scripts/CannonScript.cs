using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour {

	public GameObject CannonBallPrefab;

	private List<Transform> targets = new List<Transform> ();
	private int targetIndex = 0;

	private float currentPower = 300;
	public float shotIntensity = 500f;

	//private bool isBoosting = false;
	//private bool isIncreasing = true;

	public Transform firePosition;

	// Use this for initialization
	void Awake () {
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerFiredCannon += ResetCannon;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerDied += InitiateCannon;
		InitiateCannon ();

		Physics.gravity = Physics.gravity * 0.4f;
	}

	void Start(){
		GameObject[] targetGOs = GameObject.FindGameObjectsWithTag ("CannonTarget");

		foreach (GameObject go in targetGOs) {
			targets.Add (go.transform);
		}
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
	}

	public void Fire(){
		GameObject tempGO = Instantiate (CannonBallPrefab, firePosition.position, CannonBallPrefab.transform.rotation) as GameObject;
		Vector3 fireForce = firePosition.forward * currentPower;
		tempGO.GetComponent<Rigidbody> ().AddForce (fireForce);

		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerFiredCannon ();
	}

	public void SwitchTargetLeft(){
		targetIndex--;
		if (targetIndex == -1) {
			targetIndex = targets.Count - 1;
		}
	}

	public void SwitchTargetRight(){
		targetIndex++;
		if (targetIndex == targets.Count) {
			targetIndex = 0;
		}
	}

	public void ResetCannon(){
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedReturn -= Fire;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedLeftMouseButton -= SwitchTargetLeft;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedRightMouseButton -= SwitchTargetRight;
	}

	public void InitiateCannon(){
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedReturn += Fire;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedLeftMouseButton += SwitchTargetLeft;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedRightMouseButton += SwitchTargetRight;
	}
}