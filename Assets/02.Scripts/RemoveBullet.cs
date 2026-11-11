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
            // 첫번째 충돌 지점의 정보 추출
            ContactPoint cp = collision.GetContact(0);
            // 충돌한 총알의 법선벡터를 쿼터니언 변환
            Quaternion rot = Quaternion.LookRotation(-cp.normal);
            // 스파크 파티클을 동적으로 생성
            GameObject spark = Instantiate(sparkEffect, cp.point, rot);
                //collision.transform.position, 
                //Quaternion.identity);
            // 일정 시간이 지난 후 파티클 삭제
            Destroy(spark, 0.5f);
            // 충돌한 게임오브젝트 삭제
            Destroy(collision.gameObject);
        }
    }
}
