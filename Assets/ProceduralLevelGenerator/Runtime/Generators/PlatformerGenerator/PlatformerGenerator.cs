using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator;
using ProceduralLevelGenerator.Unity.Generators.DungeonGenerator.PipelineTasks;
using ProceduralLevelGenerator.Unity.Generators.PlatformerGenerator.PipelineTasks;
using ProceduralLevelGenerator.Unity.Pipeline;

namespace ProceduralLevelGenerator.Unity.Generators.PlatformerGenerator
{
    public class PlatformerGenerator : DungeonGeneratorBase
    {
        protected override IPipelineTask<DungeonGeneratorPayload> GetPostProcessingTask()
        {
            return new PostProcessTask<DungeonGeneratorPayload>(PostProcessConfig, () => new PlatformerTilemapLayersHandler(), CustomPostProcessTasks);
        }

        protected override IPipelineTask<DungeonGeneratorPayload> GetGeneratorTask()
        {
            return new PlatformerGeneratorTask<DungeonGeneratorPayload>(GeneratorConfig);
        }
    }
}