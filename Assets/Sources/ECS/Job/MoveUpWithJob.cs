using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct MoveUpWithJob : IComponentData
{
    // MoveUpWithJob is a "tag" component and contains no data. Tag components can be used to mark entities that a system should process.
}
