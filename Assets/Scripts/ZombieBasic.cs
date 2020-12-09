using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBasic : GamePiece {

	private MovementBehaviour movementBehaviour;
	private HealthBehaviour healthBehaviour;


	public void Start() {
		movementBehaviour = gameObject.AddComponent<MovementBehaviour>() as MovementBehaviour;	
		movementBehaviour.Initialize(this, 10f, transform.forward);
		movementBehaviour.SetMove(false);
		healthBehaviour = gameObject.AddComponent<HealthBehaviour>() as HealthBehaviour;	
		healthBehaviour.Initialize(this, 100f);
	}


}
