using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    BallLauncher launcher;
    // Start is called before the first frame update
    void Awake()
    {
        launcher = FindAnyObjectByType<BallLauncher>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        launcher.switchCollectedItem();
        Destroy(gameObject);
    }
}