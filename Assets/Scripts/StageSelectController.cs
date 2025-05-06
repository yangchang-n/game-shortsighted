using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageSelectController : MonoBehaviour
{
    public Button[] stageButtons; // 크기 10

    private void Start()
    {
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int stageIdx = i + 1; // 1 ~ 10
            string labelText = (stageIdx == 10) ? "0" : stageIdx.ToString();

            var btn     = stageButtons[i];
            var outline = btn.GetComponent<Outline>();
            var txt     = btn.GetComponentInChildren<TMP_Text>();

            bool unlocked = StageProgressManager.Instance.IsUnlocked(stageIdx);

            // 항상 true, 시각 효과는 수동으로 제어
            btn.interactable = true;

            // 시각 효과 : 잠긴 버튼은 어둡게, 열린 버튼만 밝게
            if (unlocked)
            {
                outline.effectColor = new Color32(255, 255, 255, 128);
                txt.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                outline.effectColor = new Color32(50, 50, 50, 128);
                txt.color = new Color32(16, 16, 16, 255);
            }

            // 라벨 세팅
            txt.text = labelText;

            // 클릭 리스너 : unlocked 체크 후 동작
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                if (!unlocked)
                {
                    // 잠긴 상태라면 클릭 무시, 피드백만
                    Debug.Log($"Stage {stageIdx} is locked!");
                    return;
                }
                // 열린 상태일 때만 씬 전환
                string sceneName = (stageIdx < 10)
                    ? $"Stage0{stageIdx}"
                    : "Stage10";
                SceneController.Instance.FadeToScene(sceneName);
            });
        }
    }
}
