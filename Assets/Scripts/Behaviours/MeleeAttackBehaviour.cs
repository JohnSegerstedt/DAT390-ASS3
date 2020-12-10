using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo, move Subject to a Behaviour class
// todo, create MeleeAttackBehaviour class
public class MeleeAttackBehaviour : Subject {

	private GamePiece gamePiece;
	private float cooldown;
	private float attackDamage;
	private List<string> possibleTargets;

	private bool isAttacking = false;
	private float currentCooldown = 0f;
	private GamePiece target;


	public void Initialize(GamePiece newGamePiece, float newCooldown, float newAttackDamage, List<string> newPossibleTargets){
		gamePiece = newGamePiece;
		cooldown = newCooldown;
		attackDamage = newAttackDamage;
		possibleTargets = newPossibleTargets;
	}

	public void Update() {
		if(currentCooldown < cooldown) currentCooldown += Time.deltaTime;
		else AttackTarget();
	}

	private void AttackTarget(){
		AcquireTarget();
		SetIsAttacking(target);
		if(target == null) return;
		Attack();
	}

	private void SetIsAttacking(bool newIsAttacking){
		isAttacking = newIsAttacking;
		NotifyObservers(newIsAttacking ? EventEnum.ATTACKING : EventEnum.NOT_ATTACKING);
	}

	public void Attack(){
		if(IsLegalTarget(target)) {
			target.GetComponent<HealthBehaviour>().ApplyDamage(attackDamage);
			currentCooldown = 0f;
		}
	}
	
	private void AcquireTarget(){
		target = null;
		RaycastHit hit;
		if(!Physics.Raycast(gamePiece.gameObject.transform.position, gamePiece.gameObject.transform.forward, out hit, 0.1f)) return;
		try{
			GamePiece potentialTarget = hit.transform.gameObject.GetComponent<GamePiece>();
			if(potentialTarget == null) return;
			if(IsLegalTarget(potentialTarget)) target = potentialTarget;
		}catch(Exception){}
	}

	
	private bool IsLegalTarget(GamePiece potentialTarget){
		if(!potentialTarget) return false;
		if(!potentialTarget.gameObject.activeSelf) return false;
		if(!possibleTargets.Contains(potentialTarget.transform.tag)) return false;
		if(potentialTarget.GetComponent<HealthBehaviour>() == null) return false;
		return true;
	}
}
