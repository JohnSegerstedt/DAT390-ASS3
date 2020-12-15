using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHit : MonoBehaviour
{
    public ParticleSystem peaParticles;
    public ParticleSystem dirtParticles;
    public HealthBehaviour healthBehaviour;
    private float health;
    public GameObject hat;

    void Start()
    {
        peaParticles.Stop();
        dirtParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        health = healthBehaviour.GetCurrentHealth();

        if (health < 50.0f)  // Remove hat
        {
            hat.SetActive(false);
        }

        if (health <20)
        {
            dirtParticles.Emit(10);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Pea")
        {
            peaParticles.Emit(10);
        }
    }
}
