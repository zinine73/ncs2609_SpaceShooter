using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    // 파괴력
    [SerializeField] float damage = 20.0f;
    // 발사 힘
    [SerializeField] float force = 1500.0f;
    Rigidbody rb;

    void Start()
    {
        // component 연결
        rb = GetComponent<Rigidbody>();

        // 총알의 전진 방향으로 force를 가한다
        //rb.AddForce(Vector3.forward * force);
        rb.AddRelativeForce(Vector3.forward * force);
    }
}
