using UnityEngine;
using System.Collections;

public class Roller : MonoBehaviour 
{
	public float speed = 0;
	public Vector3 Rotation;
	
	void Start()
	{
		Rotation = transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Rotation.x += speed * Time.deltaTime;
		
		transform.eulerAngles = Rotation;
	}
}
