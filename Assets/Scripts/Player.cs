//using System;
using System;
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
	
	public E_Direction NextRotationDirection = E_Direction.NONE;
	public  bool RotationRunning;


    //private GameObject _camera;

    // Use this for initialization
    void Start () {
		//_camera = GameObject.Find("Main Camera");
	}

    // Update is called once per frame
    void Update() {
        CurrentTimeLevel += Time.deltaTime;

        if (RotationRunning) {
            transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, 25 * Time.deltaTime);
        }
    }
	
	void OnTriggerEnter(Collider other){
		if (other.tag == "Item"){
			CurrentPlayerState = other.GetComponent<ItemPowerUp>().ItemEffect;
		}
		if (other.tag == "Pivot"){
			// useless tmp
		}
	}
	
	void OnTriggerExit(Collider other){
		return;
		if (other.tag == "Pivot"){
			transform.rotation = _targetRotation;
			transform.position = new Vector3(other.transform.localPosition.x, transform.localPosition.x, other.transform.localPosition.z);
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit){
		if (hit.transform.tag == "Pushable" && CurrentPlayerState == E_PlayerState.PUSH){
			var p = hit.transform.GetComponent<Rigidbody>();
			p.AddForce(Input.GetAxis("Horizontal") * hit.transform.right * 20);
		}
	}

	public void FixedUpdate(){

	}

	private Quaternion _targetRotation;

    public object TargetRotation { get{return _targetRotation;} }

    public void ExecRotation(){
		RotationRunning = true;
		switch (NextRotationDirection) {
			case E_Direction.LEFT:
			_targetRotation = transform.rotation * new Quaternion(0, -Mathf.Sqrt(0.5f), 0, Mathf.Sqrt(0.5f));
			NextRotationDirection = E_Direction.NONE; 
			break;
			case E_Direction.RIGHT:
			_targetRotation = transform.rotation * new Quaternion(0, Mathf.Sqrt(0.5f), 0, Mathf.Sqrt(0.5f));
			NextRotationDirection = E_Direction.NONE;
			break;
			case E_Direction.NONE:
			break;
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