using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    // 스파크 파티클 프리팹을 연결할 변수
    [SerializeField] GameObject sparkEffect;

    // 충돌이 시작될 때 발생하는 이벤트
    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 게임오브젝트의 태그값 비교
        //if (collision.collider.tag == "BULLET")
        if (collision.collider.CompareTag("BULLET"))
        {
            // 스파크 파티클을 동적으로 생성
            Instantiate(sparkEffect, collision.transform.position, Quaternion.identity);
            
            // 충돌한 게임오브젝트 삭제
            Destroy(collision.gameObject);
        }
    }
}
