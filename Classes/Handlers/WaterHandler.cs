using System;
using Godot;

public class WaterHandler : Handler
{
    private TerrainMap terrainMap;
    private Random rnd;

    public WaterHandler(TerrainMap terrainMap, Random rnd)
    {
        this.terrainMap = terrainMap;
        this.rnd = rnd;
    }

    public void process(int x, int y, Water water)
    {
        if (y + 1 >= terrainMap.getHeight())
        {
            return;
        }

        bool downClear = (terrainMap.getTerrainMapPointByIndex(x, y + 1).tileId == (int)Elements.Blank);

        //down
        if (downClear)
        {
            terrainMap.Swap(x, y, x, y + 1);
            return;
        }

		bool downLeftSideClear = x - 1 >= 0 && terrainMap.getTerrainMapPointByIndex(x - 1, y + 1).tileId == (int)Elements.Blank && y + 1 < terrainMap.getHeight();
        bool downRightSideClear = x + 1 < terrainMap.getWidth() && terrainMap.getTerrainMapPointByIndex(x + 1, y + 1).tileId == (int)Elements.Blank && y + 1 < terrainMap.getHeight();

		if (downLeftSideClear && downRightSideClear)
        {
            if (((int)rnd.Next(0, 9)) < 5)
            {
                terrainMap.Swap(x, y, x - 1,  y + 1);
                return;
            }
            else
            {
                terrainMap.Swap(x, y, x + 1, y + 1);
                return;
            }
        }

		if (!downLeftSideClear && downRightSideClear)
        {
            terrainMap.Swap(x, y, x + 1, y + 1);
            return;
        }

        if (downLeftSideClear && !downRightSideClear)
        {
            terrainMap.Swap(x, y, x - 1,  y + 1);
            return;
        }

        Vector2 rightSideIndex = FindClearPositionRight(x, y, water.disperseRate);
        Vector2 leftSideIndex = FindClearPositionLeft(x, y, water.disperseRate);

		bool leftSideClear = !(leftSideIndex == Vector2.Zero);
		bool rightSideClear = !(rightSideIndex == Vector2.Zero);

        if (!leftSideClear && !rightSideClear)
            return;
        

        if (leftSideClear && rightSideClear)
        {
            if (((int)rnd.Next(0, 9)) < 5)
            {
                terrainMap.Swap(x, y, (int)rightSideIndex.x, (int)rightSideIndex.y);
                return;
            }
            else
            {
                terrainMap.Swap(x, y, (int)leftSideIndex.x, (int)leftSideIndex.y);
                return;
            }
        }

        if (!leftSideClear && rightSideClear)
        {
            terrainMap.Swap(x, y, (int)rightSideIndex.x, (int)rightSideIndex.y);
            return;
        }

        if (leftSideClear && !rightSideClear)
        {
            terrainMap.Swap(x, y, (int)leftSideIndex.x, (int)leftSideIndex.y);
            return;
        }

        return;
    }

    private Vector2 FindClearPositionLeft(int x, int y, int dispersionRate)
    {
        Vector2 output = Vector2.Zero;
        for (int i = 1; i <= dispersionRate; i++)
        {
            if (!(x - i >= 0 && terrainMap.getTerrainMapPointByIndex(x - i, y).tileId == (int)Elements.Blank))
                break;
            
            output.x = x - i;
            output.y = y;
        }

        return output;
    }

    private Vector2 FindClearPositionRight(int x, int y, int dispersionRate)
    {
        Vector2 output = Vector2.Zero;
        for (int i = 1; i <= dispersionRate; i++)
        {
            if (!(x + i < terrainMap.getWidth() && terrainMap.getTerrainMapPointByIndex(x + i, y).tileId == (int)Elements.Blank))
                break;

            output.x = x + i;
            output.y = y;
        }

        return output;
    }
}

        
