using System;

using System.Collections.Generic;

using UnityEngine;



public class Timer1 : MonoBehaviour
	
{
	
	public float timeLeft = 20.0f;
	public GameObject Gameover;
	public score1 score;
	public GameObject HighScore;



	void start()
	{
		score = GetComponent<score1> ();
	}


	public void Update()
	

	{
		
				timeLeft -= Time.deltaTime;
		
		
		
				if (timeLeft <= 0.0f) {
			
						Gameover.SetActive (true);
						Time.timeScale = 0;
						GetComponent<GUIText>().text = "0";
			
				}

				if (timeLeft <= 0.0f && score.points >= PlayerPrefs.GetInt ("bestScore"))
		{		HighScore.SetActive (true);
		}

		else
			
		{
			
			GetComponent<GUIText>().text = "" + (int)timeLeft;
			
		}
}
}