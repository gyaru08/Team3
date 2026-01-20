using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrapple : MonoBehaviour
{
    public Transform firePoint;      // 包帯が出る位置（黄色プレイヤーの手）
    public GrappleHook hookPrefab;   // 白いフックのプレハブ
    public float shootSpeed = 20f;
    private LineRenderer line;
    private GrappleHook currentHook;
   

    // Start is called before the first frame update
    void Start()
    {
        if (line != null)
        {
            line.positionCount = 0;
            line.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Release();
        }

        if (currentHook != null && line != null && line.enabled)
        {
            UpdateLine();
        }
    }
    void Shoot()
    {
        // 既に発射済みならキャンセル
        if (currentHook != null) return;
        if (hookPrefab == null || firePoint == null) return;

        // 生成 (GrappleHook 型で受け取る)
        Vector3 spawnPos = firePoint.position;
        Quaternion rot = Quaternion.identity;
        GrappleHook spawned = Instantiate(hookPrefab, spawnPos, rot);

        // 方向計算（カメラが null なら中断）
        if (Camera.main == null) return;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        Vector2 dir = (mouseWorld - spawnPos).normalized;

        // 発射
        spawned.Launch(dir * shootSpeed);

        currentHook = spawned;

        // ライン表示開始
        if (line != null)
        {
            line.positionCount = 2;
            line.enabled = true;
        }
    }
    void Release()
    {
        if (currentHook != null)
        {
            Destroy(currentHook.gameObject);
            currentHook = null;
        }

        if (line != null)
        {
            line.positionCount = 0;
            line.enabled = false;
        }
    }

    void UpdateLine()
    {
        if (line == null || currentHook == null) return;
        line.SetPosition(0, firePoint.position);
        line.SetPosition(1, currentHook.transform.position);
    }
}
