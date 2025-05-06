using UnityEngine;

public class StageProgressManager : MonoBehaviour
{
    public static StageProgressManager Instance { get; private set; }

    /// <summary>
    /// 크기 21,
    /// [0] = Start 누름,
    /// [01 ~ 10] = Chapter1 스테이지 1 ~ 10,
    /// [11 ~ 20] = Chapter2 (나중에)
    /// </summary>
    private bool[] _cleared = new bool[21];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            ResetAll();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 타이틀에서 Start 버튼 누를 때 호출,
    /// 인덱스 0을 true로, 챕터 1 첫 스테이지(인덱스 1)만 활성화
    /// </summary>
    public void MarkStarted()
    {
        _cleared[0] = true;
        // 나머지 모두 초기화
        for (int i = 1; i < _cleared.Length; i++)
        {
            _cleared[i] = false;
        }
        _cleared[1] = true;
    }

    /// <summary>
    /// 필요시 전체 상태 초기화
    /// </summary>
    public void ResetAll()
    {
        for (int i = 0; i < _cleared.Length; i++)
        {
            _cleared[i] = false;
        }
    }

    /// <summary>
    /// 특정 스테이지(1 ~ 10) 클리어 마킹
    /// </summary>
    public void MarkCleared(int stageIndex)
    {
        if (stageIndex < 1 || stageIndex > 10) return;

        // 현재 스테이지 클리어 플래그
        _cleared[stageIndex] = true;

        // 다음 스테이지가 있으면 언락
        int next = stageIndex + 1;
        if (next <= 10) _cleared[next] = true;
    }

    /// <summary>
    /// 버튼 활성(해제) 여부 조회 (1 ~ 10만 처리)
    /// </summary>
    public bool IsUnlocked(int stageIndex)
    {
        if (stageIndex < 1 || stageIndex > 10) return false;
        return _cleared[stageIndex];
    }
}
