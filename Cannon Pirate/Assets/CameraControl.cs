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

	// Use this for initialization
	void Start () {
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerFiredCannon += PlayerFiredCannon;
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentState) {
		case CameraState.GOAL_POSITION:
			break;
		case CameraState.START_POSITION:
			break;
		case CameraState.FOLLOWING_TARGET:
			float fraction = Vector3.Distance (target.position + new Vector3 (0, cameraHeight, cameraDistance), transform.position) / maxOffset;

			transform.position = Vector3.Lerp(transform.position, target.position + new Vector3 (0, cameraHeight, 0) + target.forward * -cameraDistance, fraction);
			transform.LookAt (target.position + new Vector3 (0, targetHeightOffset, 0));
			break;
		}
	}

	public void PlayerFiredCannon(){
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		currentState = CameraState.FOLLOWING_TARGET;
	}
}