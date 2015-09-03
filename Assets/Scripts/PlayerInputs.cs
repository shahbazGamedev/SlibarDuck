using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
//using System.Collections;
//using System;
//using UnityEngine.UI;

public class PlayerInputs : MonoBehaviour {

	private float _inputX;
	private bool _inputJump;
	private float _instantSpeedX;
	private float _instantSpeedY;
	private CharacterController _cc;
	private Player _player;
	
	public float SetSpeedWalk = 10;
	public float SetSpeedJump = 10;
//	public float SetSpeedRun;


	// Use this for initialization
	void Start () {
		_cc = GetComponent<CharacterController>();
		_player = GetComponent<Player>();		
	}

	void CheckInputs() {
        
		_inputX = Input.GetAxis("Horizontal");
		_inputJump = Input.GetButton("Jump");
		
		//#if MOBILE_INPUT
		_inputX += CrossPlatformInputManager.GetAxis("Horizontal");
		if (!_inputJump) _inputJump = CrossPlatformInputManager.GetButton("Jump");
		//#endif

	}

	void CheckMovements(){
		_instantSpeedX = Mathf.Lerp(_instantSpeedX, _inputX * SetSpeedWalk, Time.deltaTime * 20);
		if (_inputJump && _cc.isGrounded) {
			if (_player.CurrentPlayerState == E_PlayerState.JUMP){
				_instantSpeedY = SetSpeedJump;
			} else if (_inputJump && _player.CurrentPlayerState == E_PlayerState.DESTROY) {
				DestroyFront();
			}
		}
		if(!_cc.isGrounded){
			_instantSpeedY = Mathf.Lerp(_instantSpeedY, Physics.gravity.y, 5 * Time.deltaTime);
		}
	}

    private void DestroyFront() {
		var cols = Physics.OverlapSphere(transform.position, 1);
		foreach (var c in cols){
			if (c.transform.tag == "Destroyable"){
				var dp = c.transform.GetComponent<DestroyableProp>();
				dp.AskDestroyByPlayer();
			}
		}
	}

    void ApplyMovements(){
		var v = Vector3.zero;
		v += transform.right * _instantSpeedX;
		v += transform.up * _instantSpeedY;
		_cc.Move(v * Time.deltaTime);
	}
	
	void Reinit(){
		if (_cc.isGrounded){
			_instantSpeedY = 0;	
		} 
	}
	
	void FixedUpdate () {
		Reinit();
		CheckInputs();
		CheckMovements();
		ApplyMovements();
	}
	
	void OnGui(){
		if (GUI.Button(new Rect(10,10,10,10), "toto")){
			
		}
	}
}
