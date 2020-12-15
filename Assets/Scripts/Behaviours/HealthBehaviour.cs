using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(GamePiece))]
public class HealthBehaviour : MonoBehaviour {

	[SerializeField] private float currentHealth;
	[SerializeField] private float maxHealth;
    [SerializeField] private bool scoreOnDeath;
	
	private GamePiece gamePiece;

	public void Start() {
		gamePiece = gameObject.GetComponent<GamePiece>();
		currentHealth = maxHealth;
	}


	public bool ApplyDamage(float damage){
		if(currentHealth == Mathf.Infinity)
            return false;
		currentHealth -= damage;
        if (currentHealth <= 0)
        {
            gamePiece.Deactive();
            if (scoreOnDeath)
            {
                ScoreCounter.score += 1;
            }
            return true;
        }
        return false;
	}

	public void Heal(float healingAmount){
		if(currentHealth == Mathf.Infinity) return;
		currentHealth = Mathf.Min(currentHealth+healingAmount, maxHealth);
	}
}
