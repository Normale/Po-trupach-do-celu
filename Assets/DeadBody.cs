using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -15) Destroy(this);
    }
}
