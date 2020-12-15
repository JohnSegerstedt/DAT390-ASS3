using UnityEngine;

public class OutOfBoundsTrigger : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		GamePiece otherGamePiece = other.GetComponent<GamePiece>();
		if(otherGamePiece != null) otherGamePiece.Deactive();
	}

	private void OnCollisionEnter(Collision collision) {
		GamePiece otherGamePiece = collision.gameObject.GetComponent<GamePiece>();
		if(otherGamePiece != null) otherGamePiece.Deactive();
	}
}
