using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthBehaviour))]
public class DeactivateOnHealth : MonoBehaviour
{
    public HealthBehaviour healthBehaviour;
    private float health;
    public GameObject objToDeactivate;

    // Update is called once per frame
    void Update()
    {
        health = healthBehaviour.GetCurrentHealth();

        if (health <= 50.0f && objToDeactivate.activeSelf)
        {
            objToDeactivate.SetActive(false);
        }
    }
}
