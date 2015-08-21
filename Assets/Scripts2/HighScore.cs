using UnityEngine;
using System.Collections;

public class HighScore : MonoBehaviour {

	public score1 score;


	void start()
	{
				score = GetComponent<score1> ();
		}

	void Update () {
		if ( score.points >= PlayerPrefs.GetInt ("bestScore") )
			{
			PlayerPrefs.SetInt("bestScore", score.points); // The first is the string name that refers to the saved score, the second is your score variable (int)
			PlayerPrefs.Save();

		}
	}

	void OnGUI()
	{
		GetComponent<GUIText>().text = "" +  PlayerPrefs.GetInt("bestScore");
	}

}