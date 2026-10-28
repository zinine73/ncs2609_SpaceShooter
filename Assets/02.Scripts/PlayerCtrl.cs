using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // 변수
    Transform tr = null;

    void Start()
    {
        // Component 할당
        tr = GetComponent<Transform>();
        //tr = this.gameObject.GetComponent<Transform>();
        //tr = GetComponent("Transform") as Transform;
        //tr = (Transform)GetComponent(typeof(Transform));    
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Debug.Log("h=" + h);
        Debug.Log("v=" + v);

        // Transform position
        //transform.position += new Vector3(0, 0, 1);

        // normalized vector
        tr.position += Vector3.forward * 1;
    }
}
