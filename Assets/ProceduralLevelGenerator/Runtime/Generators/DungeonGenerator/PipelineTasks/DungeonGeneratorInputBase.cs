using System.Collections;
using ProceduralLevelGenerator.Unity.Generators.Common;
using ProceduralLevelGenerator.Unity.Pipeline;
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks
{
    public abstract class DungeonGeneratorInputBase : ScriptableObject, IPipelineTask<DungeonGeneratorPayload>
    {
        public DungeonGeneratorPayload Payload { get; set; }

        public IEnumerator Process()
        {
            Payload.LevelDescription = GetLevelDescription();
            yield return null;
        }

        protected abstract LevelDescription GetLevelDescription();
    }
}