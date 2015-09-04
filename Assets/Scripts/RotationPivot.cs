using UnityEngine;
using System.Collections;

public class RotationPivot : MonoBehaviour {
	
	public E_Direction RotationDirection;
	//private GameObject _camera;
	private GameObject _player;
	static private bool _rotationRunning = false;
	
	//private float _wValue = 1;
	//private float _xValue = 0;
	//private float _yValue = 0;
	//private float _zValue = 0;
	
	public Quaternion _target;
	
	void Start(){
		//_camera = GameObject.Find("Main Camera");
		_player = GameObject.Find("Player");
		
		
	}

	private Quaternion _initRotation;
	private Coroutine _curCoroutine;
    static private bool _already;

    public void MakeRotation(){
		if (_rotationRunning == true){
			_rotationRunning = false;
			transform.rotation = _target;
			_player.transform.parent = null;
			if (_curCoroutine != null){
				StopCoroutine(_curCoroutine);
			}
		}
		_initRotation = transform.rotation;
		_rotationRunning = true;
		//_camera.GetComponent<CameraPlayer>().GoRot = true;
		//_camera.transform.parent = transform;

		
		if (RotationDirection == E_Direction.LEFT){
			_target = _initRotation * new Quaternion(0, -Mathf.Sqrt(0.5f), 0, Mathf.Sqrt(0.5f));
		} else if (RotationDirection == E_Direction.RIGHT) {
			//_target = Quaternion.identity;
			_target = _initRotation * new Quaternion(0, Mathf.Sqrt(0.5f), 0, Mathf.Sqrt(0.5f));
		}
		_player.transform.parent = transform;
		_curCoroutine = StartCoroutine(StopRotation());
		_already = true;
	}
	
	IEnumerator StopRotation(){
		if (_already){
			yield return null;
		}
		yield return new WaitForSeconds(1f);
		_rotationRunning = false;
		//_camera.GetComponent<CameraPlayer>().GoRot = false;
		transform.rotation = _target;
		//_camera.transform.parent = null;
		_player.transform.parent = null;
		_already = false;
	}
	
	void Update(){	

		if (_rotationRunning == true){
			//transform.rotation = Quaternion.Slerp(transform.rotation, _target, Time.deltaTime * 1.5f);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, _target, Time.deltaTime * 5);
		}
	}
}
