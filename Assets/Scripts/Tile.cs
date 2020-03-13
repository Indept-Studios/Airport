﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile
{
    public enum TileType { Gras, Asphalt, Concret, Floor, Empty }

    // Initial Tile set
    TileType type = TileType.Gras;

    Action<Tile> cbTileTypeChanged;

    LooseObject looseObject;
    InstalledObject installedObject;
    World world;

    int x;
    int y;

    public TileType Type
    {
        get { return type; }
        set
        {
            type = value;
            if (cbTileTypeChanged != null)
            {
                cbTileTypeChanged(this);
            }
        }

    }

    public int X { get => x; }
    public int Y { get => y; }

    public Tile(World world, int x, int y)
    {
        this.world = world;
        this.x = x;
        this.y = y;
    }

    public void RegisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged += callback;
    }

    public void UnregisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged -= callback;
    }
}
