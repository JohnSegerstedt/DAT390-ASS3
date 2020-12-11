using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : Subject{
    public delegate void OnDeathAction();
    public OnDeathAction OnDeath;

	public static List<GamePiece> gamePieces = new List<GamePiece>();

    private void OnEnable() {
		NotifyObservers(EventEnum.SPAWN);
		gamePieces.Add(this);
	}
	
	public void Deactive(){
		NotifyObservers(EventEnum.DEATH);
		gamePieces.Remove(this);
		gameObject.SetActive(false);
        OnDeath?.Invoke();
	}
}
