using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBasic : GamePiece {

	private MovementBehaviour movementBehaviour;
	private HealthBehaviour healthBehaviour;
	private MeleeAttackBehaviour meleeAttackBehaviour;


	public void Start() {
		movementBehaviour = gameObject.AddComponent<MovementBehaviour>() as MovementBehaviour;
		healthBehaviour = gameObject.AddComponent<HealthBehaviour>() as HealthBehaviour;		
		meleeAttackBehaviour = gameObject.AddComponent<MeleeAttackBehaviour>() as MeleeAttackBehaviour;		
		movementBehaviour.Initialize(this, 1f, transform.forward);
		healthBehaviour.Initialize(this, 100f);
		meleeAttackBehaviour.Initialize(this, 1f, 10f, new List<string>(){"Plant"}); // todo, make tags into an enum
		meleeAttackBehaviour.AddObserver(movementBehaviour);
	}


}
