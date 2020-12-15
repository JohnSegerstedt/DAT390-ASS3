using UnityEngine;

public class OutOfBoundsTrigger : MonoBehaviour {
	private void OnTriggerExit(Collider other) {
		GamePiece otherGamePiece = other.GetComponent<GamePiece>();
		if(otherGamePiece != null) otherGamePiece.Deactive();
	}
}
