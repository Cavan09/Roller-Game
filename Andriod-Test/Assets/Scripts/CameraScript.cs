using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {


	public float CameraMoveSpeed;
	public float MoveThreshHold;
	Transform PlayerTransform;
	public float XDiff;
	public float YDiff;
	// Use this for initialization
	void Start () 
	{
		PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(PlayerTransform != null)
		{
			XDiff = PlayerTransform.position.x - transform.position.x;
			YDiff = PlayerTransform.position.y - transform.position.y;
			Vector3 movePos = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, -10);
			
			
			if(XDiff < -MoveThreshHold || XDiff > MoveThreshHold)
			{
				transform.position = Vector3.Lerp(transform.position,movePos,CameraMoveSpeed * Time.deltaTime);
			}
			
			if(YDiff < -MoveThreshHold || YDiff > MoveThreshHold)
			{
				transform.position = Vector3.Lerp(transform.position,movePos,CameraMoveSpeed * Time.deltaTime);
			}
			
		}
	}
}
