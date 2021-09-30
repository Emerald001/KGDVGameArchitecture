using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator
{
    private Vector2Int size;
    private LevelGenerator.GridType[,] grid;
    private Tilemap tilemap;
    private Tile ground, wall;

    public TilemapGenerator (Vector2Int _size, LevelGenerator.GridType[,] _grid, Tilemap _map, Tile _ground, Tile _wall)
    {
        grid = _grid;
        tilemap = _map;
        ground = _ground;
        wall = _wall;
        size = _size;
    }


}
