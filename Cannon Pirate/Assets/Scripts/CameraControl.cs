using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	private enum CameraState
	{
		START_POSITION,
		FOLLOWING_TARGET,
		GOAL_POSITION
	}

	private CameraState currentState = CameraState.START_POSITION;

	private Transform target;

	public float cameraHeight;
	public float cameraDistance;
	public float targetHeightOffset;

	public float maxOffset;

	private Vector3 lookTarget;

	private float fraction;

	private Transform cannon;

	// Use this for initialization
	void Awake () {
		cannon = GameObject.FindGameObjectWithTag ("CannonPosition").transform;
		PlayerDied ();

		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerFiredCannon += PlayerFiredCannon;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerReachedGoal += PlayerReachedGoal;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerDied += PlayerDied;

		Toolbox.FindRequiredComponent<EventSystem> ().OnSwitchedTarget += ResetFraction;
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			return;
		}

		switch (currentState) {
		case CameraState.GOAL_POSITION:
			break;
		case CameraState.START_POSITION:
			fraction += Time.deltaTime / 2;

			lookTarget = cannon.position + cannon.forward * -(cameraDistance/2) + new Vector3 (0, cameraHeight/2, 0);
			transform.position = Vector3.Lerp (transform.position, lookTarget, fraction);
			transform.LookAt (target.position + Vector3.up * (cameraHeight/2));
			break;
		case CameraState.FOLLOWING_TARGET:
			fraction = Vector3.Distance (target.position + new Vector3 (0, cameraHeight, cameraDistance), transform.position) / maxOffset;

			transform.position = Vector3.Lerp (transform.position, target.position + new Vector3 (0, cameraHeight, 0) + target.forward * -cameraDistance, fraction);
			lookTarget = target.position + new Vector3 (0, targetHeightOffset, 0);
			transform.LookAt (Vector3.Lerp(transform.position + transform.forward, lookTarget, fraction));
			break;
		}
	}

	public void PlayerFiredCannon(){
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		currentState = CameraState.FOLLOWING_TARGET;
	}

	public void PlayerReachedGoal(){
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerDied -= PlayerDied;
		currentState = CameraState.GOAL_POSITION;
	}

	public void PlayerDied(){
		target = cannon;
		lookTarget = cannon.position + cannon.forward * -(cameraDistance/2) + new Vector3 (0, cameraHeight/2, 0);
		currentState = CameraState.START_POSITION;
	}

	public void ResetFraction(){
		fraction = 0;
	}
}