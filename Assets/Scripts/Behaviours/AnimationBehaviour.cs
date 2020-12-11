using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GamePiece))]
public class AnimationBehaviour : Observer
{
    private static readonly int ShootAnimParam = Animator.StringToHash("shoot");
    private static readonly int EatAnimParam = Animator.StringToHash("eating");
    private static readonly int MoveAnimParam = Animator.StringToHash("walking");

    [SerializeField] private Animator animator;

    private GamePiece gamePiece;

	public void Awake() {
		gamePiece = gameObject.GetComponent<GamePiece>();
        gamePiece.AddObserver(this);
    }

	override public void HandleEvent(EventEnum eventEnum){
		switch (eventEnum){
			case EventEnum.ATTACK:
				animator.SetTrigger(ShootAnimParam);
				break;
            case EventEnum.ATTACKING:
                animator.SetBool(EatAnimParam, true);
                break;
            case EventEnum.NOT_ATTACKING:
                animator.SetBool(EatAnimParam, false);
                break;
            case EventEnum.START_MOVING:
                animator.SetBool(MoveAnimParam, true);
                break;
            case EventEnum.STOP_MOVING:
                animator.SetBool(MoveAnimParam, false);
                break;
            default:
				break;
      }
   }
}
