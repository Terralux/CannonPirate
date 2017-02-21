using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButtonScript : MonoBehaviour {

	public int maxStarRatingLimit;
	public int halfStarRatingLimit;
	public int treasureCount;

	//public Texture2D levelImage;
	public Sprite levelImage;

	public SceneField levelAsset;

	public void loadLevel(){
		SceneManager.LoadScene (levelAsset.SceneAsset.name);
	}

}
