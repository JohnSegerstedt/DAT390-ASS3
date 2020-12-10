using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GamePiece))]
public class ProjectileBehaviour : MonoBehaviour {

	[SerializeField] private float damage;
	[SerializeField] private List<string> possibleTargets; // setting this ensures no "friendly fire"

	private GamePiece gamePiece;

	public void Start() {
		gamePiece = gameObject.GetComponent<GamePiece>();
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

