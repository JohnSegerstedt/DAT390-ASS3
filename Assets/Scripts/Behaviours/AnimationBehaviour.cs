using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GamePiece))]
public class AnimationBehaviour : Observer {

	[SerializeField] private Animator animator;
	[SerializeField] private Subject[] subjects; // todo, perhaps change this

	
	private GamePiece gamePiece;

	public void Start() {
		gamePiece = gameObject.GetComponent<GamePiece>();
		foreach(Subject subject in subjects) subject.AddObserver(this);
	}


	override public void HandleEvent(EventEnum eventEnum){
		switch (eventEnum){
			case EventEnum.ATTACK:
				animator.SetTrigger("shoot");
				break;
			default:
				break;
      }
   }

}
