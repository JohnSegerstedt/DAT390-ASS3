using System;
using System.Collections.Generic;
using UnityEngine;

// todo, move Subject to a Behaviour class
[RequireComponent(typeof(GamePiece))]
public class RangedAttackBehaviour : MonoBehaviour {

	[SerializeField] private PoolObject projectileType;
	[SerializeField] private float attackRange;
	[SerializeField] private float attackCooldown;
	[SerializeField] private Transform shootingPoint;
	[SerializeField] private List<GamePieceTag> possibleTargets;

	private GamePiece gamePiece;
	private float currentCooldown = 0f;
	private GamePiece target;
	private List<string> possibleTargetsString = new List<string>();

	public void Start() {
		gamePiece = gameObject.GetComponent<GamePiece>();
		foreach(GamePieceTag gamePieceTag in possibleTargets) possibleTargetsString.Add(gamePieceTag.ToString());
	}

	public void Update() {
		if(currentCooldown < attackCooldown) currentCooldown += Time.deltaTime;
		else AttackTarget();
	}

	public void Attack(){
		if(IsLegalTarget(target)) {
			GameObject projectile = ObjectPoolManager.Instance.GetGameObject(projectileType);
            if (!projectile) return;
            projectile.transform.position = shootingPoint.position;
            projectile.transform.forward = gamePiece.transform.forward;
			currentCooldown = 0f;
			gamePiece.NotifyObservers(EventEnum.ATTACK);
		}
	}


	private void AttackTarget(){
		AcquireTarget();
		if(target == null) return;
		Attack();
	}

	
	private void AcquireTarget(){
		target = null;
		RaycastHit[] hits;
		hits = Physics.RaycastAll(shootingPoint.transform.position, gamePiece.gameObject.transform.forward, attackRange);
		if(hits.Length == 0) return;
		foreach(RaycastHit hit in hits){
			try{
				GamePiece potentialTarget = hit.transform.gameObject.GetComponent<GamePiece>();
				if(potentialTarget == null) return;
				if(IsLegalTarget(potentialTarget)){
					target = potentialTarget;
					return;
				}
			}catch(Exception){}
		}

	}

	
	private bool IsLegalTarget(GamePiece potentialTarget){
		if(!potentialTarget) return false;
		if(!potentialTarget.gameObject.activeSelf) return false;
		if(!possibleTargetsString.Contains(potentialTarget.transform.tag)) return false;
		if(potentialTarget.GetComponent<HealthBehaviour>() == null) return false;
		return true;
	}
}
