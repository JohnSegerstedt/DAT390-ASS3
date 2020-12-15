using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverBehaviour : MonoBehaviour {
    
	[SerializeField] private List<GamePieceTag> possibleTargets;

	private GamePiece gamePiece;
    private List<string> possibleTargetsString = new List<string>();
	private ProgramManager programManager;

	public void Start() {
        gamePiece = gameObject.GetComponent<GamePiece>();
        foreach (GamePieceTag gamePieceTag in possibleTargets) possibleTargetsString.Add(gamePieceTag.ToString());
		programManager = ProgramManager.Instance;
	}

	private void OnTriggerEnter(Collider collider) {
		if(possibleTargetsString.Contains(collider.transform.tag))
            programManager.SetGameOver();
	}

}
