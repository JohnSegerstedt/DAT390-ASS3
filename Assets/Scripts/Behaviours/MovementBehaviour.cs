using UnityEngine;

[RequireComponent(typeof(GamePiece))]
public class MovementBehaviour : Observer {

	[SerializeField] private float movementSpeed;
	[SerializeField] private bool moveOnStart = true;
	[SerializeField] private Subject[] subjects;

	[SerializeField] private Animator animator;

	private GamePiece gamePiece;
	private bool move;

	private void Start() {
		gamePiece = gameObject.GetComponent<GamePiece>();
		foreach(Subject subject in subjects) subject.AddObserver(this);
		move = moveOnStart;
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
				animator.SetTrigger("eat");
				break;
			case EventEnum.NOT_ATTACKING:
				SetMove(true);
				animator.SetTrigger("walk");
				break;
			default:
				break;
      }
   }
}
