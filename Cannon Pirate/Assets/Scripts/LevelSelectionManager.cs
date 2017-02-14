using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelSelectionManager : MonoBehaviour {

	public CollectiveLevelData levelContainer;
	public GameObject buttonPrefab;
	public Transform levelPanel;

	// Use this for initialization
	void Start () {
		fillLevelData ();
	}

	void Update(){

	}

	void fillLevelData()
	{
		//Goes through the list of levels
		foreach (var LevelDataScritableObject in levelContainer.allLevels) 
		{
			GameObject newButton = Instantiate (buttonPrefab) as GameObject;

			LevelButtonScript buttonInfo = newButton.GetComponent<LevelButtonScript> ();

			//Compares and sets the values from our scriptable object list, to each button
			buttonInfo.maxStarRatingLimit = LevelDataScritableObject.maxStarRatingLimit;
			buttonInfo.halfStarRatingLimit = LevelDataScritableObject.halfStarRatingLimit;
			buttonInfo.treasureCount = LevelDataScritableObject.treasureCount;
			buttonInfo.levelImage = LevelDataScritableObject.levelImage;
			buttonInfo.levelAsset = LevelDataScritableObject.levelAsset;

			//Changes the sprite on the different buttons, depending on the assiged sprite at CollectiveLevelDataContainer (ScriptableObject)
			buttonPrefab.GetComponent<Image> ().sprite = LevelDataScritableObject.levelImage;

			//How do i access the goddamn OnClick ? So i can make sure that the button loads the right level by using levelAsset.SceneAssest! 
			/* Bla bla bla, some code here! */

			//Set the button as child of the panal - the UI Layout components will deal with the rest! - Right Tassi?
			newButton.transform.SetParent (levelPanel);
		}
	}


}
