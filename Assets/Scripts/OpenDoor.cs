using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (LockKey.CollectedKeys == LockKey._totalKeys)
        {
            Destroy(gameObject);
        }
        
    }
}
