using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct MoveUp : IComponentData
{
    // MoveUp is a "tag" component and contains no data. Tag components can be used to mark entities that a system should process.
}
