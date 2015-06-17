using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {


	public float CameraMoveSpeed;
	public float MoveThreshHold;
	Transform PlayerTransform;
	Vector2 ScreenSize;
	public float XDiff;
	public float YDiff;
	// Use this for initialization
	void Start () 
	{
		PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		
		ScreenSize = new Vector2(Screen.width, Screen.height);
		
		ScreenSize = ScreenSize.normalized * MoveThreshHold;
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		
		if(PlayerTransform != null)
		{
			XDiff = PlayerTransform.position.x - transform.position.x;
			YDiff = PlayerTransform.position.y - transform.position.y;
			Vector3 movePos = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, -10);
			
			
			if(XDiff < -ScreenSize.x *2 || XDiff > ScreenSize.x /2)
			{
				transform.position = Vector3.Slerp(transform.position,movePos,CameraMoveSpeed * Time.deltaTime);
			}
			
			if(YDiff < -ScreenSize.y || YDiff > ScreenSize.y)
			{
				transform.position = Vector3.Slerp(transform.position,movePos,CameraMoveSpeed * Time.deltaTime);
			}
			
		}
	}
}
