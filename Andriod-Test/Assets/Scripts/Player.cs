using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour 
{
	public PlayerStates CurrentState;
	Rigidbody body;
	Roller CurrentRoller;
	Vector3 lastPos;
	Vector3 restartPos;
	Quaternion restartRot;
	float MaxAngularVelocity;
	float Score;
	float DisplacmentConstant = 0.0007f;
	public ScoreTracker scoreTracker;
	
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
		if(Input.GetKeyDown(KeyCode.R))
		{
			Respawn();
		}
		
		transform.position = new Vector3(transform.position.x, transform.position.y, -3.7f);
		
		Score = transform.position.x - restartPos.x > Score ?  transform.position.x - restartPos.x : Score;
	}
	
	void FixedUpdate()
	{
		UpdateTouchControls();
	}
	
	void UpdateTouchControls () 
	{
		switch(CurrentState)
		{
			case PlayerStates.Grounded:
			
			if(Input.GetMouseButton(0))
			{
				//body.AddForce(transform.forward, ForceMode.VelocityChange);
			}
			
			break;
			
			case PlayerStates.Grabbing:
			
			Vector3 currentPos = Vector3.zero;
			
			currentPos = transform.position;
			
			Vector3 start = CurrentRoller.transform.position - lastPos ;
			Vector3 end =  CurrentRoller.transform.position - currentPos;
			
			if(lastPos != Vector3.zero)
			{
				float deltaDist = DisplacmentConstant * Mathf.Abs( CurrentRoller.speed);
				float angularDistplacement = deltaDist / CurrentRoller.GetRadius;
				float angularVelocity = angularDistplacement / Time.fixedDeltaTime;
				MaxAngularVelocity = angularVelocity > MaxAngularVelocity ? angularVelocity : MaxAngularVelocity;
				
				Debug.Log("angularVel: " + angularVelocity);
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
	
	public void Respawn()
	{
		transform.position = restartPos;
		transform.rotation = restartRot;
		body.velocity = Vector3.zero;
		if(Score > scoreTracker.HighScore)
		{
			scoreTracker.HighScore = Score;
		}
		Score = 0;
		
		scoreTracker.EnableButtons(false);
		
	}
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Ground")
		{
			//RemoveDoll();
			CurrentState = PlayerStates.Grounded;
			transform.rotation = restartRot;
		}
		
		else if(other.gameObject.tag == "Roller")
		{
			//RemoveDoll();
			lastPos = Vector3.zero;
			CurrentRoller = other.gameObject.GetComponent<Roller>();
			body.velocity = Vector3.zero;
			CurrentState = PlayerStates.Grabbing;
			transform.parent = other.gameObject.transform;
			body.useGravity = false;
		}
		else if(other.gameObject.tag == "DeathBox")
		{
			scoreTracker.EnableButtons(true);
		}
	}
	
	void OnCollisionExit(Collision other)
	{
		CurrentState = PlayerStates.InAir;
	}

	
	void RemoveDoll()
	{
		GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
	}
	
	public enum PlayerStates
	{
		Grounded,InAir,Grabbing
	}

	public float getScore {
		get {
			return Score;
		}
	}
}
