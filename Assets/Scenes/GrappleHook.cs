using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool stuck = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // 初期は kinematic false にして飛ぶ（Rigidbody2D は Dynamic）
        //rb.isKinematic = false;
    }



    // 外部から呼ぶ（PlayerGrapple.Shoot から）
    public void Launch(Vector2 velocity)
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
        rb.velocity = velocity;
    }

    // Player からの停止コール（安全策）
    public void Stop()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (stuck) return;

        // 当たったら止めてフック成功（必要ならタグ判定を追加）
        stuck = true;
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }

        // （将来：ここでプレイヤーにAttachを通知するなど追加可能）
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
