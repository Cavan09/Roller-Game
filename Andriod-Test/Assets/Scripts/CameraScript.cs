using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float CameraMoveSpeed;
	Player PlayerTransform;
	CameraStates _CurrentState;
	// Use this for initialization
	void Start () 
	{
		PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		switch(PlayerTransform.CurrentState)
		{
			case Player.PlayerStates.Grabbing:
				_CurrentState = CameraStates.FollowRoller;
			break;
			
			case Player.PlayerStates.InAir:
				_CurrentState = CameraStates.FollowPlayer;
			break;
		}
		
		UpdateCamState(_CurrentState);
	}
	
	void UpdateCamState(CameraStates state)
	{
		switch(state)
		{
			case CameraStates.FollowPlayer:
			{
				if(PlayerTransform != null)
				{
					Vector3 movePos = new Vector3(PlayerTransform.transform.position.x, PlayerTransform.transform.position.y, -10);
					transform.position = Vector3.Slerp(transform.position,movePos,CameraMoveSpeed * Time.deltaTime);
				}
			}
			break;
				
			case CameraStates.FollowRoller:
			{
				Vector3 movePos = new Vector3(PlayerTransform.GetRoller.transform.position.x, PlayerTransform.GetRoller.transform.transform.position.y, -10);
				transform.position = Vector3.Slerp(transform.position,movePos,CameraMoveSpeed * Time.deltaTime);
			}
			break;
		}
	}
	
	enum CameraStates
	{
		FollowPlayer, FollowRoller
	}
}
