using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // 싱글톤 인스턴스
    private static SceneController _instance;

    // 싱글톤 인스턴스 접근 프로퍼티
    public static SceneController Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("SceneController 인스턴스가 존재하지 않습니다.");
            }
            return _instance;
        }
    }

    [Header("Fade Settings")]
    [Tooltip("페이드용 UI Panel의 Image 컴포넌트")]
    public Image fadeImage;
    [Tooltip("페이드 지속 시간(초)")]
    public float fadeDuration = 1f;

    [Header("Scene Names")]
    public string selectStageSceneName = "SelectStage";
    // public string stage01SceneName = "Stage01";

    private void Awake()
    {
        // 싱글톤 설정
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            // 최초 진입 시 페이드 인
            StartCoroutine(FadeIn());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnStartButton()
    {
        StageProgressManager.Instance.MarkStarted();
        FadeToScene(selectStageSceneName);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    private IEnumerator FadeIn()
    {
        // 초기 씬 로드 시, 타이틀 또는 선택 화면에서만 커서 보이기
        string initScene = SceneManager.GetActiveScene().name;
        bool showCursor = initScene == "TitleScreen" || initScene == "SelectStage";
        Cursor.visible = showCursor;
        Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Locked;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            SetAlpha(1f - timer / fadeDuration);
            yield return null;
        }
        SetAlpha(0f);
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        // 페이드 아웃 (투명 -> 불투명)
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            SetAlpha(timer / fadeDuration);
            yield return null;
        }
        SetAlpha(1f);

        // 씬 로드
        yield return SceneManager.LoadSceneAsync(sceneName);

        // 씬 로드 후, 타이틀 또는 선택 화면에서만 커서 보이기
        bool showCursor = sceneName == "TitleScreen" || sceneName == "SelectStage";
        Cursor.visible = showCursor;
        Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Locked;

        // 페이드 인 (불투명 -> 투명)
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            SetAlpha(1f - timer / fadeDuration);
            yield return null;
        }
        SetAlpha(0f);
    }
    
    // 페이드 패널의 투명도 조절
    private void SetAlpha(float alpha)
    {
        Color c = fadeImage.color;
        c.a = Mathf.Clamp01(alpha);
        fadeImage.color = c;
    }
}