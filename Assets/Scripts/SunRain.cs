using UnityEngine;

[RequireComponent(typeof(BoardController))]
class SunRain : MonoBehaviour
{
    /// <summary>
    /// The greater the value of LowPassKernelWidthInSeconds, the slower the
    /// filtered value will converge towards current input sample (and vice versa).
    /// </summary>
    public float lowPassKernelWidthInSeconds = 1.0f;

    private float accelerometerUpdateInterval = 1.0f / 60.0f;
    // This next parameter is initialized to 2.0 per Apple's recommendation,
    // or at least according to Brady! ;)
    private float shakeDetectionThreshold = 2.0f;
    private float lowPassFilterFactor;
    private Vector3 lowPassValue;
    private BoardController boardController;

    private float cooldown;

    private void Awake()
    {
        boardController = GetComponent<BoardController>();
    }

    void Start()
    {
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
        cooldown = 0;
    }

    void Update()
    {
        Vector3 acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        Vector3 deltaAcceleration = acceleration - lowPassValue;
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold || Input.GetMouseButtonDown(1))
        {
            var pos = boardController.GetCell(boardController.RandomCell);
            var obj = ObjectPoolManager.Instance.GetGameObject(PoolObject.Sunshine);
            obj.transform.position = pos + transform.up * 0.3f;
            cooldown = Random.value > 0.4f ? Random.Range(0.1f, 1f) : Random.Range(5f, 60f);
        }
    }
}

