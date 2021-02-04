using System.Collections;

namespace ProceduralLevelGenerator.Unity.Generators.Common
{
    /// <summary>
    /// Interface that represents a level generator.
    /// </summary>
    public interface ILevelGenerator
    {
        IEnumerator GenerateCoroutine();

        /// <summary>
        /// Generates and returns a level.
        /// </summary>
        object Generate();
    }
}