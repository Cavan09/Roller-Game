using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {


	public float timer = 3.0f;
	SphereCollider collider;
	public ParticleSystem[] Explosions;
	public ParticleSystem[] Fuze;
	Collider ScanZone;
	Vector3 Rot;
	public float RandRot;
	float speed = 50;
	
	void Start()
	{
		collider = GetComponent<SphereCollider>();
		
		if(GetComponent<SphereCollider>().isTrigger)
		{
			ScanZone = GetComponent<SphereCollider>();
		}
		
		Rot = transform.rotation.eulerAngles;
	}
	
	void Update()
	{	

		
		Rot.y -= speed * Time.deltaTime;
		
		transform.eulerAngles = Rot;
		
		if(!GetComponent<Renderer>().enabled)
		{
			timer -= Time.deltaTime;
		}
		if( timer <= 0)
		{
			GetComponent<Renderer>().enabled = true;
			collider.enabled = true;
			PlayFuze(true);
			timer = 3.0f;
		}
	}
	
	// Use this for initialization
	public void Addforce(Rigidbody player)
	{
		Debug.Log("Boom");
		player.velocity = player.velocity.normalized;
		player.AddExplosionForce(60000,transform.position, 500);
		GetComponent<Renderer>().enabled = false;
		collider.enabled = false;
		PlayerEffect();
		PlayFuze(false);
	}
	
	void PlayerEffect()
	{
		for(int i = 0; i < Explosions.Length; i++)
		{
			Explosions[i].Play(true);
		}
	}
	
	void PlayFuze(bool play)
	{
		for(int i = 0; i < Fuze.Length; i++)
		{
			Fuze[i].gameObject.SetActive(play);
		}
	}
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Player")
		{	
			Addforce(other.gameObject.GetComponent<Rigidbody>());
		}
	}
	
	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			float dist = Vector3.Distance(transform.position, other.transform.position);
			GetComponent<Renderer>().material.SetColor("_OutlineColor",Color.red);
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			GetComponent<Renderer>().material.SetColor("_OutlineColor",Color.black);
		}
	}
}
