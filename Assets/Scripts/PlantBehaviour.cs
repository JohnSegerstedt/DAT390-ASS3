using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlantBehaviour : MonoBehaviour
{
    public Animator anim;
    public GameObject playerContainer;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        // Shooting
        if (Input.GetButtonDown("Jump")) 
        {
            anim.SetBool("isShooting", true);

            GameObject peaObject = ObjectPoolingManager.Instance.GetPea();
            peaObject.transform.position = playerContainer.transform.position;
            peaObject.transform.forward = playerContainer.transform.forward;
        }
        else
        {
            anim.SetBool("isShooting", false);
        }
    }
}
