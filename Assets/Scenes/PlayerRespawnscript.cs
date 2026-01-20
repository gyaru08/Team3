using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 startpawn;
    // Start is called before the first frame update
    void Start()
    {
        startpawn = transform.position;
    }

    public void Respawn()
    {
        transform.position = startpawn;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
