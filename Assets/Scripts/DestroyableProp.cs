using UnityEngine;
//using System.Collections;

public class DestroyableProp : MonoBehaviour {
	
	public void AskDestroyByPlayer(){
		Destroy(gameObject);
	}
}
