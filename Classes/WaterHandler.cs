using System;
using Godot;

public class WaterHandler : Handler{

    private TerrainMap terrainMap;
    private Random rnd;

    public WaterHandler(TerrainMap terrainMap, Random rnd) {
        this.terrainMap = terrainMap;
        this.rnd = rnd;
    }
	
	public void process( int x, int y){

		bool downClear = (terrainMap.getTerrainMapPointByIndex(x, y - 1).tileId == (int)Elements.Blank) 
                        && y - 1 > 0; 
				
		//down
		if (downClear)
		{
			terrainMap.setTerrainMapDataValue(x, y - 1, terrainMap.getTerrainMapPointByIndex(x, y));
			terrainMap.setTerrainMapDataValue(x, y, new Blank());
			return;
		}
				
		bool rightSideClear  = x - 1 >= 0 ? (terrainMap.getTerrainMapPointByIndex(x - 1, y).tileId == (int)Elements.Blank) : false;
		bool leftSideClear   = x + 1 < terrainMap.getWidth() ? (terrainMap.getTerrainMapPointByIndex(x + 1, y).tileId == (int)Elements.Blank) : false;
		
		if(!leftSideClear && !rightSideClear){
			return;
		}
		
		if(leftSideClear && rightSideClear){
			if(((int)rnd.Next(0,9)) < 5)
			{
				GoRight(x,y);
				return;
			} else
			{
				GoLef(x,y);
				return;
			}
		}
				
				
		if(!leftSideClear && rightSideClear){
			GoRight(x, y);
			return;
		}
		
		if(leftSideClear && !rightSideClear){
			GoLef(x, y);
			return;
		}
				
		return;
	}

    public void GoLef(int x, int y)
	{	
		var temp = terrainMap.getTerrainMapPointByIndex(x + 1, y);
		terrainMap.setTerrainMapDataValue(x + 1, y, terrainMap.getTerrainMapPointByIndex(x, y));
		terrainMap.setTerrainMapDataValue(x, y, temp);
	}
		
	public void GoRight(int x, int y)
	{
		var temp = terrainMap.getTerrainMapPointByIndex(x - 1, y);
		terrainMap.setTerrainMapDataValue(x - 1, y, terrainMap.getTerrainMapPointByIndex(x, y));
		terrainMap.setTerrainMapDataValue(x, y, temp);
	}
}