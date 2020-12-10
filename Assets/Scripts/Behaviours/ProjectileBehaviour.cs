using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {


	private GamePiece gamePiece;
	private float damage;
	private List<string> possibleTargets; // setting this ensures no "friendly fire"
	
	public void Initialize(GamePiece newGamePiece, float newDamage, List<string> newPossibleTargets){
		gamePiece = newGamePiece;
		damage = newDamage;
		possibleTargets = newPossibleTargets;
	}

	private void OnTriggerEnter(Collider collider) {
		if(!IsLegalTarget(collider.gameObject)) return;
		collider.gameObject.GetComponent<HealthBehaviour>().ApplyDamage(damage);
		gamePiece.Deactive();
	}

	private bool IsLegalTarget(GameObject target){
		if(!target) return false;
		if(target == gameObject) return false;
		if(!target.gameObject.activeSelf) return false;
		if(possibleTargets != null && possibleTargets.Count > 0 && !possibleTargets.Contains(target.transform.tag)) return false;
		if(target.GetComponent<HealthBehaviour>() == null) return false;
		return true;
	}

}

