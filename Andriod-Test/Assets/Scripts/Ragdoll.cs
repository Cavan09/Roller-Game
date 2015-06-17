using UnityEngine;
using System.Collections;

public class Ragdoll : MonoBehaviour {
	
	GameObject player;
	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = player.transform.position;
	}
	
	public void AttachBody(Rigidbody player)
	{
		gameObject.AddComponent<FixedJoint>();
		GetComponent<FixedJoint>().connectedBody = player;
	}
}
