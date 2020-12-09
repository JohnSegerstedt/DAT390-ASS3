using UnityEngine;

public class MovementBehaviour : MonoBehaviour {

	public GamePiece gamePiece;
	public float movementSpeed;
	public Vector3 movementDirection;

	private bool move;

	public void Initialize(GamePiece newGamePiece, float newMovementSpeed, Vector3 newMovementDirection){
		Initialize(newGamePiece, newMovementSpeed, newMovementDirection, true);
	}

	public void Initialize(GamePiece newGamePiece, float newMovementSpeed, Vector3 newMovementDirection, bool newMove){
		gamePiece = newGamePiece;
		movementSpeed = newMovementSpeed;
		movementDirection = newMovementDirection;
		move = newMove;
	}

	void Update(){
        if(move) gamePiece.transform.position += movementDirection * movementSpeed * Time.deltaTime;
    }

	public void SetMove(bool newMove){
		move = newMove;
	}
}
