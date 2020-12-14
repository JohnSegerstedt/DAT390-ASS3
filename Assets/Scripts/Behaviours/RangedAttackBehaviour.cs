using System;
using System.Collections.Generic;
using UnityEngine;

// todo, move Subject to a Behaviour class
[RequireComponent(typeof(GamePiece))]
public class RangedAttackBehaviour : MonoBehaviour
{

    [SerializeField] private PoolObject projectileType;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private string[] possibleTargetLayers;

    private GamePiece gamePiece;
    private float currentCooldown = 0f;
    private GameObject target;
    private int possibleTargetLayersMask;

    public void Start()
    {
        gamePiece = gameObject.GetComponent<GamePiece>();
        possibleTargetLayersMask = LayerMask.GetMask(possibleTargetLayers);
    }

    public void Update()
    {
        if (currentCooldown < attackCooldown) currentCooldown += Time.deltaTime;
        else AttackTarget();
    }

    public void Attack()
    {
        if (IsLegalTarget(target))
        {
            GameObject projectile = ObjectPoolManager.Instance.GetGameObject(projectileType);
            if (!projectile) return;
            projectile.transform.position = shootingPoint.position;
            projectile.transform.forward = gamePiece.transform.forward;
            currentCooldown = 0f;
            gamePiece.NotifyObservers(EventEnum.ATTACK);
        }
    }


    private void AttackTarget()
    {
        AcquireTarget();
        if (target == null) return;
        Attack();
    }


    private void AcquireTarget()
    {
        target = null;
        if (!Physics.Raycast(shootingPoint.transform.position, gamePiece.gameObject.transform.forward,
            out var hit,
            attackRange, possibleTargetLayersMask))
            return;

        GameObject potentialTarget = hit.transform.gameObject;
        if (IsLegalTarget(potentialTarget))
        {
            target = potentialTarget;
            return;
        }
    }


    private bool IsLegalTarget(GameObject potentialTarget)
    {
        if (!potentialTarget) return false;
        if (!potentialTarget.gameObject.activeSelf) return false;
        return true;
    }
}
