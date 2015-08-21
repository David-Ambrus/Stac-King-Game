using UnityEngine;
using System.Collections;

public class HitSound : MonoBehaviour {

	void OnTriggerEnter(Collider gameObject)
	{
		if (gameObject.tag == "player")
		{
			GetComponent<AudioSource>().Play();
		}
}
}
