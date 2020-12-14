using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo, move Subject to a Behaviour class
[RequireComponent(typeof(GamePiece))]
public class MeleeAttackBehaviour : MonoBehaviour
{

    [SerializeField] private float cooldown;
    [SerializeField] private float attackDamage;
    [SerializeField] private List<GamePieceTag> possibleTargets;

    private GamePiece gamePiece;
    private bool isAttacking = false;
    private float currentCooldown = 0f;
    private GamePiece target;
    private List<string> possibleTargetsString = new List<string>();

    public void Start()
    {
        gamePiece = gameObject.GetComponent<GamePiece>();
        foreach (GamePieceTag gamePieceTag in possibleTargets) possibleTargetsString.Add(gamePieceTag.ToString());
    }

    public void Update()
    {
        if (currentCooldown < cooldown) currentCooldown += Time.deltaTime;
        else if (isAttacking) Attack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target == null && IsLegalTarget(other.gameObject))
        {
            currentCooldown = cooldown;
            target = other.GetComponent<GamePiece>();
            SetIsAttacking(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target.gameObject)
        {
            SetIsAttacking(false);
            target = null;
        }

    }

    private void SetIsAttacking(bool newIsAttacking)
    {
        if (newIsAttacking == isAttacking) return;
        isAttacking = newIsAttacking;
        gamePiece.NotifyObservers(newIsAttacking ? EventEnum.ATTACKING : EventEnum.NOT_ATTACKING);
    }

    public void Attack()
    {
        if (target.GetComponent<HealthBehaviour>().ApplyDamage(attackDamage))
        {
            SetIsAttacking(false);
            target = null;
        }
        currentCooldown = 0f;
    }

    private bool IsLegalTarget(GameObject potentialTarget)
    {
        if (!potentialTarget) return false;
        if (!potentialTarget.activeSelf) return false;
        if (!possibleTargetsString.Contains(potentialTarget.transform.tag)) return false;
        if (potentialTarget.GetComponent<HealthBehaviour>() == null) return false;
        return true;
    }
}
