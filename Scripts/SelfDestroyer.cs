using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelfDestroyer : MonoBehaviour
{

    public float destructionTime;

    void Start()
    {
        Destroy(this.gameObject, destructionTime);
    }
}
