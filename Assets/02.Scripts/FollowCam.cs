using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // 따라갈 대상
    [SerializeField] Transform targetTr;
    // Main camera
    Transform camTr;
    // 대상 거리
    [Range(2.0f, 20.0f)]
    [SerializeField] float distance = 10.0f;
    // y높이
    [Range(0.0f, 10.0f)]
    [SerializeField] float height = 2.0f;
    // 반응 속도
    [SerializeField] float damping = 10.0f;
    // LookAt offset
    [SerializeField] float targetOffset = 2.0f;
    // SmoothDamp
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        camTr = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        // 대상 뒤쪽으로 distance, 높이는 height 이동
        Vector3 pos = targetTr.position
                        + (-targetTr.forward * distance)
                        + (Vector3.up * height);
        // 구면선형보간
        // camTr.position = Vector3.Slerp(camTr.position,              // 시작 위치
        //                                 pos,                        // 목표 위치
        //                                 Time.deltaTime * damping);  // 시간 t
        // SmoothDamp
        camTr.position = Vector3.SmoothDamp(camTr.position,
                                            pos,
                                            ref velocity,
                                            damping);
        // 회전
        camTr.LookAt(targetTr.position + (targetTr.up * targetOffset));
    }
}
