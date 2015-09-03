//using System;
using UnityEngine;
//using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class EnemyDog : MonoBehaviour {

	public float Speed = 2f;

	public E_Direction StartDirection = E_Direction.LEFT;
	private E_Direction _currentDirection;
	private CharacterController _cc;
	
	private Vector3 _v;
	
	// Check Cliff
	public bool Smart = true;
	private bool _isGrounded;
	public Transform GroundCheckA;
	public Transform GroundCheckB;

	void Start () {
		_currentDirection = StartDirection;
		_cc = GetComponent<CharacterController>();
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
		if (Smart){
			CheckCliff();
		}
		CheckDirection();
		ApplyDirection();
    }

    void CheckCliff() {
		_isGrounded = Physics.Linecast(transform.position, GroundCheckA.position, 1 << LayerMask.NameToLayer("Ground"));
		if (_isGrounded == false){
			SwitchDirection();
			return;
		}
		_isGrounded = Physics.Linecast(transform.position, GroundCheckB.position, 1 << LayerMask.NameToLayer("Ground"));
		if (_isGrounded == false){
			SwitchDirection();
			return;
		}
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
