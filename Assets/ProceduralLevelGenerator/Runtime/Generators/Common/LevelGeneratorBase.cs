using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ProceduralLevelGenerator.Unity.Pipeline;
using ProceduralLevelGenerator.Unity.Pro;
using ProceduralLevelGenerator.Unity.Utils;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = System.Random;

namespace ProceduralLevelGenerator.Unity.Generators.Common
{
    /// <summary>
    /// Base class for level generators.
    /// </summary>
    /// <typeparam name="TPayload"></typeparam>
    public abstract class LevelGeneratorBase<TPayload> : VersionedMonoBehaviour, ILevelGenerator where TPayload : class
    {
        private readonly Random seedsGenerator = new Random();

        protected readonly PipelineRunner<TPayload> PipelineRunner = new PipelineRunner<TPayload>();

        // TODO: Not ideal
        protected abstract bool ThrowExceptionImmediately { get; }

        protected virtual Random GetRandomNumbersGenerator(bool useRandomSeed, int seed)
        {
            if (useRandomSeed)
            {
                seed = seedsGenerator.Next();
            }

            Debug.Log($"Random generator seed: {seed}");

            return new Random(seed);
        }

        public virtual object Generate()
        {
            Debug.Log("--- Generator started ---");
             
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var (pipelineItems, payload) = GetPipelineItemsAndPayload();

            PipelineRunner.Run(pipelineItems, payload);

            Debug.Log($"--- Level generated in {stopwatch.ElapsedMilliseconds / 1000f:F}s ---");

            return payload;
        }

        public virtual IEnumerator GenerateCoroutine()
        {
            Debug.Log("--- Generator started ---");
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var (pipelineItems, payload) = GetPipelineItemsAndPayload();

            var pipelineIterator = PipelineRunner.GetEnumerator(pipelineItems, payload);
            
            if (Application.isPlaying)
            {
                var pipelineCoroutine = this.StartSmartCoroutine<TPayload>(pipelineIterator, ThrowExceptionImmediately);

                yield return pipelineCoroutine.Coroutine;
                yield return pipelineCoroutine.Value;
            }
            else
            {
                while (pipelineIterator.MoveNext())
                {

                }

                yield return payload;
            }

            Debug.Log($"--- Level generated in {stopwatch.ElapsedMilliseconds / 1000f:F}s ---");
        }
        protected abstract (List<IPipelineTask<TPayload>> pipelineItems, TPayload payload) GetPipelineItemsAndPayload();
    }
}