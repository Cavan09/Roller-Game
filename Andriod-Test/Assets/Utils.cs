using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class Utils : MonoBehaviour
{
	static HighScores _PlayerScores = new HighScores();

	public static void Save()
	{
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/SaveFile.dat");
		
		formatter.Serialize(file, _PlayerScores);
		file.Close();
	}
	
	public static HighScores Load()
	{
		if(File.Exists(Application.persistentDataPath + "/SaveFile.dat"))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/SaveFile.dat",FileMode.Open);
			_PlayerScores = (HighScores)formatter.Deserialize(file);
			file.Close();
			Debug.Log(_PlayerScores.HighScore.Count);
			return _PlayerScores;
		}
		else
		{
			for(int i = 0; i < 5; i++)
			{
				_PlayerScores.HighScore.Add(0.0f);
			}
			return _PlayerScores;
		}
		
		
	}
	
	public static float ReturnTopScore()
	{
		float TopScore = 0;
		for(int i = 0; i < _PlayerScores.HighScore.Count; i++)
		{
			if(TopScore < _PlayerScores.HighScore[i])
			{
				TopScore = _PlayerScores.HighScore[i];
			}
		}
		
		return TopScore;
	}
	
	public static bool CheckForNewScore(float newScore)
	{
		bool Higher = false;
		
		for(int i = 0; i < _PlayerScores.HighScore.Count; i++)
		{
			Debug.Log("NewScore:" + newScore  + "\nScoreChecked: " + _PlayerScores.HighScore[i]);
			if(newScore > _PlayerScores.HighScore[i])
			{
				Higher = true;
			}
		}
		
		if(Higher)
		{
			_PlayerScores.HighScore.Add(newScore);
			_PlayerScores.HighScore.Sort();
			_PlayerScores.HighScore.Reverse();
			_PlayerScores.HighScore.RemoveAt(5);
			
			
		}
		
		return Higher;
	}
}

[Serializable]
public class HighScores
{
	public List<float> HighScore = new List<float>();
}