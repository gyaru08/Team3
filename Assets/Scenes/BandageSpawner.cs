using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandageSpawner : MonoBehaviour
{
    [SerializeField] GameObject bandagePrefab;
    [SerializeField] Transform spawnPoint;

    const int MAX_BANDAGE = 3;

    // 今出ている包帯を管理
    List<GameObject> bandages = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBandage();
        }
    }
    void SpawnBandage()
    {
        // すでに3本あったら出さない
        if (bandages.Count >= MAX_BANDAGE)
        {
            Debug.Log("包帯は最大3本まで");
            return;
        }

        GameObject bandage =
            Instantiate(bandagePrefab, spawnPoint.position, Quaternion.identity);

        bandages.Add(bandage);
    }

    // 包帯が消えたときに呼ぶ
    public void RemoveBandage(GameObject bandage)
    {
        if (bandages.Contains(bandage))
        {
            bandages.Remove(bandage);
        }
    }
}
