using UnityEngine;
using System.Collections;

public class TestGrab : MonoBehaviour {

	Collider[] Bones;
	Vector3 ForceDir;
	Transform Roller;
	bool CalcOutwardForce = false;
	Transform[] Parent = new Transform[2];
	public Transform[] Hands;
	bool attached = false;
	
	// Use this for initialization
	void Start () 
	{
		ForceDir = Vector3.zero;
		for(int i = 0; i < Hands.Length; i++)
		{
			Parent[i] = Hands[i].parent.transform;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
//		if(CalcOutwardForce)
//			AddOutwardForce();

		if(Input.GetMouseButtonDown(0))
		{
			for(int i = 0; i < Hands.Length; i++)
			{
				if(Hands[i].GetComponent<FixedJoint>() != null)
				{
					Hands[i].parent = Parent[i];
					Hands[i].GetComponent<FixedJoint>().connectedBody = null;
					Destroy(Hands[i].GetComponent<FixedJoint>());
				}
				
			}
			EnableGravity();
			attached = false;
		}
		
	}
	
	public void OnCollisionEnter(Collision other)
	{
		
		
		if(!attached)
		{
		if(other.gameObject.tag == "Roller")
		{
			Debug.Log("Hit");
			Roller = other.gameObject.transform;
			ContactPoint contact = other.contacts[0];
				for(int i = 0; i < Hands.Length; i++)
				{
					if(GetComponent<FixedJoint>() == null)
					{
						Hands[i].parent = Roller;
						Hands[i].gameObject.AddComponent<FixedJoint>();
						Hands[i].GetComponent<FixedJoint>().connectedBody = Roller.GetComponent<Rigidbody>();
						//Hands[i].position = contact.point - GetComponent<CapsuleCollider>().bounds.extents;
					}
				}
			Bones = Physics.OverlapSphere(transform.position, 3.0f);

			for(int i = 0; i < Bones.Length; i++)
			{
				if(Bones[i].GetComponent<Rigidbody>() && Bones[i].gameObject.tag == "Player")
				{
					Rigidbody currentBondy = Bones[i].GetComponent<Rigidbody>();
					currentBondy.useGravity = false;
					currentBondy.velocity = Vector3.zero;
					
				}
			}
			
			CalcOutwardForce = true;
			
		}
		}
		attached = true;
	}
	
	void EnableGravity()
	{
		for(int i = 0; i < Bones.Length; i++)
		{
			if(Bones[i].GetComponent<Rigidbody>() && Bones[i].gameObject.tag == "Player")
			{
				Rigidbody currentBondy = Bones[i].GetComponent<Rigidbody>();
				currentBondy.useGravity = true;
			}
		}
	}
	
	void AddOutwardForce()
	{
		for(int i = 0; i < Bones.Length; i++)
		{
			if(Bones[i].GetComponent<Rigidbody>() && Bones[i].gameObject.tag == "Player")
			{
				Rigidbody currentBondy = Bones[i].GetComponent<Rigidbody>();
				currentBondy.useGravity = false;
				ForceDir =  Bones[i].transform.position - Roller.position;
				currentBondy.AddForce(ForceDir, ForceMode.VelocityChange);
			}
		}
	}
}
