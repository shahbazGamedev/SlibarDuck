using UnityEngine;
using System.Collections;

// _target = _initRotation * new Quaternion(0, -Mathf.Sqrt(0.5f), 0, Mathf.Sqrt(0.5f));

public class RotationPivot : MonoBehaviour {
	
	Player _player;
	
	void Start(){
		_player = GameObject.Find("Player").GetComponent<Player>();
	}

	void OnTriggerEnter(Collider collider){
		_player.SendMessage("ExecRotation");
	}

}
