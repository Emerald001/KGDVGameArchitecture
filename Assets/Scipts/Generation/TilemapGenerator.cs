using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator
{
    private LevelGenerator.GridType[,] grid;
    private Tilemap tilemap;
    private Tile ground, wall;

    public TilemapGenerator (LevelGenerator.GridType[,] _grid, Tilemap _map, Tile _ground, Tile _wall)
    {
        grid = _grid;
        tilemap = _map;
        ground = _ground;
        wall = _wall;
    }

    public void OnEnter()
    {
        foreach (var cell in grid)
        {
            var tile = cell switch
            {
                LevelGenerator.GridType.Empty => wall,
                LevelGenerator.GridType.Floor => ground,
                LevelGenerator.GridType.Wall => wall,
                _ => null
            };
        }
    }
}
