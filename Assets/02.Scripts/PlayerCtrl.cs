using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerCtrl : MonoBehaviour
{
    // 상수
    const float COMPARE_FLOAT = 0.1f;
    const float FADE_LENGTH = 0.25f;

    // 변수
    Transform tr = null;
    // Animation 변수
    Animation anim;
    // 이동 속도 변수
    [SerializeField] float moveSpeed = 10.0f;
    // 회전 속도 변수
    [SerializeField] float turnSpeed = 80.0f;

    void Start()
    {
        // Component 할당
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        // animation 실행
        anim.Play("Idle");
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");

        //Vector  
        // 이동 방향 계산
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        // Translate(이동 방향 * 속력 * Time.deltaTime)
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);

        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);

        // animation 
        PlayerAnim(h, v);
    }

    void PlayerAnim(float h, float v)
    {
        if (v >= COMPARE_FLOAT)
        {
            anim.CrossFade("RunF", FADE_LENGTH);
        }
        else if (v <= -COMPARE_FLOAT)
        {
            anim.CrossFade("RunB", FADE_LENGTH);
        }
        else if (h >= COMPARE_FLOAT)
        {
            anim.CrossFade("RunR", FADE_LENGTH);
        }
        else if (h <= -COMPARE_FLOAT)
        {
            anim.CrossFade("RunL", FADE_LENGTH);
        }
        else
        {
            anim.CrossFade("Idle", FADE_LENGTH);
        }
    }
}
