using UnityEngine;
using System.Collections;

public class Bomb_Explode : MonoBehaviour {

	
		public float radius = 50.0F;
		public float power = 100.0F;
		public GameObject explosionsprite;
		public GameObject explosionsprite2;
		public GameObject explosionsprite3;
		public GameObject explosionsprite4;
		public GameObject bomb;
		public Rigidbody rb;
		public int randomnumber;
	
	
	//Starts explosions off as inactive
	//Gets rigidbody from object at start		
		void start () {		
			explosionsprite.SetActive (false);
			explosionsprite2.SetActive (false);
			explosionsprite3.SetActive (false);
			explosionsprite4.SetActive (false);
			rb = GetComponent<Rigidbody>();
			
			}



	//Randomising explosion that plays
		void RandomExplosion() {
		if (randomnumber == 1) {
			explosionsprite.SetActive (true);
			Debug.Log ("Explosion 1");
		} else if (randomnumber == 2) {
			explosionsprite2.SetActive (true);
			Debug.Log ("Explosion 2");
		} else if (randomnumber == 3) {
			explosionsprite3.SetActive (true);
			Debug.Log ("Explosion 3");
		} else if (randomnumber == 4) {
			explosionsprite4.SetActive (true);
			Debug.Log ("Explosion 4");
		}
			}


	//Setting rigidbody to kinematic
		void DisableRagdoll() {
			rb.isKinematic = true;
			rb.detectCollisions = false;
   			}



		IEnumerator Start()	{
			randomnumber = Random.Range(1,5);
			yield return StartCoroutine(Wait());
				}

		
		IEnumerator Wait()	{

			Debug.Log("waiting");
			
			//Waits for an amount of seconds
			yield return new WaitForSeconds(3); 
			RandomExplosion ();
			DisableRagdoll();
			Destroy(bomb);
			//Applies an explosion force to all nearby rigidbodies
			Vector3 explosionPos = transform.position;
			Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
			foreach (Collider hit in colliders) {
				Rigidbody rb = hit.GetComponent<Rigidbody>();
			
			if (rb != null)
				rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
					}
				}

}
