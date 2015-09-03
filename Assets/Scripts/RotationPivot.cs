using UnityEngine;
using System.Collections;

public class RotationPivot : MonoBehaviour {
	
	public E_Direction RotationDirection;
	//private GameObject _camera;
	private GameObject _player;
	private bool GoRotation = false;
	
	//private float _wValue = 1;
	//private float _xValue = 0;
	//private float _yValue = 0;
	//private float _zValue = 0;
	
	public Quaternion _target;
	
	void Start(){
		//_camera = GameObject.Find("Main Camera");
		_player = GameObject.Find("Player");
		
		
	}

    public void MakeRotation(){
		//_camera.GetComponent<CameraPlayer>().GoRot = true;
		//_camera.transform.parent = transform;

		if (RotationDirection == E_Direction.LEFT){
			_target = new Quaternion(0, -Mathf.Sqrt(0.5f), 0, Mathf.Sqrt(0.5f));
		} else if (RotationDirection == E_Direction.RIGHT) {
			_target = Quaternion.identity;
		}
		_player.transform.parent = transform;
		GoRotation = true;
		StopCoroutine(StopRotation());
		StartCoroutine(StopRotation());
	}
	
	IEnumerator StopRotation(){
		yield return new WaitForSeconds(1f);
		GoRotation = false;
		//_camera.GetComponent<CameraPlayer>().GoRot = false;
		transform.rotation = _target;
		//_camera.transform.parent = null;
		_player.transform.parent = null;
	}
	
	void Update(){	

		if (GoRotation == true){
			//transform.rotation = Quaternion.Slerp(transform.rotation, _target, Time.deltaTime * 1.5f);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, _target, Time.deltaTime * 5);
		}
	}
}
