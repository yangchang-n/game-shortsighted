using UnityEngine;

public class DonutGoal : MonoBehaviour
{
    private StageDonutManager manager;

    void Start()
    {
        // 씬에서 매니저 검색
        manager = FindFirstObjectByType<StageDonutManager>();
        if (manager == null)
            Debug.LogError("StageDonutManager가 씬에 없습니다!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // 도넛 수집 이벤트
        manager.CollectDonut();

        // 도넛 오브젝트 제거
        Destroy(gameObject);
    }
}
