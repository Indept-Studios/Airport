using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldManager : MonoBehaviour
{    
    public static WorldManager Instance { get; protected set; }
    public World World { get; protected set; }

    public Sprite grasSprite;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance!=null)
        {
            Debug.LogError("There should never be two world controllers.");
        }
        Instance = this;
        InstantiateNewWorld();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InstantiateNewWorld()
    {
        World = new World(100, 100);

        for (int x = 0; x < World.Width; x++)
        {
            for (int y = 0; y < World.Height; y++)
            {
                Tile tile_data = World.GetTileAt(x, y);
                GameObject tile_go = new GameObject();

                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);
                tile_go.transform.SetParent(this.transform, true);
                tile_go.AddComponent<SpriteRenderer>();
                OnTileTypeChanged(tile_data, tile_go);
                tile_data.RegisterTileTypeChangedCallback((tile) => { OnTileTypeChanged(tile, tile_go); });
            }
        }
    }

    public void OnTileTypeChanged(Tile tile_data, GameObject tile_go)
    {
        if (tile_data.Type == Tile.TileType.Gras)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = grasSprite;
        }
        else if (tile_data.Type == Tile.TileType.Empty)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            Debug.LogError("OnTileTypeChanged - Unrecognized tile Type.");
        }
    }

    public Tile GetTileAtWorldCoord(Vector3 coord)
    {
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);

        return WorldManager.Instance.World.GetTileAt(x, y);
    }
}
