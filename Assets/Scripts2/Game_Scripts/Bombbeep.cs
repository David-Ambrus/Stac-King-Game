using UnityEngine;
using System.Collections;

public class Bombbeep : MonoBehaviour {

	IEnumerator Start()	{
		yield return StartCoroutine(Wait());
	}
	
	
	IEnumerator Wait()	{
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
		Debug.Log("Bomb Tick");
		yield return new WaitForSeconds (1);
		audio.Play();
		Debug.Log("Bomb Tick");
		yield return new WaitForSeconds (1);
		audio.Play();
		Debug.Log("Bomb Tick");
	}
}