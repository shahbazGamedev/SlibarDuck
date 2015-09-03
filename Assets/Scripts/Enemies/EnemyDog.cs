//using System;
using System;
using UnityEngine;
//using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class EnemyDog : MonoBehaviour {

	public float Speed = 2f;
	public bool Smart = true;

	public E_Direction StartDirection = E_Direction.LEFT;
	private E_Direction _currentDirection;
	private CharacterController _cc;
	
	private Vector3 _v;
	
	private Collider[] _colliders; 

	void Start () {
		_currentDirection = StartDirection;
		_cc = GetComponent<CharacterController>();
		_colliders = GetComponentsInChildren<Collider>();
	}
	
	void Update () {
	}
	
	public void SetVelocityDirection(){
		switch (_currentDirection) {
			case E_Direction.RIGHT:
				_v = transform.right*Speed;
				break;
			case E_Direction.LEFT:
				_v = transform.right*Speed*-1;
				break;
			case E_Direction.NONE:
				_v = Vector3.zero;
				break;
		}
	}
	
	void CheckDirection() {
		SetVelocityDirection();
		if (!_cc.isGrounded){
			_v += Physics.gravity;
		}
	}
	
	public void SwitchDirection(){
		if (_currentDirection == E_Direction.LEFT) {
			_currentDirection = E_Direction.RIGHT;
		} else if (_currentDirection == E_Direction.RIGHT){
			_currentDirection = E_Direction.LEFT;
		}
	}
	
	void FixedUpdate() {
		CheckCliff();
		CheckDirection();
		ApplyDirection();
    }

    void CheckCliff() {
		var c = Physics.OverlapSphere(transform.position - Vector3.up + Vector3.right * -1, 0.25f);
		var d = Physics.OverlapSphere(transform.position - Vector3.up + Vector3.right * -1, 0.25f);
		
    }

    void ApplyDirection() {
		_cc.Move(_v * Time.deltaTime);	
    }
	
	void OnControllerColliderHit(ControllerColliderHit hit){
		if (hit.transform.tag == "Wall"){
			SwitchDirection();
		} else if (hit.transform.tag == "Player") {
			var p = hit.transform.GetComponent<Player>();
			p.HitByEnemy();
		}
	}
}
