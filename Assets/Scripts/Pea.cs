using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pea : MonoBehaviour
{
    public float speed = 4f;
    public float lifeDuration = 40f;
    private float lifeTime;

    void OnEnable()
    {
        lifeTime = lifeDuration;
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        // deactivate the pea otherwise
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
