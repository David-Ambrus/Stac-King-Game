using UnityEngine;
using System.Collections;

public class  ScriptB : MonoBehaviour {


	public GameObject lshape;
	public GameObject smlsquare;
	public GameObject bgsquare;
	public GameObject hatshape;
	public GameObject zshape;
	public GameObject straight;
	public GameObject bomb;
	public GameObject spawnlocal;
	public int number;






	
	void Start () 
	{
		StartCoroutine(spawning());
	}
	
	IEnumerator spawning()
	{
		if (number <= 10f)
		{
			Instantiate(smlsquare, spawnlocal.transform.position, Quaternion.identity);
			
			yield return new WaitForSeconds(1);
			Debug.Log ("Spawn small square");
		}
		else if (number > 10f && number <= 20f)
		{
			Instantiate(lshape, spawnlocal.transform.position, Quaternion.identity);
			yield return new WaitForSeconds(1);
			Debug.Log ("Spawn l shape");
		}
		else if (number > 20f && number <= 30f)
		{
			Instantiate(bgsquare, spawnlocal.transform.position, Quaternion.identity);
			yield return new WaitForSeconds(1);
			Debug.Log ("Spawn l bgsquare");
		}
		else if (number > 30f && number <= 40f)
		{
			Instantiate(hatshape, spawnlocal.transform.position, Quaternion.identity);
			yield return new WaitForSeconds(1);
			Debug.Log ("Spawn hatshape");
		}
		else if (number > 40f && number <= 50f)
		{
			Instantiate(zshape, spawnlocal.transform.position, Quaternion.identity);
			
			yield return new WaitForSeconds(1);
			Debug.Log ("Spawn zshape");
		}
		else if (number > 50f && number <= 60f)
		{
			Instantiate(straight, spawnlocal.transform.position, Quaternion.identity);
			
			yield return new WaitForSeconds(1);
			Debug.Log ("Spawn straight");
		}
		else if (number > 60f && number <= 65f)
		{
			Instantiate(bomb, spawnlocal.transform.position, Quaternion.identity);
			
			yield return new WaitForSeconds(1);
			Debug.Log ("Spawn bomb");
		}
	}
}

