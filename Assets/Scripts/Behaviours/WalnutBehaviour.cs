using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalnutBehaviour : MonoBehaviour
{
    public HealthBehaviour healthBehaviour;
    private float health;
    [Header("Toggled Body Parts")]
    public GameObject leftEye;
    public GameObject rightEye;
    public GameObject fullBody;
    public GameObject injuredHead;
    public GameObject halfBody;

    void Update()
    {
        health =  healthBehaviour.GetCurrentHealth();

        if (health <= 70.0f)  // Change mesh to injured head
        {
            injuredHead.SetActive(true);

            fullBody.SetActive(false);
        }

        if (health <= 50.0f)  // Change mesh to half body
        {
            halfBody.SetActive(true);
            
            injuredHead.SetActive(false);
            leftEye.SetActive(false);
            rightEye.SetActive(false);
        }
    }
}
