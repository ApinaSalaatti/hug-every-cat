using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class InvertedSphere : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invert();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void Invert() {
		float size = 3f;

		MeshFilter mf = GetComponent<MeshFilter>();
		Mesh mesh = mf.sharedMesh;

		mf.sharedMesh = new Mesh();
		
		
		//Scale the vertices;
		Vector3[] vertices = mesh.vertices;
		for (int i = 0; i < vertices.Length; i++)
			vertices[i] = vertices[i] * size;
		mf.sharedMesh.vertices = vertices;
		
		// Reverse the triangles
		int[] triangles = mesh.triangles;
		for (int i = 0; i < triangles.Length; i += 3) {
			int t = triangles[i];
			triangles[i] = triangles[i+2];
			triangles[i+2] = t;
		}
		mf.sharedMesh.triangles = triangles;
		
		// Reverse the normals;
		Vector3[] normals = mesh.normals;
		for (int i = 0; i < normals.Length; i++)
			normals[i] = -normals[i];
		mf.sharedMesh.normals = normals;
		
		
		mf.sharedMesh.uv = mesh.uv;
		mf.sharedMesh.uv2 = mesh.uv2;
		mf.sharedMesh.RecalculateBounds();
	}
}
