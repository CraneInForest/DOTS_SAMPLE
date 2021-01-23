using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MoveUpMono : MonoBehaviour
{
    public float speed;
    // Update is called once per frame
    void Update()
    {
        transform.position = new float3(transform.position.x, transform.position.y + Time.deltaTime * speed, transform.position.z);
        if (transform.position.y > 10.0f)
            transform.position = new float3(transform.position.x, -10, transform.position.z);
    }
}
