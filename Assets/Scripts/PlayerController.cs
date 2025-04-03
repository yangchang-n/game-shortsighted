using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;               // 이동 속도
    public float mouseSensitivity = 2f;    // 마우스 감도
    public float jumpForce = 2f;           // 점프력
    public float gAcceleration = -20f;     // 중력가속도

    private CharacterController controller;
    private Vector3 velocity;              // 수직(중력, 점프)용 속도

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // 커서를 중앙에 고정하고 보이지 않게 처리
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 1. 마우스 입력으로 좌우 회전 처리
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0f, mouseX, 0f);

        // 2. WASD 입력을 통한 수평 이동 처리 (플레이어가 바라보는 방향 기준)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // 3. 지면에 닿아있을 때
        if (controller.isGrounded)
        {
            // 약간의 하강력을 주어 바닥과의 접촉을 유지함 (정확한 충돌 판정을 위해)
            if (velocity.y < 0) velocity.y = -2f;

            // 스페이스바 입력 시 점프 처리
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 점프 초기 속도 계산 : 원하는 점프 높이에 맞는 값 설정
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gAcceleration);
            }
        }

        // 4. 중력 적용 (매 프레임마다 누적)
        velocity.y += gAcceleration * Time.deltaTime;

        // 5. 수평 이동과 수직 속도를 하나의 벡터로 결합하여 이동 적용
        Vector3 finalMove = move * speed + velocity;
        controller.Move(finalMove * Time.deltaTime);
    }
}
