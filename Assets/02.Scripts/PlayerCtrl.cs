using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerCtrl : MonoBehaviour
{
    // 변수
    [SerializeField] Transform tr = null;
    // 이동 속도 변수
    [SerializeField] float moveSpeed = 10.0f;
    // 회전 속도 변수
    [SerializeField] float turnSpeed = 80.0f;

    void Start()
    {
        // Component 할당
        tr = GetComponent<Transform>();
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
    }
}
