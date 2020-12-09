using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Observer : MonoBehaviour {

	
	abstract public void HandleEvent(EventEnum eventEnum);

}
