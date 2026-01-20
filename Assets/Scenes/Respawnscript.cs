using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnscript : MonoBehaviour
{
    public Transform respawnPoint; // ← 黄色い位置を指定
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }
    void Respawn()
    {
        // プレイヤーの位置を固定リスポーン位置に移動
        transform.position = respawnPoint.position;
    }
}
