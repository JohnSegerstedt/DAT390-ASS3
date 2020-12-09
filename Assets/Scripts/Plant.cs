using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Plant : GamePiece {


	private RangedAttackBehaviour rangedAttackBehaviour;
	private HealthBehaviour healthBehaviour;
	private AnimationBehaviour animationBehaviour;
	

    void Start() {
		rangedAttackBehaviour = gameObject.AddComponent<RangedAttackBehaviour>() as RangedAttackBehaviour;
		healthBehaviour = gameObject.AddComponent<HealthBehaviour>() as HealthBehaviour;
		animationBehaviour = gameObject.AddComponent<AnimationBehaviour>() as AnimationBehaviour;
		rangedAttackBehaviour.Initialize(this, 200, 1f, new Vector3(0f, 3.5f, 0f), new List<string>(){"Zombie"});
		healthBehaviour.Initialize(this, 100f);
		animationBehaviour.Initialize(this, GetComponentInChildren<Animator>(), new List<Subject>(){rangedAttackBehaviour}); 
    }
}
