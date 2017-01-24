using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class BuoyancyScript : MonoBehaviour {

	[Range(0f,10f)]
	public float upwardsForce = 1;

	[Range(0f,10f)]
	public float viscusity = 1;

	[Range(0f,10f)]
	public float surfaceFloatForce = 1;

	private Rigidbody rb;
	private BoxCollider bc;

	private BoxPoints bp;

	private Mesh water;
	private Transform waterTransform;
	//private WavesScript waves;

	private Vector3 average;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		bc = GetComponent<BoxCollider> ();
		bp = new BoxPoints(bc.size.x/2, bc.size.z/2, bc.size.y/2);

		waterTransform = GameObject.FindGameObjectWithTag ("Water").transform;
		//waves = waterTransform.GetComponent<WavesScript> ();
		water = waterTransform.GetComponent<MeshFilter>().mesh;
	}
	
	// Update is called once per frame
	void Update () {
		int minIndex = 0;
		for(int i = 0; i < water.vertices.Length; i++){
			if (Vector3.Distance (water.vertices[i], transform.position) < Vector3.Distance (water.vertices[minIndex], transform.position)) {
				minIndex = i;
			}
		}

		Vector3 minPos = waterTransform.TransformPoint (water.vertices[minIndex]);
		//Vector3 minPos = waterTransform.TransformPoint (waves.GetPositionOfVertex(minIndex));

		//it runs at 10 by 83 FPS
		//it should have 

		if (transform.position.y < minPos.y) {
			rb.AddForce ((new Vector3 (0, upwardsForce, 0) + rb.velocity * -1 * viscusity) * Time.deltaTime * 83);

			average = Vector3.zero;
			int count = 0;

			for (int i = 0; i < 4; i++) {
				if (transform.TransformPoint(bp.top.points [i]).y < minPos.y) {
					average += transform.TransformPoint(bp.top.points [i]);
					count++;
				}
				if (transform.TransformPoint(bp.bottom.points [i]).y < minPos.y) {
					average += transform.TransformPoint(bp.bottom.points [i]);
					count++;
				}
			}

			average = average / count;

			rb.AddForceAtPosition ((Vector3.up * surfaceFloatForce) * Time.deltaTime * 83, average);
		}
	}
}

public class BoxPoints{
	public PlanePoints top;
	public PlanePoints bottom;

	public BoxPoints(float width, float length, float height){
		top = new PlanePoints (width, length, height);
		bottom = new PlanePoints (width, length, -height);
	}
}

public class PlanePoints{
	public Vector3[] points = new Vector3[4];

	public PlanePoints(float width, float length, float height){
		points[0] = new Vector3 (-width, height, length);
		points[1] = new Vector3 (width, height, length);
		points[2] = new Vector3 (-width, height, -length);
		points[3] = new Vector3 (width, height, -length);
	}
}