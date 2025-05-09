using UnityEngine;

public class StageDonutManager : MonoBehaviour
{
    [Tooltip("스테이지에 배치된 도넛의 총 개수")]
    public int totalDonuts = 1;

    private int remainingDonuts;

    void Start()
    {
        remainingDonuts = totalDonuts;
    }

    /// <summary>
    /// 1개의 도넛이 먹혔을 때 호출
    /// </summary>
    public void CollectDonut()
    {
        remainingDonuts = Mathf.Max(0, remainingDonuts - 1);
        Debug.Log($"Donuts left: {remainingDonuts}");

        if (remainingDonuts == 0)
            OnAllDonutsCollected();
    }

    private void OnAllDonutsCollected()
    {
        // 현재 씬 인덱스 얻기
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        int stageIndex = int.Parse(currentScene.Substring(currentScene.Length - 2)); // "Stage01" → 1

        // 클리어 플래그
        StageProgressManager.Instance.MarkCleared(stageIndex);

        // 다음 씬 이름 계산
        string selectStageScene = "SelectStage";
        /*
        int nextStage = stageIndex + 1;
        string nextScene = nextStage <= 9
            ? $"Stage0{nextStage}"
            : nextStage == 10
                ? "Stage10"
                : "SelectStage";
        */

        // 씬 전환
        SceneController.Instance.FadeToScene(selectStageScene);
        // SceneController.Instance.FadeToScene(nextScene);
    }
}
