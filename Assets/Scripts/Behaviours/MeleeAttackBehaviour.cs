using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo, move Subject to a Behaviour class
[RequireComponent(typeof(GamePiece))]
public class MeleeAttackBehaviour : MonoBehaviour {

	[SerializeField] private float cooldown;
	[SerializeField] private float attackDamage;
	[SerializeField] private List<GamePieceTag> possibleTargets;

	private GamePiece gamePiece;
	private bool isAttacking = false;
	private float currentCooldown = 0f;
	private GamePiece target;
	private List<string> possibleTargetsString = new List<string>();

	public void Start() {
		gamePiece = gameObject.GetComponent<GamePiece>();
		foreach(GamePieceTag gamePieceTag in possibleTargets) possibleTargetsString.Add(gamePieceTag.ToString());

	}

	public void Update() {
        if (currentCooldown < cooldown) currentCooldown += Time.deltaTime;
        else if (isAttacking) Attack();
	}

    private void OnTriggerEnter(Collider other)
    {
        currentCooldown = cooldown;
        SetIsAttacking(true);
    }

    private void OnTriggerExit(Collider other)
    {
        SetIsAttacking(false);
    }

	private void SetIsAttacking(bool newIsAttacking){
		if(newIsAttacking == isAttacking) return;
		isAttacking = newIsAttacking;
		gamePiece.NotifyObservers(newIsAttacking ? EventEnum.ATTACKING : EventEnum.NOT_ATTACKING);
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
		if(!possibleTargetsString.Contains(potentialTarget.transform.tag)) return false;
		if(potentialTarget.GetComponent<HealthBehaviour>() == null) return false;
		return true;
	}
}
