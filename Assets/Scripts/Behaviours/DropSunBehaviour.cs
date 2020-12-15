using UnityEngine;

public class DropSunBehaviour : MonoBehaviour
{
    public float throwForce = 1.5f;

    void OnEnable()
    {
        GetComponent<Rigidbody>().AddForce(
            new Vector3(Random.value - .5f, .5f, Random.value - .5f).normalized * throwForce,
            ForceMode.Impulse
        );
    }

}

