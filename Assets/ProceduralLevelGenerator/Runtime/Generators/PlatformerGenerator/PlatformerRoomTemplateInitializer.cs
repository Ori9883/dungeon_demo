using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.RoomTemplateInitializers;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace ProceduralLevelGenerator.Unity.Generators.PlatformerGenerator
{
    public class PlatformerRoomTemplateInitializer : RoomTemplateInitializerBase
    {
        protected override void InitializeTilemaps(GameObject tilemapsRoot)
        {
            var tilemapLayersHandlers = new PlatformerTilemapLayersHandler();
            tilemapLayersHandlers.InitializeTilemaps(tilemapsRoot);
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Dungeon generator/Platformer room template")]
        public static void CreateRoomTemplatePrefab()
        {
            RoomTemplateInitializerUtils.CreateRoomTemplatePrefab<PlatformerRoomTemplateInitializer>();
        }
#endif
    }
}