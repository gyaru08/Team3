using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandage : MonoBehaviour
{
    BandageSpawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<BandageSpawner>();
    }

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.RemoveBandage(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
