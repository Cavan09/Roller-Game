using UnityEngine;
using System.Collections;

public class MovingRoller : Roller {

	Vector3 OriginalPos;
	Vector3 TargetPos;
	
	MoveStates CurrentState;
	
	public override void Start()
	{
		base.Start();
		OriginalPos = transform.position;
		TargetPos = OriginalPos;
		CurrentState = MoveStates.Move;
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		base.Update();
		
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
			
				if(OriginalPos == transform.position)
				{
				TargetPos = new Vector3(OriginalPos.x , OriginalPos.y + Random.Range(-2,2), OriginalPos.z);
				}
				else
				{
					TargetPos = OriginalPos;
				}
				
				CurrentState = MoveStates.Move;
				
			break;
		}
		
	}
	
	enum MoveStates
	{
		Move,Return,ChangeTarget
	}
}
