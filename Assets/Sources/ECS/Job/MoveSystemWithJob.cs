using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class MoveSystemWithJob : JobComponentSystem
{
    EntityQuery m_Group;

    protected override void OnCreate()
    {
        m_Group = GetEntityQuery(typeof(Translation), ComponentType.ReadOnly<MoveComponent>(), ComponentType.ReadOnly<MoveUpWithJob>());
    }
    [BurstCompile]
    struct moveJob : IJobChunk
    {
        public float DeltaTime;
        public ArchetypeChunkComponentType<Translation> TranslationType;
        [ReadOnly] public ArchetypeChunkComponentType<MoveComponent> MoveComponentType;
        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            var chunkTranslations = chunk.GetNativeArray(TranslationType);
            var chunkMoveSpeeds = chunk.GetNativeArray(MoveComponentType);
            for (var i = 0; i < chunk.Count; i++)
            {
                var translation = chunkTranslations[i];
                var moveSpeed = chunkMoveSpeeds[i];
                var value = new float3(translation.Value.x, translation.Value.y > 10 ? 0: translation.Value.y + DeltaTime * moveSpeed.speed, translation.Value.z);
                chunkTranslations[i] = new Translation
                {
                    Value = value
                };
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var translationType = GetArchetypeChunkComponentType<Translation>();
        var moveComponentType = GetArchetypeChunkComponentType<MoveComponent>(true);

        var job = new moveJob()
        {
            TranslationType = translationType,
            MoveComponentType = moveComponentType,
            DeltaTime = Time.deltaTime
        };
        return job.Schedule(m_Group, inputDeps);
    }
}
