using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MapGeneration.Benchmarks;
using MapGeneration.Benchmarks.AdditionalData;
using MapGeneration.Benchmarks.Interfaces;
using MapGeneration.Benchmarks.ResultSaving;
using MapGeneration.MetaOptimization.Evolution.DungeonGeneratorEvolution;
using MapGeneration.Utils;
using MapGeneration.Utils.MapDrawing;
using ProceduralLevelGenerator.Unity.Generators.Common.LevelGraph;
using ProceduralLevelGenerator.Unity.Generators.Common.Payloads.Interfaces;
using ProceduralLevelGenerator.Unity.Pro;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ProceduralLevelGenerator.Unity.Generators.Common.Benchmarks
{
    // PRO
    public class BenchmarkRunner : MonoBehaviour
    {
        /// <summary>
        /// Number of benchmark runs.
        /// </summary>
        public int Runs = 20;

        /// <summary>
        /// The camera to take the screenshot.
        /// </summary>
        public Camera ScreenshotCamera;

        /// <summary>
        /// Orthographic size of the camera used for the screenshot. Should fit the whole generated level.
        /// </summary>
        public float ScreenshotCameraSize = 60;

        /// <summary>
        /// Whether the benchmark is running or not.
        /// </summary>
        public bool IsRunning;

        /// <summary>
        /// The number of completed runs of the benchmark.
        /// </summary>
        public int Progress;

        private IEnumerator benchmarkCoroutine;

        public void RunBenchmark()
        {
            var generator = GetComponent<ILevelGenerator>();

            if (generator == null)
            {
                throw new InvalidOperationException("There is no generator runner attached to the GameObject. Make sure that the benchmark runner is attached to a GameObject with a generator runner component.");
            }

            benchmarkCoroutine = RunBenchmarkCoroutine(generator);

            if (Application.isPlaying)
            {
                StartCoroutine(benchmarkCoroutine);
            }
            else
            {
#if UNITY_EDITOR
                EditorApplication.update -= EditorUpdate;
                EditorApplication.update += EditorUpdate;
#endif
            }
        }

        public void StopBenchmark()
        {
            if (benchmarkCoroutine != null)
            {
                StopCoroutine(benchmarkCoroutine);
                benchmarkCoroutine = null;
            }
            
            IsRunning = false;
        }

        public void EditorUpdate()
        {
            benchmarkCoroutine?.MoveNext();
        }

        public IEnumerator RunBenchmarkCoroutine(ILevelGenerator generator)
        {
            IsRunning = true;
            var path = Path.Combine("Benchmarks", FileNamesHelper.PrefixWithTimestamp("benchmark.json"));
            var layoutDrawer = new SVGLayoutDrawer<RoomBase>();
            var runs = new List<GeneratorRun<AdditionalRunData>>();

            Progress = 0;

            for (var i = 0; i < Runs; i++)
            {
                Progress = i + 1;

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var payload = generator.Generate();

                if (payload is IBenchmarkInfoPayload benchmarkInfoPayload)
                {
                    var screenshot = ProUtils.TakeScreenshot(ScreenshotCamera, ScreenshotCameraSize, 1000, 1000);
                    var png = screenshot.EncodeToPNG();
                    var base64 = Convert.ToBase64String(png);

                    var additionalData = new AdditionalUnityData
                    {
                        GeneratedLayoutSvg = layoutDrawer.DrawLayout(benchmarkInfoPayload.GeneratedLevel.GetInternalLayoutRepresentation(), 800,
                            forceSquare: true, fixedFontSize: 20),
                        ImageBase64 = base64
                    };

                    var generatorRun = new GeneratorRun<AdditionalRunData>(benchmarkInfoPayload.GeneratedLevel.GetInternalLayoutRepresentation() != null,
                        stopwatch.ElapsedMilliseconds, benchmarkInfoPayload.GeneratorStats.Iterations, additionalData);

                    runs.Add(generatorRun);
                }
                else
                {
                    throw new InvalidOperationException($"Payload must implement {nameof(IBenchmarkInfoPayload)}");
                }

                yield return null; 
            }

            var scenarioResult = new BenchmarkScenarioResult(name, new List<BenchmarkResult>
            {
                new BenchmarkResult(name, runs.Cast<IGeneratorRun>().ToList())
            });
            var resultSaver = new BenchmarkResultSaver();
            resultSaver.SaveResult(scenarioResult, path);
            IsRunning = false;
        }

        private class AdditionalUnityData : AdditionalRunData
        {
            public string ImageBase64 { get; set; }
        }
    }
}