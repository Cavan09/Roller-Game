using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour 
{
	public Player player;
	Canvas ScoreBoard;
	public Text ScoreOutput;
	public Text HighScoreOutput;
	float _HighScore;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		ScoreBoard = GetComponent<Canvas>();
		_HighScore = Utils.ReturnTopScore();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(player != null)
		{
			ScoreOutput.text = "Score: " + Mathf.Round(player.getScore * 100);
		}
		
		HighScoreOutput.text = "HighScore: " + Mathf.Round(HighScore * 100);
		
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Quit(); 
		}
	}
	
	public void Quit()
	{
		Application.Quit();
	}
	
	public void Menu()
	{
		Application.LoadLevel("Menu");
	}
	
	public void EnableButtons(bool enable)
	{
		for(int i = 0; i < transform.childCount; i++)
		{
			if(transform.GetChild(i).GetComponent<Button>())
			{
				transform.GetChild(i).gameObject.SetActive(enable);
			}
		}
	}
	
	public void CheckHighScore(float score)
	{
		if(Utils.CheckForNewScore(score))
		{
		
			//Display New HighScore for player
			//Have a button press show the buttons for retry, menu, and quit
			Debug.Log("True");
			Utils.Save();
		}

		EnableButtons(true);
		
	}

	public float HighScore 
	{
		get {return _HighScore;}
		set {_HighScore = value;}
	}
}


