using ProceduralLevelGenerator.Unity.Generators.Common.RoomTemplates.TilemapLayers;
using UnityEngine;
using UnityEngine.Tilemaps;
// TilemapLayersHandlerBase inherit from ScriptableObject so we need to create an asset
// menu item that we will use to create the scriptable object instance.
// The menu name can be changed to anything you want.
[CreateAssetMenu(menuName = "Edgar/Custom tilemap layers handler", fileName = "CustomTilemapLayersHandler")]
public class CustomTilemapLayers : TilemapLayersHandlerBase
{
    public override void InitializeTilemaps(GameObject gameObject)
    {
        // First make sure that you add the grid component
        var grid = gameObject.AddComponent<Grid>();
        // If we want a different cell size, we can configure that here
        // grid.cellSize = new Vector3(1, 2, 1);
        // And now we create individual tilemap layers
        var floorTilemapObject = CreateTilemapGameObject("Floor", gameObject,"Default", 0);
        var wallsTilemapObject = CreateTilemapGameObject("Walls", gameObject, "wall", 1);
        AddCompositeCollider(wallsTilemapObject);
        var collideable= CreateTilemapGameObject("collideable", gameObject, "wall", 1);
        AddCompositeCollider(collideable);
        CreateTilemapGameObject("other 1", gameObject, "Default", 3);
        CreateTilemapGameObject("other 2", gameObject, "Default", 4);
        CreateTilemapGameObject("other 3", gameObject, "Default", 5);
        CreateTilemapGameObject("Fore", gameObject, "Foreground", 0);
        

    }
    /// <summary>
    /// Helper to create a tilemap layer
    /// </summary>
    protected GameObject CreateTilemapGameObject(string name, GameObject parentObject, string sortingLayerName,int sortingOrder)
    {
        // Create a new game object that will hold our tilemap layer
        var tilemapObject = new GameObject(name);
        // Make sure to correctly set the parent
        tilemapObject.transform.SetParent(parentObject.transform);
        var tilemap = tilemapObject.AddComponent<Tilemap>();
        var tilemapRenderer = tilemapObject.AddComponent<TilemapRenderer>();
        tilemapRenderer.sortingLayerName = sortingLayerName;
        tilemapRenderer.sortingOrder = sortingOrder;
        return tilemapObject;
    }
    /// <summary>
    /// Helper to add a collider to a given tilemap game object.
    /// </summary>
    protected void AddCompositeCollider(GameObject tilemapGameObject, bool isTrigger = false)
    {
        var tilemapCollider2D = tilemapGameObject.AddComponent<TilemapCollider2D>();
        tilemapCollider2D.usedByComposite = true;
        var compositeCollider2d = tilemapGameObject.AddComponent<CompositeCollider2D>();
        compositeCollider2d.geometryType = CompositeCollider2D.GeometryType.Polygons;
        compositeCollider2d.isTrigger = isTrigger;
        tilemapGameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}