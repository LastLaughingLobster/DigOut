using System;
using Godot;

public class SandHandler : Handler
{
    private TerrainMap terrainMap;
    private Random rnd;

    public SandHandler(TerrainMap terrainMap, Random rnd)
    {
        this.terrainMap = terrainMap;
        this.rnd = rnd;
    }

    public void process(int x, int y)
    {
        if (y + 1 >= terrainMap.getHeight())
        {
            return;
        }

        Element down = terrainMap.getTerrainMapPointByIndex(x, y + 1);

        bool downIsBlack = down.tileId == (int)Elements.Blank;
        bool downIsWater = down.tileId == (int)Elements.Water;

        bool downClear = downIsBlack || downIsWater;

        //down
        if (downClear)
        {
            terrainMap.Swap(x, y, x, y + 1);
            return;
        }

        bool rightSideClear = x + 1 < terrainMap.getWidth() && (terrainMap.getTerrainMapPointByIndex(x + 1, y + 1).tileId == (int)Elements.Blank || terrainMap.getTerrainMapPointByIndex(x + 1, y + 1).tileId == (int)Elements.Water);
        bool leftSideClear = x - 1 >= 0 && (terrainMap.getTerrainMapPointByIndex(x - 1, y + 1).tileId == (int)Elements.Blank || terrainMap.getTerrainMapPointByIndex(x - 1, y + 1).tileId == (int)Elements.Water);

        if (leftSideClear && rightSideClear)
        {
            if (((int)rnd.Next(0, 9)) < 5)
            {
                terrainMap.Swap(x, y, x + 1, y + 1);
            }
            else
            {
                terrainMap.Swap(x, y, x - 1, y + 1);
            }
        }

        if (leftSideClear && !rightSideClear)
        {
            terrainMap.Swap(x, y, x - 1, y + 1);
            return;
        }

        if (!leftSideClear && rightSideClear)
        {
            terrainMap.Swap(x, y, x + 1, y + 1);
            return;
        }
    }
}
