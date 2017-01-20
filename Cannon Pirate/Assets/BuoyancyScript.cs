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

	private Vector3 average;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		bc = GetComponent<BoxCollider> ();
		bp = new BoxPoints(bc.size.x/2, bc.size.z/2, bc.size.y/2);

		waterTransform = GameObject.FindGameObjectWithTag ("Water").transform;
		water = waterTransform.GetComponent<MeshFilter>().mesh;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 min = water.vertices[0];
		for(int i = 0; i < water.vertices.Length; i++){
			if (Vector3.Distance (water.vertices[i], transform.position) < Vector3.Distance (min, transform.position)) {
				min = water.vertices[i];
			}
		}

		min = waterTransform.TransformPoint (min);

		if (transform.position.y < min.y) {
			rb.AddForce (new Vector3 (0, upwardsForce, 0) + rb.velocity * -1 * viscusity);

			average = Vector3.zero;
			int count = 0;

			for (int i = 0; i < 4; i++) {
				if (transform.TransformPoint(bp.top.points [i]).y < min.y) {
					average += transform.TransformPoint(bp.top.points [i]);
					count++;
				}
				if (transform.TransformPoint(bp.bottom.points [i]).y < min.y) {
					average += transform.TransformPoint(bp.bottom.points [i]);
					count++;
				}
			}

			average = average / count;

			rb.AddForceAtPosition (Vector3.up * surfaceFloatForce, average);
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawLine (average + transform.position, average + transform.position + Vector3.up * 5);

		if (bp != null) {

			Gizmos.color = Color.green;
			for (int i = 0; i < 4; i++) {
				Gizmos.DrawSphere (transform.TransformPoint (bp.top.points [i]), 0.1f);
				Gizmos.DrawSphere (transform.TransformPoint (bp.bottom.points [i]), 0.1f);
			}
		}
	}

	//calculate average of points below the water
	//Vector3 v1 = Vector3.Cross(forwardVelocity, Vector3.up);
	//Vector3 v2 = Vector3.Cross(forwardVelocity, v1);
	//raycast across that plane

	/*
	Vector3 pointOutFront = transform.position + (velocityDirection * 10);

	for(float x = -width; x <= width; x += 1){
		for(float y = -width; y <= width; y += 1){
			Vector3 start = pointOutFront + (v1 * x) + (v2 * y);
			if(Physics.Raycast(start, -velocityDirection, out hit, 10)){
				Push it!
			}
		}
	} 
	*/

	//To handle waves, compare points of water to center of object

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