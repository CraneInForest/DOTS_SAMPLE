using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

[Serializable]
public struct MoveComponent : IComponentData
{
    public float speed;
}
