using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	
	public PlayerStates CurrentState;
	Rigidbody body;
	Roller CurrentRoller;
	Vector3 lastPos;
	Vector3 restartPos;
	Quaternion restartRot;
	float MaxAngularVelocity;
	
	// Use this for initialization
	void Start () 
	{
		body = GetComponent<Rigidbody>();
		lastPos = Vector3.zero;
		restartPos = transform.position;
		restartRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateTouchControls();
		if(Input.GetKeyDown(KeyCode.R))
		{
			transform.position = restartPos;
			transform.rotation = restartRot;
			body.velocity = Vector3.zero;
		}
		
	}
	
	void UpdateTouchControls () 
	{
		switch(CurrentState)
		{
			case PlayerStates.Grounded:
			
			if(Input.GetMouseButton(0))
			{
				body.AddForce(transform.forward, ForceMode.VelocityChange);
			}
			
			break;
			
			case PlayerStates.Grabbing:
			
			Vector3 currentPos = Vector3.zero;
			
			currentPos = transform.position;
			
			Vector3 start = CurrentRoller.transform.position - lastPos ;
			Vector3 end =  CurrentRoller.transform.position - currentPos;
			
			if(lastPos != Vector3.zero)
			{
				float deltaDist = (lastPos - currentPos).magnitude;
//				Debug.DrawLine(CurrentRoller.transform.position,currentPos, Color.red);
//				Debug.DrawLine(CurrentRoller.transform.position,lastPos, Color.red);
//				Debug.DrawLine(lastPos, currentPos, Color.green);
				Debug.Log((lastPos - currentPos).magnitude);
				float angularDistplacement = deltaDist / CurrentRoller.GetComponent<CapsuleCollider>().radius;
				float angularVelocity = angularDistplacement / Time.fixedDeltaTime;
				MaxAngularVelocity = angularVelocity > MaxAngularVelocity ? angularVelocity : MaxAngularVelocity;
			}

			if(Input.GetMouseButton(0))
			{
				body.velocity = (currentPos - lastPos).normalized * MaxAngularVelocity;
				transform.parent = null;
				body.useGravity = true;
				MaxAngularVelocity = 0;
				CurrentState = PlayerStates.InAir;
			}
			
			lastPos = currentPos;
			
			break;
			
			case PlayerStates.InAir:
			transform.parent = null;
			body.useGravity = !body.useGravity ? true : true; 
			
			break;
		}
	}
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Ground")
		{
			CurrentState = PlayerStates.Grounded;
			transform.rotation = restartRot;
		}
		
		else if(other.gameObject.tag == "Roller")
		{
			lastPos = Vector3.zero;
			CurrentRoller = other.gameObject.GetComponent<Roller>();
			body.velocity = Vector3.zero;
			CurrentState = PlayerStates.Grabbing;
			transform.parent = other.gameObject.transform;
			body.useGravity = false;
		}
	}
	
	void OnCollisionExit(Collision other)
	{
		CurrentState = PlayerStates.InAir;
		
	}
	
	public enum PlayerStates
	{
		Grounded,InAir,Grabbing
	}
}
