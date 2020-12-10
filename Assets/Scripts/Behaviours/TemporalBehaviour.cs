using UnityEngine;

[RequireComponent(typeof(GamePiece))]
public class TemporalBehaviour : MonoBehaviour {

    [SerializeField] private float lifeDuration;
	
	private GamePiece gamePiece;
	private float lifeTime;

	private void Start() {
		gamePiece = gameObject.GetComponent<GamePiece>();
		lifeTime = lifeDuration;
	}

    void OnEnable(){
        lifeTime = lifeDuration;
    }

    void Update(){
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)  gamePiece.Deactive();
    }
}
