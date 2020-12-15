using UnityEngine;

public class RotativeBehaviour : MonoBehaviour
{
    [SerializeField] private float rotationAxisChangeSpeed;
    [SerializeField] private float rotationSpeed;

    private Vector3 rotationAxis = Vector3.right;

    private void Update()
    {
        var rotationAxisRotationAxis = new Vector3(
           Random.Range(0.1f, 1f),
           Random.Range(0.1f, 1f),
           Random.Range(0.1f, 1f)
        ).normalized;
        rotationAxis = Quaternion.AngleAxis(Time.deltaTime * rotationAxisChangeSpeed, rotationAxisRotationAxis) * rotationAxis;
        transform.rotation *= Quaternion.AngleAxis(Time.deltaTime * rotationSpeed, rotationAxis);
    }
}
