using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class MoveSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.deltaTime;
        Entities.WithAllReadOnly<MoveComponent, MoveUp>().ForEach(
                (Entity id, ref Translation translation, ref MoveComponent _MoveComponent) =>
                {
                    translation = new Translation()
                    {
                        Value = new float3(translation.Value.x, translation.Value.y + deltaTime * _MoveComponent.speed, translation.Value.z)
                    };

                    if (translation.Value.y > 10.0f)
                        PostUpdateCommands.RemoveComponent<MoveUp>(id);
                }
            );
        Entities.WithAllReadOnly<MoveComponent>().WithNone<MoveUp,MoveUpWithJob>().ForEach(
               (Entity id, ref Translation translation) =>
               {
                   translation = new Translation()
                   {
                       Value = new float3(translation.Value.x, -10, translation.Value.z)
                   };

                   PostUpdateCommands.AddComponent<MoveUp>(id, new MoveUp());
               }
           );
    }
}
