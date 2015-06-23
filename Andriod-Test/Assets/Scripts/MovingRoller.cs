using UnityEngine;
using System.Collections;

public class MovingRoller : MonoBehaviour {

	Vector3 OriginalPos;
	Vector3 TargetPos;
	MoveStates CurrentState;
	Vector3 MovePos;
	public bool MoveX;
	public float RangeX = 2;
	public bool MoveY;
	public float RangeY = 2;
	
	public void Start()
	{
		OriginalPos = transform.position;
		TargetPos = OriginalPos;
		CurrentState = MoveStates.Move;
		MovePos = Vector3.zero;
	}
	
	// Update is called once per frame
	public void Update () 
	{
		switch(CurrentState)
		{
			case MoveStates.Move:
			
				transform.position = Vector3.MoveTowards(transform.position,TargetPos, Time.deltaTime);
				
				if(transform.position == TargetPos)
				{
					CurrentState = MoveStates.ChangeTarget;
				}
				
			break;
			
			case MoveStates.ChangeTarget:
			
				if(MoveX)
				{
					MovePos.x = Random.Range(RangeX,-RangeX);
				}
				if(MoveY)
				{
					MovePos.y = Random.Range(RangeY,-RangeY);
				}
				
				if(OriginalPos == transform.position)
				{
					TargetPos = OriginalPos + MovePos;;
				}
				else
				{
					TargetPos = OriginalPos;
				}
				
				CurrentState = MoveStates.Move;
				
			break;
		}
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == tag)
		{
			Debug.Log("Hit Roller");
			TargetPos = OriginalPos;
		}
	}
	
	enum MoveStates
	{
		Move,Return,ChangeTarget
	}
}
