using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo, move Subject to a Behaviour class
// todo, create MeleeAttackBehaviour class
public class RangedAttackBehaviour : Subject {

	private GamePiece gamePiece;
	private float attackRange;
	private float cooldown;
	private Vector3 shootOffset; // toto - create shooting point object instead
	private List<string> possibleTargets;

	private float currentCooldown = 0f;
	private GamePiece target;


	public void Initialize(GamePiece newGamePiece, float newAttackRange, float newCooldown, Vector3 newShootOffset, List<string> newPossibleTargets){
		gamePiece = newGamePiece;
		attackRange = newAttackRange;
		cooldown = newCooldown;
		shootOffset = newShootOffset;
		possibleTargets = newPossibleTargets;
	}

	public void Update() {
		if(currentCooldown < cooldown){
			currentCooldown += Time.deltaTime;
			return;
		}else{
			currentCooldown = 0f;
			AttackTarget();
		}
	}


	public void Attack(){
		if(IsLegalTarget(target)) {
			GameObject peaObject = ObjectPoolingManager.Instance.GetGameObject(); // todo, make projectile type abstract
            peaObject.transform.position = gamePiece.transform.position + shootOffset;
            peaObject.transform.forward = gamePiece.transform.forward;
			NotifyObservers(EventEnum.ATTACK);
		}
	}


	private void AttackTarget(){
		if(target == null) AcquireTarget();
		if(target == null) return;
		Attack();
	}

	
	private void AcquireTarget(){
		if(IsLegalTarget(target)) return;
		else foreach(GamePiece iter_gamePiece in GamePiece.gamePieces) if(IsLegalTarget(iter_gamePiece)){
			target = iter_gamePiece;
			return;
			}
		target = null;
	}

	
	private bool IsLegalTarget(GamePiece potentialTarget){
		if(!potentialTarget) return false;
		if(!potentialTarget.gameObject.activeSelf) return false;
		if(!possibleTargets.Contains(potentialTarget.transform.tag)) return false;
		if((potentialTarget.transform.position - gamePiece.transform.position).magnitude > attackRange) return false; // todo, replace with ray cast
		if(potentialTarget.GetComponent<HealthBehaviour>() == null) return false;
		return true;
	}
}
