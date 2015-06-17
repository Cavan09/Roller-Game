using UnityEngine;
using System.Collections;

public class Roller : MonoBehaviour 
{
	public float speed = 0;
	Vector3 Rotation;
	public bool ClockWise;
	
	float Radius;
	
	public virtual void Start()
	{
		Radius = GetComponent<CapsuleCollider>().radius / transform.localScale.x;
		Rotation = transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
	if(ClockWise)
		Rotation.z += speed * Time.deltaTime;
		else
			Rotation.z -= speed * Time.deltaTime;
		
		transform.eulerAngles = Rotation;
	}

	public float GetRadius {
		get {
			return Radius;
		}
	}
}
