using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowSide : MonoBehaviour
{
     public Transform target;
    public float offsetX = 2f;
    public float smooth = 0.1f;
    public float fixedZ = -10f;

    public float minY = -10f;  // 下の限界
    public float maxY = 30f;   // 上の限界（必ず 14 より上に）


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null) return;

        float targetY = Mathf.Clamp(target.position.y, minY, maxY);
 
        Vector3 pos = transform.position;
        pos.y = target.position.y + 5.0f; // カメラの高さ調整
        transform.position = pos;
        Vector3 desiredPosition = new Vector3(
            target.position.x + offsetX,
            targetY,
            fixedZ
            );
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smooth);

    }
}
