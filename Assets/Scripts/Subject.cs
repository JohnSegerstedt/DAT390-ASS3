using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour {

	private List<Observer> observers = new List<Observer>();

	public void AddObserver(Observer observer){
		observers.Add(observer);
	}

	public void RemoveObserver(Observer observer){
		if(observers.Contains(observer)) observers.Remove(observer);
	}

	public void NotifyObservers(EventEnum eventEnum){
		foreach(Observer observer in observers){
			observer.HandleEvent(eventEnum);
		}
	}
}
