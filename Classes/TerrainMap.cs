using Godot;
using System;

public class TerrainMap
{
    private Element[] terrainMap;

    private int height;

    private int width;

    public TerrainMap(int width, int height){
        this.height = height;
        this.width = width;
        this.terrainMap = new Element[width * height];
    }

    public void setTerrainMapDataValue(int x, int y, Element value){
        if (isValidIndex(x, y))
            this.terrainMap[linearIndex(x, y)] = value;
        
    }

    public void setTerrainMapDataValueFromWorldPos(Vector2 cellPos, Element value){
        int x = (int)(-cellPos.x + width / 2);
        int y = (int)(-cellPos.y + height / 2);
        if (isValidIndex(x, y))
        {
            terrainMap[linearIndex(x, y)] = value;
        }
    }

    public Element getTerrainMapPointByIndex(int x, int y){
        if (isValidIndex(x, y))
            return this.terrainMap[linearIndex(x, y)];

        return null;
    }

    public Element getTerrainMapPointByVector(Vector2 pos){
        int x = (int)pos.x;
        int y = (int)pos.y;
        if (isValidIndex(x, y))
        {
            return this.terrainMap[linearIndex(x, y)];
        }
        return null;
    }

    public void Swap(int x1, int y1, int x2, int y2)
    {
        if (isValidIndex(x1, y1) && isValidIndex(x2, y2))
        {
            Element temp = terrainMap[linearIndex(x1, y1)];
            terrainMap[linearIndex(x1, y1)] = terrainMap[linearIndex(x2, y2)];
            terrainMap[linearIndex(x2, y2)] = temp;
        }
    }

    public int linearIndex(int x, int y){
        return x + y * width;
    }

    public bool isValidIndex(int x, int y){
        return x >= 0 && x < width && y >= 0 && y < height;
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
