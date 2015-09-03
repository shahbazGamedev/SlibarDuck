using UnityEngine;
//using System.Collections;

public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T> {

	private static T _instance = null;

	public static T Instance {
		get {
			if (_instance ==null){
				_instance = GameObject.FindObjectOfType(typeof(T)) as T;
				if (_instance==null){
					_instance = new GameObject("Singleton of" + typeof(T).ToString(), typeof(T)).GetComponent<T>();
					_instance.Init();
				}
			}
			return _instance;

		}
	}

	void Awake() {
		if (_instance == null) {
			_instance = this as T;
			DontDestroyOnLoad (this);
		} else {
			if (this != _instance){
				Destroy(this.gameObject);
			}
		}
	}

	public virtual void Init(){
	}

	void OnApplicationQuit(){
		_instance = null;
	}
}
