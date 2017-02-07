using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelDataScritableObject {

	public int maxStarRatingLimit;
	public int halfStarRatingLimit;

	public int treasureCount;
	public Texture2D levelImage;

	public SceneField levelAsset;

}
