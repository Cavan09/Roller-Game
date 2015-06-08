using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour 
{
	Player player;
	Canvas ScoreBoard;
	Text ScoreOutput;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		ScoreBoard = GetComponent<Canvas>();
		ScoreOutput = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		ScoreOutput.text = "Score: " + Mathf.Round(player.getScore * 100);
	}
}
