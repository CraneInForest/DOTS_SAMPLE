using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnMainMono : MonoBehaviour
{
    public GameObject Prefab;
    public int CountX = 100;
    public int CountY = 100;
    public int moveSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < CountX; x++)
        {
            for (int y = 0; y < CountY; y++)
            {
                var instance = GameObject.Instantiate(Prefab);
                instance.transform.position = instance.transform.TransformPoint(new float3(x - CountX / 2, noise.cnoise(new float2(x, y) * 0.21F) * 10, y - CountY / 2));
                MoveUpMono _ = instance.AddComponent<MoveUpMono>();
                _.speed = moveSpeed;
            }
        }
    }
}
