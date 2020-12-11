using UnityEngine;

[RequireComponent(typeof(GamePiece))]
public class MovementBehaviour : Observer {
    [SerializeField] private float movementSpeed;
	[SerializeField] private bool moveOnStart = true;

	private GamePiece gamePiece;
	private bool moving;
    private bool attacking;

	private void Awake() {
		gamePiece = gameObject.GetComponent<GamePiece>();
        gamePiece.AddObserver(this);
	}

    private void Start()
    {
        SetMove(moveOnStart);
    }

    void Update(){
        if(moving) gamePiece.transform.position += gamePiece.transform.forward * movementSpeed * Time.deltaTime;
    }

	public void SetMove(bool newMove){
		moving = newMove;
        gamePiece.NotifyObservers(moving ? EventEnum.START_MOVING : EventEnum.STOP_MOVING);
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
