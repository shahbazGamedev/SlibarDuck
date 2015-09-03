using UnityEngine;
//using System.Collections;

public enum E_GameState {
	INGAME,
	PAUSE,
	MENU
};

public class GameManager : SingletonBehaviour<GameManager> {
	
	public E_GameState CurrentGameState;
	private Player _player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (_player == null) {
		}
	}		

	
	void InitPlayer(){
		_player = GameObject.Find("Player").GetComponent<Player>();
	}
}
