using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartCanvas : Observer {

	[SerializeField] private GameObject gameOverBanner;
	
	private ProgramManager programManager;

	private void Start() {
		gameOverBanner.SetActive(false);
		ProgramManager.Instance.AddObserver(this);
	}

	override public void HandleEvent(EventEnum eventEnum){
		switch (eventEnum){
			case EventEnum.GAME_OVER:
				gameOverBanner.SetActive(true);
				break;
            default:
				break;
      }
	}

	public void RestartButton(){
		ProgramManager.Instance.RestartGame();
	}
}
