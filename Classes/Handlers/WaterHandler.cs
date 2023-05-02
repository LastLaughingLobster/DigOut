using System;
using Godot;

public class WaterHandler : IHandler
{
    private TerrainMap terrainMap;
    private Random rnd;
    private float gravity;
    private float terminalVelocity;

    public WaterHandler(TerrainMap terrainMap, Random rnd, float gravity, float terminalVelocity)
    {
        this.terrainMap = terrainMap;
        this.rnd = rnd;
        this.gravity = gravity;
        this.terminalVelocity = terminalVelocity;
    }

    public void Process(int x, int y, Element element, bool processState)
    {
        Water water = (Water)element;

        // Update the water's velocity based on the gravity factor
        water.velocity.y += gravity;

        // Limit the water's velocity to the terminal velocity
        if (water.velocity.y > terminalVelocity)
        {
            water.velocity.y = terminalVelocity;
        }

        // Calculate the new potential position based on the updated velocity
        int newY = y - (int)Math.Round(water.velocity.y);

        // Make sure the new position is within bounds and the element below is Blank
        if (newY >= 0 && newY < terrainMap.getHeight())
        {
            Element belowElement = terrainMap.getTerrainMapPointByIndex(x, newY);
            bool belowIsBlank = belowElement.tileId == (int)Elements.Blank;

            if (belowIsBlank)
            {
                terrainMap.Swap(x, y, x, newY);
                return;
            }
        }

        // Reset the velocity if water cannot move downwards
        water.velocity.y = 0;

		bool downLeftSideClear = x + 1 < terrainMap.getWidth() && terrainMap.getTerrainMapPointByIndex(x + 1, y - 1).tileId == (int)Elements.Blank && y - 1 > 0;
        bool downRightSideClear = x - 1 >= 0 && terrainMap.getTerrainMapPointByIndex(x - 1, y - 1).tileId == (int)Elements.Blank && y - 1 > 0;

		if (downLeftSideClear && downRightSideClear)
        {
            if (((int)rnd.Next(0, 9)) < 5)
            {
                terrainMap.Swap(x, y, x + 1,  y - 1);
                return;
            }
            else
            {
                terrainMap.Swap(x, y, x - 1, y - 1);
                return;
            }
        }

		if (!downLeftSideClear && downRightSideClear)
        {
            terrainMap.Swap(x, y, x - 1, y - 1);
            return;
        }

        if (downLeftSideClear && !downRightSideClear)
        {
            terrainMap.Swap(x, y, x + 1,  y - 1);
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
}
