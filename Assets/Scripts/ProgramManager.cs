using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgramManager : Subject {

    private static ProgramManager instance;
    public static ProgramManager Instance {
        get {
            if (instance == null)
            {
                instance = FindObjectOfType<ProgramManager>();
                if (instance == null)
                {
                    var obj = new GameObject("Program Manager");
                    instance = obj.AddComponent<ProgramManager>();
                }
            }
            return instance;
        }
    }

	private void Awake() {
        if (instance)
        {
            DestroyImmediate(this);
        }
        else
        {
            instance = this;
        }
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
