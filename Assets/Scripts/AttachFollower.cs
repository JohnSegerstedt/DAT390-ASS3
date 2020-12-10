using UnityEngine;
using Vuforia;

[RequireComponent(typeof(ImageTargetBehaviour))]
public class AttachFollower : MonoBehaviour
{
    public Transform follower;
    private ImageTargetBehaviour imageTarget;
    private Transform imageTargetTransform;
    // Start is called before the first frame update
    void Start()
    {
        imageTarget = GetComponent<ImageTargetBehaviour>();
        imageTargetTransform = imageTarget.transform;
    }

    void Update()
    {
        if (!follower.gameObject.activeSelf)
        {
            if (imageTarget.CurrentStatus == TrackableBehaviour.Status.TRACKED)
            {
                follower.gameObject.SetActive(true);
            }
        }
    }

    private void LateUpdate()
    {
        if (imageTarget.CurrentStatus == TrackableBehaviour.Status.TRACKED)
        {
            follower.localPosition = imageTargetTransform.localPosition;
            follower.localRotation = imageTargetTransform.localRotation;
            follower.localScale = imageTargetTransform.localScale;
        }
    }
}
