using UnityEngine;

public class HeadlightController : MonoBehaviour
{
    // Light 컴포넌트를 캐시
    private Light headlight;

    // 광원 토글을 위한 키 (일단은 L키)
    public KeyCode toggleKey = KeyCode.L;

    void Start()
    {
        headlight = GetComponent<Light>();
        if (headlight == null) Debug.LogError("HeadlightController : Light 컴포넌트를 찾을 수 없습니다!");
    }

    void Update()
    {
        // 토글키 입력 시 광원 On/Off 전환
        if (Input.GetKeyDown(toggleKey)) headlight.enabled = !headlight.enabled;
    }
}
