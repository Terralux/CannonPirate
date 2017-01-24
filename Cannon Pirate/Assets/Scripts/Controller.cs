using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	private float fingerStartTime  = 0.0f;
	private Vector2 fingerStartPos = Vector2.zero;

	private bool isSwipe = false;
	private float minSwipeDist  = 50.0f;
	private float maxSwipeTime = 0.5f;
	
	// Update is called once per frame
	void Update () {
		if (Application.isMobilePlatform) {
			if (Input.touchCount > 0) {
				foreach (Touch touch in Input.touches) {
					switch (touch.phase) {
					case TouchPhase.Began:
						/* this is a new touch */
						isSwipe = true;
						fingerStartTime = Time.time;
						fingerStartPos = touch.position;
						break;
					case TouchPhase.Canceled:
						/* The touch is being canceled */
						isSwipe = false;
						break;
					case TouchPhase.Ended:
						float gestureTime = Time.time - fingerStartTime;
						float gestureDist = (touch.position - fingerStartPos).magnitude;

						if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist) {
							Vector2 direction = touch.position - fingerStartPos;
							Vector2 swipeType = Vector2.zero;

							if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y)) {
								// the swipe is horizontal:
								swipeType = Vector2.right * Mathf.Sign (direction.x);
							} else {
								// the swipe is vertical:
								swipeType = Vector2.up * Mathf.Sign (direction.y);
							}

							if (swipeType.x != 0.0f) {
								if (swipeType.x > 0.0f) {
									// MOVE RIGHT
									if (Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedRightMouseButton != null) {
										Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedRightMouseButton ();
									}
								} else {
									// MOVE LEFT
									if (Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedLeftMouseButton != null) {
										Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedLeftMouseButton ();
									}
								}
							}

							if (swipeType.y != 0.0f) {
								if (swipeType.y > 0.0f) {
									// MOVE UP
									if (Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedReturn != null) {
										Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedReturn ();
									}
								} else {
									// MOVE DOWN
								}
							}
						}
						break;
					}
				}
			}
		}else {
			if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)) {
				if (Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedReturn != null) {
					Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedReturn ();
				}
			}
			if (Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow)) {
				if (Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedLeftMouseButton != null) {
					Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedLeftMouseButton ();
				}
			}
			if (Input.GetMouseButtonDown (1) || Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow)) {
				if (Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedRightMouseButton != null) {
					Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerPressedRightMouseButton ();
				}
			}
		}
	}
}