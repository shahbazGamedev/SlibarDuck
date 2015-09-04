using UnityEngine;
using System.Collections;

public class SetDirectionPlayer : MonoBehaviour {

	private Player _player;
	
	void Start(){
		_player = GameObject.Find("Player").GetComponent<Player>();
	}
	
	public E_Direction Direction;

	void OnTriggerEnter(Collider col){
		_player.NextRotationDirection = Direction;
	}
}
