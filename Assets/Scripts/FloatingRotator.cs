using UnityEngine;

public class FloatingRotator : MonoBehaviour
{
    [Header("Floating Settings")]
    public float floatAmplitude = 0.1f;
    public float floatFrequency = 3f;

    [Header("Rotation Settings")]
    public Vector3 rotationSpeed = new Vector3(45f, 45f, 0f); // 회전 속도 (deg/sec)

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // 위아래 주기 운동
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        // 회전
        transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);
    }
}
