using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgramManager : Subject {

    private static ProgramManager instance;
    public static ProgramManager Instance { get { return instance; } }

	private void Awake() {
        instance = this;
	}

	private bool isGameAlive = true;
	private int gameSceneIndex = 0;

	public void SetGameOver(){
		if(isGameAlive) NotifyObservers(EventEnum.GAME_OVER);
		isGameAlive = false;
	}

	public void RestartGame(){
		SceneManager.LoadScene(gameSceneIndex); 
	}


}
