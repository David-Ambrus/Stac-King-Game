using System;

using System.Collections.Generic;

using UnityEngine;

public class LevelCounter : MonoBehaviour {

	public static int level = 1;
	
	void OnGUI()
	{
		GetComponent<GUIText>().text = " " + (int)level;
	}
}


