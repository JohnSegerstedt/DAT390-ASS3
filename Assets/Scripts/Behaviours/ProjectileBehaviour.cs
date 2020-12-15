using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GamePiece))]
public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject collisionParticles;
    [SerializeField] private float damage;
    [SerializeField] private List<GamePieceTag> possibleTargets; // setting this ensures no "friendly fire"

    private GamePiece gamePiece;
    private List<string> possibleTargetsString = new List<string>();

    public void Start()
    {
        gamePiece = GetComponent<GamePiece>();
        foreach (GamePieceTag gamePieceTag in possibleTargets) possibleTargetsString.Add(gamePieceTag.ToString());
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!IsLegalTarget(collider.gameObject)) return;
        collider.gameObject.GetComponent<HealthBehaviour>().ApplyDamage(damage);
        if (collisionParticles)
        {
            Instantiate(collisionParticles, transform.position, transform.rotation);
        }
        gamePiece.Deactive();
    }

    private bool IsLegalTarget(GameObject target)
    {
        if (!target) return false;
        if (target == gameObject) return false;
        if (!target.gameObject.activeSelf) return false;
        if (possibleTargets != null && possibleTargets.Count > 0 && !possibleTargetsString.Contains(target.transform.tag)) return false;
        if (target.GetComponent<HealthBehaviour>() == null) return false;
        return true;
    }

}

