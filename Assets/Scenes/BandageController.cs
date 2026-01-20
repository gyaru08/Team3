using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandageController : MonoBehaviour
{
    public float swingForce = 30f;

    float lastMouseX;
    public Transform player;
    public float extendSpeed = 30f;
    public float maxLength = 8f;
    public LayerMask hookLayer;

    DistanceJoint2D joint;
    Rigidbody2D playerRb;
    Vector2 hookPoint;
    bool Hookflg=false;
    enum BandageState
    {
        Idle,
        Extending,
        Hit,
        Stopped
    }

    BandageState state = BandageState.Idle;

    Vector2 extendDir;
    float currentLength;

    //private bool isExtending = false;
    //private Vector2 extendDir; // 伸びる方向
    ///*private*/ float currentLength;
    ///*private */Transform targetHook; // ← 狙うフックを固定

    // Start is called before the first frame update
    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
        joint = player.GetComponent<DistanceJoint2D>();
        joint.enabled = false;

        ResetBandage();
    }

    // Update is called once per frame
    void Update()
    {
        // 左クリック：発射
        if (Input.GetMouseButtonDown(0) && state == BandageState.Idle)
        {
            DecideDirectionByMouse();
            state = BandageState.Extending;
        }

       if(Hookflg)
        {
            Vector3 vec = (new Vector3(hookPoint.x, hookPoint.y) - player.position);
            transform.localScale = new Vector3(0.2f, vec.magnitude, 1);
            vec.Normalize();
            float angle =
              Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.position=player.position;

        }
        // 右クリック：キャンセル
        if (Input.GetMouseButtonDown(1))
        {
            ResetBandage();
        }

        if (state == BandageState.Extending)
        {
            Extend();
        }
        if (state == BandageState.Stopped && joint.enabled)
        {
            SwingByMouse();
        }
    }
    void SwingByMouse()
    {
        float mouseX = Input.mousePosition.x;
        float deltaX = mouseX - lastMouseX;

        // マウス移動量を制限（超重要）
        deltaX = Mathf.Clamp(deltaX, -50f, 50f);

        // フレームレート依存を消す
        float forceX = deltaX * swingForce * Time.deltaTime;

        Vector2 force = new Vector2(forceX, 0);
        playerRb.AddForce(force, ForceMode2D.Force);

        lastMouseX = mouseX;
    }

    // クリック位置で方向決定
    void DecideDirectionByMouse()
    {
        Vector3 mouseWorld =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;

        extendDir = (mouseWorld - player.position).normalized;

        float angle =
            Mathf.Atan2(extendDir.y, extendDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Extend()
    {
        currentLength += extendSpeed * Time.deltaTime;
        currentLength = Mathf.Min(currentLength, maxLength);

        transform.localScale = new Vector3(0.2f, currentLength, 1);

        RaycastHit2D hit = Physics2D.Raycast(
            player.position,
            extendDir,
            currentLength,
            hookLayer
        );

        if (hit.collider != null)
        {
            currentLength = hit.distance;
            transform.localScale = new Vector3(0.2f, currentLength, 1);

            hookPoint = hit.point;
            
            joint.connectedBody = hit.transform.GetComponent<Rigidbody2D>();

            StartCoroutine(AttachAfterDelay(0.5f));
            state = BandageState.Hit;
        }

        if (currentLength >= maxLength)
        {
            state = BandageState.Stopped;
        }
    }

    IEnumerator AttachAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        //joint.connectedAnchor = hookPoint;
        //joint.distance = currentLength;
        joint.distance = 5f;
        joint.enabled = true;
        Hookflg=true;

        lastMouseX = Input.mousePosition.x;

        state = BandageState.Stopped;
    }

    IEnumerator StopAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        state = BandageState.Stopped;
    }
    public void ResetBandage()
    {
        Hookflg=false;
        joint.enabled = false;
        StopAllCoroutines();
        state = BandageState.Idle;
        currentLength = 0.01f;
        transform.position = player.position;
        transform.rotation = Quaternion.identity;
        transform.localScale = new Vector3(0.2f, currentLength, 1);
    }
}
