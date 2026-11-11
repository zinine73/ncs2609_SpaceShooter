using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    // 총알 프리펩
    [SerializeField] GameObject bullet;
    // 총알 발사 좌표
    [SerializeField] Transform firePos;
    void Start()
    {
        
    }

    void Update()
    {
        // 마우스 왼쪽 버튼을 클릭했을 때 fire 함수 호출
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }        
    }

    void Fire()
    {
        // Bullet 프리펩을 동적으로 생성(객체, 위치, 회전)
        var bul = Instantiate(bullet, firePos.position, firePos.rotation);
        // 가끔 스테이지를 벗어나는 총알 처리
        Destroy(bul, 5f);
    }
}
