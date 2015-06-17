using UnityEngine;
using System.Collections;

public class ForcePosition : MonoBehaviour {
	
	float zPos;
	public float speed = 10;
	
	// Use this for initialization
	void Start () {
		zPos = transform.position.z;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
	}
}
