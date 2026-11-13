using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class BarrelCtrl : MonoBehaviour
{
    // 폭발 effect
    [SerializeField] GameObject expEffect;
    [SerializeField] float expForce = 1500.0f;
    [SerializeField] int goalCount = 3;
    [SerializeField] float expDestroySec = 5.0f;
    [SerializeField] float barrelDestroySec = 3.0f;
    [SerializeField] float radius = 10.0f;
    [SerializeField] Texture[] textures;
    new MeshRenderer renderer;
    Transform tr;
    Rigidbody rb;
    int hitCount = 0;
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        renderer = GetComponentInChildren<MeshRenderer>();
        // 난수 발생
        int idx = Random.Range(0, textures.Length);
        //int ii = Random.Range(1, 5);
        //float ff = Random.Range(1.0f, 5.0f);
        renderer.material.mainTexture = textures[idx];
    }
    // 충돌 시 발생하는 콜백함수
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            // 총알 맞은 횟수를 증가시키고 3회 이상이면 폭발
            if (++hitCount >= goalCount)
            {
                ExpBarrel();
            }
        }
    }
    void ExpBarrel()
    {
        // 파티클 생성
        var exp = Instantiate(expEffect, 
            tr.position, Quaternion.identity);
        // 5초 후 파티클 효과 제거
        Destroy(exp, expDestroySec);
        // mass를 1.0으로 무게를 가볍게 함
        //rb.mass = 1.0f;
        // 위로 솟구치는 힘을 가함
        //rb.AddForce(Vector3.up * expForce);
        // 간접 폭발력 전달
        IndirectDamage(tr.position);
        // 3초 후 드럼통 제거
        Destroy(gameObject, barrelDestroySec);
    }

    void IndirectDamage(Vector3 pos)
    {
        Collider[] colls = Physics.OverlapSphere(
                            pos, 
                            radius,
                            1 << 3);
        foreach (var coll in colls)
        {
            rb = coll.GetComponent<Rigidbody>();
            rb.mass = 1.0f;
            rb.constraints = RigidbodyConstraints.None;
            rb.AddExplosionForce(expForce, pos, radius, 1200.0f);
        }
    }
}
