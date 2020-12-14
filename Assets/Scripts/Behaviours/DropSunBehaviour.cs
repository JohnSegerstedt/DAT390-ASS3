using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSunBehaviour : MonoBehaviour
{
    public float throwForce = 1.5f;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.right * throwForce, ForceMode.Impulse);
    }

}
