using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//MonoBehaviour for testing only! Start and OnDrawGizmos are the only functions that use it
public class LevelGenerator : MonoBehaviour
{
    private struct Walker
    {
        public Vector2 pos;
        public Vector2 dir;
    }

    public struct CellData
    {
        public Vector2 pos;
        public GridType type;
    }

    public enum GridType
    {
        Empty,
        Floor,
        Wall,
    }

    private Vector2Int size = Vector2Int.one * 50;

    private GridType[,] grid;
    private List<Walker> walkers = new List<Walker>();

    private float walkerDirectionChangeChance = 0.45f;
    private float walkerSpawnChance = 0.05f;
    private float walkerDestroyChance = 0.05f;

    private int maxWalkers = 10;

    private float fillPercent = 0.2f;

    private void Start()
    {
        Setup();
        MoveWalkers();
        CreateWalls();
        RemoveIsolatedWalls();
    }

    private void Setup()
    {
        grid = new GridType[size.x, size.y];

        for (var x = 0; x < size.x; x++)
        {
            for (var y = 0; y < size.y; y++)
            {
                grid[x, y] = GridType.Empty;
            }
        }

        var newWalker = new Walker
        {
            pos = new Vector2(Mathf.RoundToInt(size.x * 0.5f), Mathf.RoundToInt(size.y * 0.5f)),
            dir = GetRandomDirection()
        };

        walkers.Add(newWalker);
    }

    private void MoveWalkers()
    {
        var floorCount = 0;

        while ((float) floorCount / grid.Length < fillPercent)
        {
            for (var i = 0; i < walkers.Count; i++)
            {
                var walker = walkers[i];
                
                //Spawn floors below walkers
                grid[(int)walker.pos.x, (int)walker.pos.y] = GridType.Floor;
                floorCount++;
                
                //Kill walkers
                if (Random.value < walkerDestroyChance && walkers.Count > 1)
                {
                    walkers.RemoveAt(i);
                    break;
                }
                
                //Change walker directions
                if (Random.value < walkerDirectionChangeChance)
                {
                    walker.dir = GetRandomDirection();
                    walkers[i] = walker;
                }
                
                //Birth new walkers
                if (Random.value < walkerSpawnChance && walkers.Count < maxWalkers)
                {
                    var newWalker = new Walker
                    {
                        dir = GetRandomDirection(),
                        pos = walker.pos
                    };
                    walkers.Add(newWalker);
                }
               
                //Move walkers
                walker.pos += walker.dir;

                //Clamp walkers within bounds
                walker.pos.x = Mathf.Clamp(walker.pos.x, 1, size.x - 2);
                walker.pos.y = Mathf.Clamp(walker.pos.y, 1, size.y - 2);
                walkers[i] = walker;
            }
        }
    }

    private void CreateWalls()
    {
        for (var x = 0; x < size.x - 1; x++)
        {
            for (var y = 0; y < size.y - 1; y++)
            {
                if (grid[x, y] != GridType.Floor) continue;
                
                for (var neighbourX = x - 1; neighbourX <= x + 1; neighbourX++)
                {
                    for (var neighbourY = y - 1; neighbourY <= y + 1; neighbourY++)
                    {
                        if (neighbourX < 0 || neighbourX >= size.x || neighbourY < 0 || neighbourY >= size.y) continue;
                        
                        if (grid[neighbourX, neighbourY] == GridType.Empty)
                        {
                            grid[neighbourX, neighbourY] = GridType.Wall;
                        }
                    }
                }
            }
        }
    }

    private void RemoveIsolatedWalls()
    {
        for (var x = 0; x < size.x - 1; x++)
        {
            for (var y = 0; y < size.y - 1; y++)
            {
                if (grid[x, y] != GridType.Wall) continue;

                var wallCount = 0;
                
                for (var neighbourX = x - 1; neighbourX <= x + 1; neighbourX++)
                {
                    for (var neighbourY = y - 1; neighbourY <= y + 1; neighbourY++)
                    {
                        if (neighbourX < 0 || neighbourX >= size.x || neighbourY < 0 || neighbourY >= size.y) continue;

                        if (grid[neighbourX, neighbourY] == GridType.Wall)
                        {
                            wallCount++;
                        }
                    }
                }

                if (wallCount == 1)
                {
                    grid[x, y] = GridType.Floor;
                }
            }
        }
    }

    private Vector2 GetRandomDirection()
    {
        var value = Random.Range(0, 4);
        return value switch
        {
            0 => Vector2.up,
            1 => Vector2.down,
            2 => Vector2.left,
            3 => Vector2.right,
            _ => Vector2.right
        };
    }

    private void OnDrawGizmos()
    {
        if (grid == null) return;

        for (var x = 0; x < size.x; x++)
        {
            for (var y = 0; y < size.y; y++)
            {
                var value = grid[x, y];
                Gizmos.color = value switch
                {
                    GridType.Empty => Color.black,
                    GridType.Floor => Color.white,
                    GridType.Wall => Color.blue,
                    _ => Color.red //I don't even know how this could happen, but just to be sure
                };
                var pos = new Vector3(-size.x * 0.5f + x + 0.5f, -size.y * 0.5f + y + 0.5f, 0);
                Gizmos.DrawCube(pos, Vector3.one);
            }
        }
    }
}