using System.Collections.Generic;
using UnityEngine;

public class AnimationBehaviour : Observer {


	private Animator animator;
	private GamePiece gamePiece;

	public void Initialize(GamePiece newGamePiece, Animator newAnimator, List<Subject> subjects){
		gamePiece = newGamePiece;
		animator = newAnimator;
		foreach(Subject subject in subjects) subject.AddObserver(this);
	}


	// todo fix animation transition
	override public void HandleEvent(EventEnum eventEnum){
		switch (eventEnum){
			case EventEnum.ATTACK:
				animator.SetBool("isShooting", true);
				break;
			default:
				break;
      }
   }

}
