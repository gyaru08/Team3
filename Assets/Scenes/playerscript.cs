using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerscript : MonoBehaviour
{
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private bool isGround = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 3f;
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
}