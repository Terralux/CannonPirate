using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSessionUIHandler : MonoBehaviour {

	public GameObject winScreen;
	public GameObject gameScreen;

	public GameObject livesParent;
	public Text bulletsText;
	public Text fartsText;

	public GameObject lifePrefab;

	void Awake () {
		winScreen.SetActive (false);
		gameScreen.SetActive (true);
	}

	void Start() {
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerReachedGoal += GoalReached;

		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerLivesUpdated += UpdateLife;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerFartsUpdated += UpdateFarts;
		Toolbox.FindRequiredComponent<EventSystem> ().OnPlayerBulletsUpdated += UpdateBullet;

		Toolbox.FindRequiredComponent<EventSystem> ().OnStartGame ();
	}

	public void GoalReached(){
		StartCoroutine (WaitForGoalPopup());
	}

	IEnumerator WaitForGoalPopup(){
		yield return new WaitForSeconds (3f);

		winScreen.SetActive (true);
		gameScreen.SetActive (false);
	}

	public void UpdateFarts(int value){
		fartsText.text = value.ToString ();
	}

	public void UpdateBullet(int value){
		bulletsText.text = value.ToString ();
	}

	public void UpdateLife(int value){
		if (livesParent.transform.childCount < value) {
			for (int i = livesParent.transform.childCount; i < value; i++) {
				Transform t = (Instantiate (lifePrefab, transform.position, Quaternion.identity) as GameObject).transform;
				t.SetParent (livesParent.transform);
			}
		} else {
			for (int i = value; i < livesParent.transform.childCount; i++) {
				Destroy(livesParent.transform.GetChild(0).gameObject);
			}
		}
	}
}