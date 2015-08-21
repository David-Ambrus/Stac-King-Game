using UnityEngine;
using System.Collections;

public class ScoreTicker : MonoBehaviour {

	
		//this is what our score should be like after we're done 'ticking'
		public int TargetScore;
		
		//this is the value representing our 'real' score at any given time
		public int CurrentScore;
		
		//this is how much we increment the score every tick - set in the inspector
		public int ScorePerTick = 1;
		
		//this is how long we wait between ticks - set in the inspector
		public float TickInterval = 1;
		
		void Start()
		{
			//in this case, as in most cases, I start the coroutine using it's name, so it can be stopped later
			StartCoroutine("Ticker");
		}
		
		//raise the score target
		public void AddScore(int score)
		{
			TargetScore += score;
		}
		
		//this will increment the CurrentScore towards the TargetScore over time
		public IEnumerator Ticker()
		{
			//this loop will run forever so you can just call AddScore and the ticker will continue automatically
			while (true)
			{
				//we don't want to increment CurrentScore to infinity, so we only do it if it's lower than TargetScore
				if (CurrentScore < TargetScore)
				{
					CurrentScore += ScorePerTick;
					
					//this is a 'safety net' to ensure we never exceed our TargetScore
					if (CurrentScore > TargetScore)
					{
						CurrentScore = TargetScore;
					}
				}
				
				//wait for some time before incrementing again
				yield return new WaitForSeconds(TickInterval);
			}
		}
	}