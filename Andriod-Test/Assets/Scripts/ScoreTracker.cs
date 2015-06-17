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
		//Load ();
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
			//Save();
			Application.Quit(); 
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
	
	public void Save()
	{
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/SaveFile.dat");
		
		HighScores playerScores = new HighScores();
		playerScores.HighScore = _HighScore;
		
		formatter.Serialize(file, playerScores);
		file.Close();
	}
	
	public void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/SaveFile.dat"))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/SaveFile.dat",FileMode.Open);
			HighScores playerScores = (HighScores)formatter.Deserialize(file);
			file.Close();
			
			_HighScore = playerScores.HighScore;
		}
	}

	public float HighScore {
		get {
			return _HighScore;
		}
		set {
			_HighScore = value;
		}
	}
}

[Serializable]
class HighScores
{
	public float HighScore;
}
