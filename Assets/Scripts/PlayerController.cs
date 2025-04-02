using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;              // 이동 속도
    public float mouseSensitivity = 2f;   // 마우스 감도

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // 커서를 중앙에 고정하고 보이지 않게 처리
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 마우스 X축 입력으로 플레이어(수평) 회전
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0f, mouseX, 0f);

        // WASD 키를 통한 이동 처리
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);
    }
}
