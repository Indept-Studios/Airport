﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldManager : MonoBehaviour
{
    public Sprite grasSprite;

    World world;

    // Start is called before the first frame update
    void Start()
    {
        world = new World(100, 100);

        for (int x = 0; x < world.Width; x++)
        {
            for (int y = 0; y < world.Height; y++)
            {
                Tile tile_data = world.GetTileAt(x, y);

                GameObject tile_go = new GameObject();
                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);

                tile_go.AddComponent<SpriteRenderer>();

                tile_data.RegisterTileTypeChangedCallback((tile)=> { OnTileTypeChanged(tile, tile_go); });

            }
        }
        world.RandomizeTiles();
    }
    float randomizeTileTimer = 2f;

    // Update is called once per frame
    void Update()
    {
        randomizeTileTimer -= Time.deltaTime;
        if (randomizeTileTimer<0)
        {
            world.RandomizeTiles();
            randomizeTileTimer = 2f;
        }
    }
    void OnTileTypeChanged(Tile tile_data, GameObject tile_go)
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
}
