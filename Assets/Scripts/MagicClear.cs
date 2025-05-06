using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class MagicClear : MonoBehaviour
{
    [Tooltip("클리어 처리 후 돌아갈 씬 이름")]
    public string returnSceneName = "SelectStage";

    private static readonly Regex _stageRegex = new Regex(@"Stage(\d{2})");

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            // 현재 씬 이름 추출
            string sceneName = SceneManager.GetActiveScene().name;

            // "Stage" 뒤의 숫자 두 자리를 찾아 파싱
            var m = _stageRegex.Match(sceneName);
            if (!m.Success)
            {
                Debug.LogError($"씬 이름 '{sceneName}'이 'StageXX' 패턴과 맞지 않습니다.");
                return;
            }

            int stageNum = int.Parse(m.Groups[1].Value);
            // 바로 글로벌 인덱스로 쓰면 되므로
            StageProgressManager.Instance.MarkCleared(stageNum);

            SceneController.Instance.FadeToScene(returnSceneName);
        }
    }
}
