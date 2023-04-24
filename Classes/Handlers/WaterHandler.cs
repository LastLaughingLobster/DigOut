using System;
using Godot;

public class WaterHandler : Handler
{
    private TerrainMap terrainMap;
    private Random rnd;
    private const float pressureThreshold = 0.1f;

    public WaterHandler(TerrainMap terrainMap, Random rnd)
    {
        this.terrainMap = terrainMap;
        this.rnd = rnd;
    }

    public void process(int x, int y, Water water)
    {

        bool downClear = (terrainMap.getTerrainMapPointByIndex(x, y - 1).tileId == (int)Elements.Blank)
                      && y - 1 > 0;

        //down
        if (downClear)
        {
            MoveWithPressure(x, y, new Vector2(0, -1), water);
            return;
        }

		bool downLeftSideClear = x + 1 < terrainMap.getWidth() && terrainMap.getTerrainMapPointByIndex(x + 1, y - 1).tileId == (int)Elements.Blank && y - 1 > 0;
        bool downRightSideClear = x - 1 >= 0 && terrainMap.getTerrainMapPointByIndex(x - 1, y - 1).tileId == (int)Elements.Blank && y - 1 > 0;

		if (downLeftSideClear && downRightSideClear)
        {
            if (((int)rnd.Next(0, 9)) < 5)
            {
                MoveWithPressure(x, y, new Vector2(-1,-1), water);
                return;
            }
            else
            {
                MoveWithPressure(x, y, new Vector2(1,-1), water);
                return;
            }
        }

		if (!downLeftSideClear && downRightSideClear)
        {
            MoveWithPressure(x, y, new Vector2(-1,-1), water);
            return;
        }

        if (downLeftSideClear && !downRightSideClear)
        {
            MoveWithPressure(x, y, new Vector2(1,-1), water);
            return;
        }

        // if(water.pressure < pressureThreshold)
        //     return;

        Vector2 rightSideIndex = FindClearPositionRight(x, y, water.disperseRate);
        Vector2 leftSideIndex = FindClearPositionLeft(x, y, water.disperseRate);

		bool leftSideClear = !(leftSideIndex == Vector2.Zero);
		bool rightSideClear = !(rightSideIndex == Vector2.Zero);

        // Check left and right
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

        Element top = terrainMap.getTerrainMapPointByIndex(x + 1, y);
        if(top is Sand sand)
            terrainMap.Swap(x,y,x + 1, y);

        return;
    }

    private Vector2 FindClearPositionLeft(int x, int y, int dispersionRate)
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

    private Vector2 FindClearPositionRight(int x, int y, int dispersionRate)
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

    private void MoveWithPressure(int x, int y, Vector2 direction, Water water)
    {
        Element neighbor = terrainMap.getTerrainMapPointByIndex((int)(x + direction.x), (int)(y + direction.y));
        if (neighbor.tileId == (int)Elements.Blank)
        {   
            water.pressure = water.pressure < pressureThreshold ? water.pressure : water.pressure / 2;
            terrainMap.Swap(x, y, (int)(x + direction.x), (int)(y + direction.y));
        }
        else if (neighbor.tileId == (int)Elements.Water)
        {
            Water neighborWater = neighbor as Water;
            float pressureDifference = water.pressure - neighborWater.pressure;
            if (pressureDifference > pressureThreshold)
            {
                float pressureExchange = pressureDifference / 2;
                water.pressure -= pressureExchange;
                neighborWater.pressure += pressureExchange;
                terrainMap.Swap(x, y, (int)(x + direction.x), (int)(y + direction.y));
            }
        }
    }

}
