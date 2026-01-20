using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook1 : MonoBehaviour
{
    Rigidbody2D rb;
    bool isStuck = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isStuck) return;

        // •Ç‚â’n–Ê‚É“–‚½‚Á‚½‚çŽ~‚Ü‚é
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            isStuck = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
