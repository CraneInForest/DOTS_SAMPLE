using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SpawnMain : MonoBehaviour
{
    public GameObject Prefab;
    public int CountX = 100;
    public int CountY = 100;
    public int moveSpeed = 1;
    void Start()
    {
        Entity prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(Prefab, World.Active);
        var entityManager = World.Active.EntityManager;

        for (int x = 0; x < CountX; x++)
        {
            for (int y = 0; y < CountY; y++)
            {
                var instance = entityManager.Instantiate(prefab);
                // Place the instantiated entity in a grid with some noise
                var position = transform.TransformPoint(new float3(x - CountX / 2, noise.cnoise(new float2(x, y) * 0.21F) * 10, y - CountY / 2));
                entityManager.SetComponentData(instance, new Translation() { Value = position });
                entityManager.AddComponentData(instance, new MoveUp());
                entityManager.AddComponentData(instance, new MoveComponent() { speed = moveSpeed });
            }
        }
    }
}
