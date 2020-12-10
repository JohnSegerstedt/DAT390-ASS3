using UnityEngine;

[RequireComponent(typeof(GamePiece))]
public class MovementBehaviour : Observer {

	[SerializeField] private float movementSpeed;
	[SerializeField] private Subject[] subjects;
	
	private GamePiece gamePiece;
	private bool move = true;

	private void Start() {
		gamePiece = gameObject.GetComponent<GamePiece>();
		foreach(Subject subject in subjects) subject.AddObserver(this);
	}

	void Update(){
        if(move) gamePiece.transform.position += gamePiece.transform.forward * movementSpeed * Time.deltaTime;
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
