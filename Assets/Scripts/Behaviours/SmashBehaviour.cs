using System.Collections.Generic;
using UnityEngine;

class SmashBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject smashedZombie;
    [SerializeField] private List<GamePieceTag> possibleTargets;

    private GamePiece gamePiece;
    private List<string> possibleTargetsString = new List<string>();

    public void Start()
    {
        gamePiece = gameObject.GetComponent<GamePiece>();
        foreach (GamePieceTag gamePieceTag in possibleTargets) possibleTargetsString.Add(gamePieceTag.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsLegalTarget(other.gameObject))
        {
            gamePiece.NotifyObservers(EventEnum.ATTACKING);
            var target = other.GetComponent<GamePiece>();
            target.GetComponent<HealthBehaviour>().Kill();
            Instantiate(smashedZombie, target.transform.position, smashedZombie.transform.rotation);
        }
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
