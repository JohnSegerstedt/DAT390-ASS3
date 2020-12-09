using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour {


	public GamePiece gamePiece;
	public float currentHealth;
	public float maxHealth;

	public void Initialize(GamePiece newGamePiece, float newHealth){
		gamePiece = newGamePiece;
		currentHealth = newHealth;
		maxHealth = newHealth;
	}

	public void ApplyDamage(float damage){
		if(currentHealth == Mathf.Infinity) return;
		currentHealth -= damage;
		if(currentHealth <= 0) gamePiece.Deactive();
	}

	public void Heal(float healingAmount){
		if(currentHealth == Mathf.Infinity) return;
		currentHealth = Mathf.Min(currentHealth+healingAmount, maxHealth);
	}
}
