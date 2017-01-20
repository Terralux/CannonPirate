using UnityEngine;
using System.Collections;

public class WavesScript : MonoBehaviour
{
	public float scale = 0.1f;
	public float speed = 1.0f;
	public float noiseStrength = 1f;
	public float noiseWalk = 1f;

	private Vector3[] baseHeight;

	void Update () {
		Mesh mesh = GetComponent<MeshFilter>().mesh;

		if (baseHeight == null)
			baseHeight = mesh.vertices;

		Vector3[] vertices = new Vector3[baseHeight.Length];
		for (int i=0;i<vertices.Length;i++)
		{
			Vector3 vertex = baseHeight[i];
			vertices [i] = AddNoise (vertex, i);
		}
		mesh.vertices = vertices;
		mesh.RecalculateNormals();
	}

	private Vector3 AddNoise(Vector3 vertex, int index){
		vertex.y += Mathf.Sin(Time.time * speed+ baseHeight[index].x + baseHeight[index].y + baseHeight[index].z) * scale;
		vertex.y += Mathf.PerlinNoise(baseHeight[index].x + noiseWalk, baseHeight[index].y + Mathf.Sin(Time.time * 0.1f)    ) * noiseStrength;
		return vertex;
	}

	public Vector3 GetPositionOfVertex(int index){
		return AddNoise(baseHeight [index], index);
	}
}