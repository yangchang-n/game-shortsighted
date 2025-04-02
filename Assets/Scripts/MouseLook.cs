using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 2f;  // 마우스 감도
    private float xRotation = 0f;        // 현재 카메라의 상하 회전 각도

    void Start()
    {
        // 커서를 중앙에 고정하고 보이지 않게 처리 (PlayerController에서 처리)
        // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 마우스 Y축 입력으로 상하 회전 (마우스의 움직임에 따라 반전 처리)
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        xRotation -= mouseY;
        // 상하 회전 각도를 -90° ~ 90°로 제한하여 과도한 회전 방지
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 카메라의 로컬 회전을 적용 (수평 회전은 제외)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
