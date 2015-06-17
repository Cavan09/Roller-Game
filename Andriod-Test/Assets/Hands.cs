using UnityEngine;
using System.Collections;

public class Hands : MonoBehaviour {
	
	TestGrab parent;
	
	// Use this for initialization
	void Start () {
		
		parent = GetComponentInParent<TestGrab>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision other)
	{
		parent.OnCollisionEnter(other);
	}
}
