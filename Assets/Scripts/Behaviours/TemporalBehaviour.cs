using UnityEngine;

public class TemporalBehaviour : MonoBehaviour {

	public GamePiece gamePiece;
    public float lifeDuration;

	private float lifeTime;

	public void Initialize(GamePiece newGamePiece, float newLifeDuration){
		gamePiece = newGamePiece;
		lifeDuration = newLifeDuration;
	}

    void OnEnable(){
        lifeTime = lifeDuration;
    }

    void Update(){
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)  gamePiece.Deactive();
    }
}
