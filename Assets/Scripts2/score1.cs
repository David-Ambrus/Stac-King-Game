using System;

using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class score1 : MonoBehaviour {



	public int points = 0;
	
	void OnGUI()
	{
		GetComponent<GUIText>().text = "" + (int)points;
	}



}
	