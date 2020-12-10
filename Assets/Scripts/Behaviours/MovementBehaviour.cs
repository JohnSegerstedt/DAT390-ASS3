using UnityEngine;

public class MovementBehaviour : Observer {

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

	override public void HandleEvent(EventEnum eventEnum){
		switch (eventEnum){
			case EventEnum.ATTACKING:
				SetMove(false);
				break;
			case EventEnum.NOT_ATTACKING:
				SetMove(true);
				break;
			default:
				break;
      }
   }
}
