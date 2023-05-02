using Godot;
using System;

public class TerrainMap
{
    private Element[,] terrainMap;

    private int height;

    private int width;

    public TerrainMap(int width, int height){
        this.height = height;
        this.width = width;
        this.terrainMap = new Element[width, height];
    }

    public void setTerrainMapDataValue(int x, int y, Element value){

        try
        {
            this.terrainMap[x,y] = value;
        }
        catch (IndexOutOfRangeException e)
        {
            throw new ArgumentOutOfRangeException("Parameter index is out of range.", e);
        }
    }

    public void setTerrainMapDataValueFromWorldPos(Vector2 cellPos, Element value){

        try
        {   
            terrainMap[(int)(-cellPos.x + width /2), (int)(-cellPos.y + height / 2)] = value;
            GD.Print(terrainMap);
        }
        catch (IndexOutOfRangeException e)
        {
            throw new ArgumentOutOfRangeException("Parameter index is out of range.", e);
        }
    }

    public Element getTerrainMapPointByIndex(int x, int y){
        try
        {
            return this.terrainMap[x,y];
        }
        catch (IndexOutOfRangeException e)
        {
            throw new ArgumentOutOfRangeException("Parameter index is out of range.", e);
        }
    }

    public Element getTerrainMapPointByVector(Vector2 pos){
        try
        {
            return this.terrainMap[(int)pos.x, (int)pos.y];
        }
        catch (IndexOutOfRangeException e)
        {
            throw new ArgumentOutOfRangeException("Parameter index is out of range.", e);
        }
    }

    public void Swap(int x1, int y1, int x2, int y2)
    {
        try
        {
            Element temp = terrainMap[x1, y1];
            terrainMap[x1, y1] = terrainMap[x2, y2];
            terrainMap[x2, y2] = temp;
        }
        catch (IndexOutOfRangeException e)
        {
            throw new ArgumentOutOfRangeException("Parameter index is out of range.", e);
        }
    }

    public void SwapAndChangeState(int x1, int y1, int x2, int y2, bool processState)
    {
        try
        {   
            terrainMap[x2, y2].processed = processState;
            Element temp = terrainMap[x1, y1];
            terrainMap[x1, y1] = terrainMap[x2, y2];
            terrainMap[x2, y2] = temp;
        }
        catch (IndexOutOfRangeException e)
        {
            throw new ArgumentOutOfRangeException("Parameter index is out of range.", e);
        }
    }

    public int convertFromTerrainMapToWorldPosX(int x){
        return -x + this.width / 2;
    }

    public int convertFromTerrainMapToWorldPosY(int y){
        return -y + this.height / 2;
    }

    public int getWidth(){
        return this.width;
    }

    public int getHeight(){
        return this.height;
    }


}