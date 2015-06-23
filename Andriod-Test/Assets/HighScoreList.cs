using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HighScoreList : MonoBehaviour {

	public Text[] Scores;
	HighScores _HighScores = new HighScores();
	List<float> _OrderedScores = new List<float>();

	// Use this for initialization
	void Awake () 
	{
		_HighScores = Utils.Load();
	}
	
	// Update is called once per frame
	public void UpdateScores()
	{
		LoadOrderedScores();
		for(int i = 0; i < Scores.Length; i++)
		{
			int place = i + 1;
			Scores[i].text = place + ".\t" + Mathf.Round(_OrderedScores[i] * 100);
		}
	}
	
	void LoadOrderedScores()
	{
		_OrderedScores.Clear();
		if(_HighScores != null)
		{
			for(int i = 0; i < Scores.Length; i++)
			{
				_OrderedScores.Add(_HighScores.HighScore[i]);
			}
		}
		
		_OrderedScores.Sort();
		_OrderedScores.Reverse();
	}
}
