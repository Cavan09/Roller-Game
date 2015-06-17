using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	Animator Anim;
	
	// Use this for initialization
	void Start () 
	{
		Anim = GetComponentInChildren<Animator>();
	}
	
	public void StartGame()
	{
		Application.LoadLevel("Game");
	}
	
	public void QuitGame()
	{
		Application.Quit();
	}
	
	public void ShowHighScores(bool Show)
	{
		Anim.SetBool("HighScores", Show);
	}
}
