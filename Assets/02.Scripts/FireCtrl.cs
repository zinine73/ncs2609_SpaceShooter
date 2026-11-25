using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    // 총알 프리펩
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletParent;
    // 총알 발사 좌표
    [SerializeField] Transform firePos;
    [SerializeField] AudioClip fireSfx;
    new AudioSource audio;
    // MuzzleFlash용 meshRenderer
    MeshRenderer muzzleFlash;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();
        muzzleFlash.enabled = false;
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
        // Bullet 프리펩을 동적으로 생성(객체, 위치, 회전, 부모)
        var bul = Instantiate(bullet, 
            firePos.position, firePos.rotation, bulletParent);
        audio.PlayOneShot(fireSfx, 1.0f);
        // 가끔 스테이지를 벗어나는 총알 처리
        Destroy(bul, 5f);
        // 총구 화염효과 코루틴 함수 호출
        //StartCoroutine("ShowMuzzleFlash");
        StartCoroutine(ShowMuzzleFlash());
    }
    IEnumerator ShowMuzzleFlash()
    {
        Vector2 offset = new Vector2(Random.Range(0, 2),
                        Random.Range(0, 2)) * 0.5f;
        muzzleFlash.material.mainTextureOffset = offset;
        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, angle);
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(0.2f);
        muzzleFlash.enabled = false;
    }
}
