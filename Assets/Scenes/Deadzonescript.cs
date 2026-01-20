using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzonescript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerRespawn player = collision.GetComponent<PlayerRespawn>();

        if (player != null)
        {
            player.Respawn();
        }
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
