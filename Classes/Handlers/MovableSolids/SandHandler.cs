using System;
using Godot;

public class SandHandler : IHandler
{
    private TerrainMap terrainMap;
    private Random rnd;
    private float gravity;
    private float terminalVelocity;

    public SandHandler(TerrainMap terrainMap, Random rnd, float gravity, float terminalVelocity)
    {
        this.terrainMap = terrainMap;
        this.rnd = rnd;
        this.gravity = gravity;
        this.terminalVelocity = terminalVelocity;
    }

    public void Process(int x, int y, Element element, bool processState)
    {

        if(element.processed == processState)
            return;

        // Cast the element to Sand to access the Velocity property
        Sand sand = element as Sand;

        ProcessDownMovement(x,y,sand, processState);
        ProcessSideWayMovement(x,y, processState);

        element.processed = processState;
    }

    private void ProcessDownMovement(int x, int y, Sand sand, bool processState){

       
        // Update the sand's velocity based on the gravity factor
        sand.velocity.y += gravity;

        // Limit the sand's velocity to the terminal velocity
        if (sand.velocity.y > terminalVelocity)
        {
            sand.velocity.y = terminalVelocity;
        }

        // this movement logic in the y axis is similar to the x axis please create a method 
        // Calculate the new potential position based on the updated velocity
        int newY = y - (int)Math.Round(sand.velocity.y);

        // Find the first obstacle in the sand's path
        for (int i = y - 1; i >= newY && i >= 0; i--)
        {
            Element obstacle = terrainMap.getTerrainMapPointByIndex(x, i);
            bool obstacleIsBlankOrWater = obstacle.tileId == (int)Elements.Blank || obstacle.tileId == (int)Elements.Water;

            if (!obstacleIsBlankOrWater)
            {
                newY = i + 1;
                break;
            }
        }

        bool belowIsBlankOrWater = false;

        // Make sure the new position is within bounds and the element below is Blank or Water
        if (newY >= 0 && newY < terrainMap.getHeight())
        {
            Element belowElement = terrainMap.getTerrainMapPointByIndex(x, newY);
            belowIsBlankOrWater = belowElement.tileId == (int)Elements.Blank || belowElement.tileId == (int)Elements.Water;

            if (belowIsBlankOrWater)
            {
                terrainMap.SwapAndChangeState(x, y, x, newY, processState);
                return;
            }
        }

        // end of it

        // If sand hits an immovable object or lower bound of the simulation
        if (!belowIsBlankOrWater)
        {
            sand.isFalling = false;

            float tempYSpeed = sand.velocity.y;

            // Reset the velocity if sand cannot move downwards
            sand.velocity.y = 0;

            if (tempYSpeed != 0){
                sand.velocity.x = tempYSpeed * sand.friction;
            }

            // Check if the preserved X velocity is below a certain threshold, and if so, set it to zero
            if (Math.Abs(sand.velocity.x) < 2f)
            {
                sand.velocity.x = 0;
            }
            else
            {
                 // Apply the decay factor
                sand.velocity.x *= sand.friction;

                // Randomly choose the direction for the X axis movement (left or right)
                int direction = rnd.Next(0, 2) * 2 - 1; // Generates either -1 or 1


                // This is the similar part that should be added to a function
                // Calculate the new X position
                int newX = x + direction * (int)Math.Round(sand.velocity.x);

                // Find the first obstacle in the sand's path along the X-axis
                for (int i = x + direction; (direction > 0 && i < newX && i < terrainMap.getWidth()) || (direction < 0 && i > newX && i >= 0); i += direction)
                {
                    Element obstacle = terrainMap.getTerrainMapPointByIndex(i, y);
                    bool obstacleIsBlankOrWater = obstacle.tileId == (int)Elements.Blank || obstacle.tileId == (int)Elements.Water;

                    if (!obstacleIsBlankOrWater)
                    {
                        newX = i - direction;
                        break;
                    }
                }

                // Check if the new X position is within bounds and the element is Blank or Water
                if (newX >= 0 && newX < terrainMap.getWidth())
                {
                    Element sideElement = terrainMap.getTerrainMapPointByIndex(newX, y);
                    bool sideIsBlankOrWater = sideElement.tileId == (int)Elements.Blank || sideElement.tileId == (int)Elements.Water;

                    if (sideIsBlankOrWater)
                    {
                        terrainMap.SwapAndChangeState(x, y, newX, y, processState);
                        return;
                    }
                }
                // end of similar part
            }
        }else{
            sand.isFalling = true;
        }
    }

    private void ProcessSideWayMovement(int x, int y, bool processState) {
        
        bool rightSideClear = x - 1 >= 0 ? (terrainMap.getTerrainMapPointByIndex(x - 1, y - 1).tileId == (int)Elements.Blank || terrainMap.getTerrainMapPointByIndex(x - 1, y - 1).tileId == (int)Elements.Water) : false;
        bool leftSideClear = x + 1 < terrainMap.getWidth() ? (terrainMap.getTerrainMapPointByIndex(x + 1, y - 1).tileId == (int)Elements.Blank || terrainMap.getTerrainMapPointByIndex(x + 1, y - 1).tileId == (int)Elements.Water) : false;

        if (leftSideClear && rightSideClear)
        {
            if (((int)rnd.Next(0, 9)) < 5)
            {
                terrainMap.SwapAndChangeState(x, y, x - 1, y - 1, processState);
            }
            else
            {
                terrainMap.SwapAndChangeState(x, y, x + 1, y - 1, processState);
            }
        }

        if (leftSideClear && !rightSideClear)
        {
            terrainMap.SwapAndChangeState(x, y, x + 1, y - 1, processState);
            return;
        }

        if (!leftSideClear && rightSideClear)
        {
            terrainMap.SwapAndChangeState(x, y, x - 1, y - 1, processState);
            return;
        }
    }
}

