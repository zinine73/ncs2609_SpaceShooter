using System.Collections;
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
    readonly float initHp = 100.0f;
    [SerializeField] float currHp;
    IEnumerator Start()
    {
        currHp = initHp;
        // Component 할당
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();

        // animation 실행
        anim.Play("Idle");

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        turnSpeed = 80.0f;
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
    void OnTriggerEnter(Collider other)
    {
        if (currHp >= 0.0f && other.CompareTag("PUNCH"))
        {
            currHp -= 10.0f;
            //Debug.Log("Player hp = " + currHp + "/" + initHp + "=" + currHp/initHp);
            //Debug.LogFormat("Player hp = {0}/{1}={2}", currHp, initHp, currHp/initHp);
            Debug.Log($"Player hp = {currHp}/{initHp}={currHp/initHp}");
            //Debug.Log($"Player hp = {currHp/initHp}");
            if (currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }
    void PlayerDie()
    {
        Debug.Log("Player Die !");
        // MONSTER 태그를 가진 모든 게임오브젝트들을 찾아옴
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");
        // 모든 몬스터의 OnPlayerDie 함수를 순차적으로 호출
        foreach(GameObject monster in monsters)
        {
            monster.SendMessage("OnPlayerDie", 
                SendMessageOptions.DontRequireReceiver);
        }
    }
}
