//using System;
using System.Collections;
using UnityEngine;
//using System.Collections;

public enum E_PlayerState {
	NOTHING,
	JUMP,
	PUSH,
	DESTROY
}

public class Player : MonoBehaviour {

	public E_PlayerState CurrentPlayerState;
	public float CurrentTimeLevel;
    private bool _god;


    //private GameObject _camera;

    // Use this for initialization
    void Start () {
		//_camera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		CurrentTimeLevel += Time.deltaTime;
	}
	
	void OnTriggerEnter(Collider other){
		if (other.tag == "Item"){
			CurrentPlayerState = other.GetComponent<ItemPowerUp>().ItemEffect;
		}
		if (other.tag == "Pivot"){
			var rp = other.GetComponent<RotationPivot>();
			rp.MakeRotation();
		}
	}
	
	void OnTriggerExit(Collider other){
		if (other.tag == "Pivot") {
			var o = other.GetComponent<RotationPivot>();
			if (o.RotationDirection == E_Direction.LEFT){
				o.RotationDirection = E_Direction.RIGHT;
			} else if (o.RotationDirection == E_Direction.RIGHT){
				o.RotationDirection = E_Direction.LEFT;
			}
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit){
		if (hit.transform.tag == "Pushable" && CurrentPlayerState == E_PlayerState.PUSH){
			var p = hit.transform.GetComponent<Rigidbody>();
			p.AddForce(Input.GetAxis("Horizontal") * hit.transform.right * 20);
		}
	}

    public void HitByEnemy(){
		if (CurrentPlayerState == E_PlayerState.NOTHING && !_god){
			PlayerDie();
		} else {
			PlayerDie();
			//  CurrentPlayerState = E_PlayerState.NOTHING;
			//  StartCoroutine(GodMod(1.5f));
		}
    }

    private IEnumerator GodMod(float t){
		_god = true;
		if (t == 0){
			yield return new WaitForSeconds(0);
		} else {
			yield return new WaitForSeconds(t);
			_god = false;
		}
    }

    private void PlayerDie() {
		Application.LoadLevel(Application.loadedLevel);
    }
}