using System;
using Godot;

public class SandHandler : Handler{

	private TerrainMap terrainMap;
	private Random rnd;

	public SandHandler(TerrainMap terrainMap, Random rnd) {
		this.terrainMap = terrainMap;
		this.rnd = rnd;
	}
	
	public void Process(int x, int y) {


		Element down = terrainMap.getTerrainMapPointByIndex(x, y - 1);

		
		bool downIsBlack = down.tileId == (int)Elements.Blank;
		bool downIsWater = down.tileId == (int)Elements.Water;
		bool downIsBoud = y - 1 < 0;
				
		bool downClear = (downIsBlack || downIsWater) && !downIsBoud;

		//down
		if (downClear)
		{
			var temp = terrainMap.getTerrainMapPointByIndex(x, y - 1);
			terrainMap.setTerrainMapDataValue(x, y - 1, terrainMap.getTerrainMapPointByIndex(x, y));
			terrainMap.setTerrainMapDataValue(x, y, temp);
			return;
		}
				
				
		bool rightSideClear  = x - 1 >= 0 ? (terrainMap.getTerrainMapPointByIndex(x - 1, y - 1).tileId == (int)Elements.Blank || terrainMap.getTerrainMapPointByIndex(x - 1, y - 1).tileId == (int) Elements.Water) : false;
		bool leftSideClear   = x + 1 < terrainMap.getWidth() ? (terrainMap.getTerrainMapPointByIndex(x + 1, y - 1).tileId == (int)Elements.Blank || terrainMap.getTerrainMapPointByIndex(x + 1, y - 1).tileId == (int) Elements.Water) : false;
				
		if(leftSideClear && !rightSideClear){
			GoLeftSand(x, y);
			return;
		}
				
		if(!leftSideClear && rightSideClear){
			GoRightSand(x, y);
			return;
		}
				
		if(!leftSideClear && !rightSideClear){
			return;
		}
				
		if(((int)rnd.Next(0,9)) < 5)
		{
			GoRightSand(x,y);
		}else
		{
			GoLeftSand(x,y);
		}
	}

	private void GoLeftSand(int x, int y)
	{
		var temp = terrainMap.getTerrainMapPointByIndex(x + 1, y - 1);
		terrainMap.setTerrainMapDataValue(x + 1, y - 1, terrainMap.getTerrainMapPointByIndex(x, y));
		terrainMap.setTerrainMapDataValue(x, y, temp);
	}
		
	private void GoRightSand(int x, int y)
	{
		var temp = terrainMap.getTerrainMapPointByIndex(x - 1, y - 1);
		terrainMap.setTerrainMapDataValue(x - 1, y - 1, terrainMap.getTerrainMapPointByIndex(x, y));
		terrainMap.setTerrainMapDataValue(x, y, temp);
	}
}
