using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo, make more like Unity
public class Plant : GamePiece {

	public GameObject shootingPoint;

	private RangedAttackBehaviour rangedAttackBehaviour;
	private HealthBehaviour healthBehaviour;
	private AnimationBehaviour animationBehaviour;
	

    void Start() {
		rangedAttackBehaviour = gameObject.AddComponent<RangedAttackBehaviour>() as RangedAttackBehaviour;
		healthBehaviour = gameObject.AddComponent<HealthBehaviour>() as HealthBehaviour;
		animationBehaviour = gameObject.AddComponent<AnimationBehaviour>() as AnimationBehaviour;
		rangedAttackBehaviour.Initialize(this, 20, 2f, shootingPoint.transform, new List<string>(){"Zombie"});
		healthBehaviour.Initialize(this, 100f);
		animationBehaviour.Initialize(this, GetComponentInChildren<Animator>(), new List<Subject>(){rangedAttackBehaviour}); 
    }
}
