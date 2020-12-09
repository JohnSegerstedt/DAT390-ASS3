using System.Collections.Generic;
using UnityEngine;

public class Pea : GamePiece {

	private MovementBehaviour movementBehaviour;
	private TemporalBehaviour temporalBehaviour;
	private ProjectileBehaviour projectileBehaviour;


	public void Start() {
		movementBehaviour = gameObject.AddComponent<MovementBehaviour>() as MovementBehaviour;	
		temporalBehaviour = gameObject.AddComponent<TemporalBehaviour>() as TemporalBehaviour;	
		projectileBehaviour = gameObject.AddComponent<ProjectileBehaviour>() as ProjectileBehaviour;	
		movementBehaviour.Initialize(this, 40f, transform.forward);
		temporalBehaviour.Initialize(this, 4f);
		projectileBehaviour.Initialize(this, 10f, new List<string>(){"Zombie"});
	}



}
