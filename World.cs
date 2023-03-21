using Godot;
using System;

public class World : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

   [Export(PropertyHint.Range, "0,100,5")]
	int iniChance;


	[Export(PropertyHint.Range, "1,8,1")]
	int birthLimit;

	[Export(PropertyHint.Range, "1,8,1")]
	int deathLimit;


	[Export(PropertyHint.Range, "1,10,1")]
	int numR;

	[Export]
	public Godot.TileMap pixelMap;
	[Export]
	public int SandTile, BlankTile, WaterTile, WoodTile;

	[Export]
	public Vector2 tMapSize;


	int width, height;
	Random rnd;

	Vector2[] BoundsInt;

	//int[,] terrainMap;

	public TerrainMap terrainMap;

	public HeadProcess headProcess;


	// Called when the node enters the scene tree for the first tllime.
	public override void _Ready()
	{
		rnd = new Random();
		BoundsInt = new Vector2[9];
		pixelMap = GetChild(0) as Godot.TileMap;
		
		terrainMap = new TerrainMap((int)tMapSize.x, (int)tMapSize.y);
		headProcess = new HeadProcess(terrainMap, rnd);
		initPos();

		int j = 0;
		for (int i = -1; i < 2; i++)
		{
			BoundsInt[j] = new Vector2(i, -1);
			BoundsInt[j+1] = new Vector2(i, 0);
			BoundsInt[j+2] =  new Vector2(i, 1);

			j += 3;

		}


	}

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
		
		if (Input.IsActionPressed("Mouse_left"))
		{
			Vector2 mousePos = GetGlobalMousePosition();
			Vector2 cellPos = pixelMap.WorldToMap(mousePos);
			
			pixelMap.SetCellv(cellPos, (int)Elements.Sand);
			
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Sand());
			pixelMap.SetCellv(cellPos, (int)Elements.Sand);

			//left
			cellPos.x = cellPos.x - 1;
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Sand());
			pixelMap.SetCellv(cellPos, (int)Elements.Sand);
			cellPos.x = cellPos.x + 1;

			//Right
			cellPos.x = cellPos.x + 1;
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Sand());
			pixelMap.SetCellv(cellPos, (int)Elements.Sand);
			cellPos.x = cellPos.x - 1;

			//top 
			cellPos.y = cellPos.y + 1;
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Sand());
			pixelMap.SetCellv(cellPos, (int)Elements.Sand);
			cellPos.y = cellPos.y - 1;

			//botton
			cellPos.y = cellPos.y - 1;
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Sand());
			pixelMap.SetCellv(cellPos, (int)Elements.Sand);
			cellPos.y = cellPos.y + 1;

			//botton-left
			cellPos.y = cellPos.y - 1;
			cellPos.x = cellPos.x - 1;
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Sand());
			pixelMap.SetCellv(cellPos, (int)Elements.Sand);
			cellPos.y = cellPos.y + 1;
			cellPos.x = cellPos.x + 1;

			//botton-right
			cellPos.y = cellPos.y - 1;
			cellPos.x = cellPos.x + 1;
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Sand());
			pixelMap.SetCellv(cellPos, (int)Elements.Sand);
			
			

			GD.Print("Mouse pos =", mousePos);
			GD.Print("Cell pos = ", cellPos);
		}

		if(Input.IsActionPressed("Mouse_right"))
		{
			
			Vector2 mousePos = GetGlobalMousePosition();
			Vector2 cellPos = pixelMap.WorldToMap(mousePos);
			
			pixelMap.SetCellv(cellPos, (int)Elements.Water);
			
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Water());
			pixelMap.SetCellv(cellPos, (int)Elements.Water);

			//left
			cellPos.x = cellPos.x - 1;
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Water());
			pixelMap.SetCellv(cellPos, (int)Elements.Water);
			cellPos.x = cellPos.x + 1;

			//Right
			cellPos.x = cellPos.x + 1;
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Water());
			pixelMap.SetCellv(cellPos, (int)Elements.Water);
			cellPos.x = cellPos.x - 1;

			//top 
			cellPos.y = cellPos.y + 1;
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Water());
			pixelMap.SetCellv(cellPos, (int)Elements.Water);
			cellPos.y = cellPos.y - 1;

			//botton
			cellPos.y = cellPos.y - 1;
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Water());
			pixelMap.SetCellv(cellPos, (int)Elements.Water);
			cellPos.y = cellPos.y + 1;

			//botton-left
			cellPos.y = cellPos.y - 1;
			cellPos.x = cellPos.x - 1;
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Water());
			pixelMap.SetCellv(cellPos, (int)Elements.Water);
			cellPos.y = cellPos.y + 1;
			cellPos.x = cellPos.x + 1;

			//botton-right
			cellPos.y = cellPos.y - 1;
			cellPos.x = cellPos.x + 1;
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Water());
			pixelMap.SetCellv(cellPos, (int)Elements.Water);
			
 
		}
		if (Input.IsActionPressed("Mouse_middle"))
		{
			Vector2 mousePos = GetGlobalMousePosition();
			Vector2 cellPos = pixelMap.WorldToMap(mousePos);
			
			pixelMap.SetCellv(cellPos, (int)Elements.Wood);
			
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Wood());
			pixelMap.SetCellv(cellPos, (int)Elements.Wood);

			//left
			cellPos.x = cellPos.x - 1;
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Wood());
			pixelMap.SetCellv(cellPos, (int)Elements.Wood);
			cellPos.x = cellPos.x + 1;

			//Right
			cellPos.x = cellPos.x + 1;
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Wood());
			pixelMap.SetCellv(cellPos, (int)Elements.Wood);
			cellPos.x = cellPos.x - 1;

			//top 
			cellPos.y = cellPos.y + 1;
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Wood());
			pixelMap.SetCellv(cellPos, (int)Elements.Wood);
			cellPos.y = cellPos.y - 1;

			//botton
			cellPos.y = cellPos.y - 1;
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Wood());
			pixelMap.SetCellv(cellPos, (int)Elements.Wood);
			cellPos.y = cellPos.y + 1;

			//botton-left
			cellPos.y = cellPos.y - 1;
			cellPos.x = cellPos.x - 1;
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Wood());
			pixelMap.SetCellv(cellPos, (int)Elements.Wood);
			cellPos.y = cellPos.y + 1;
			cellPos.x = cellPos.x + 1;

			//botton-right
			cellPos.y = cellPos.y - 1;
			cellPos.x = cellPos.x + 1; 
			terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, new Wood());
			pixelMap.SetCellv(cellPos, (int)Elements.Wood);
			
			

			GD.Print("Mouse pos =", mousePos);
			GD.Print("Cell pos = ", cellPos);
		}
		
	  	
		Looper();
		Renderer();
  }

	public void Renderer(){
		
		for (int x = 0; x < terrainMap.getWidth(); x++)
		{
			for (int y = 0; y < terrainMap.getHeight(); y++)
			{
				if (terrainMap.getTerrainMapPointByIndex(x,y).tileId == (int)Elements.Sand)
				{
					SetCellLogical(x,y,Elements.Sand);
				}
				else if(terrainMap.getTerrainMapPointByIndex(x,y).tileId == (int)Elements.Water)
				{
					SetCellLogical(x,y,Elements.Water);
				}
				else if(terrainMap.getTerrainMapPointByIndex(x,y).tileId == (int)Elements.Wood)
				{
					SetCellLogical(x,y,Elements.Wood);
				}else
				{
					SetCellLogical(x,y,Elements.Blank);
				}
			}
		}
	}

	public void SetCellLogical(int x, int y, Elements element){
		pixelMap.SetCell(-x + terrainMap.getWidth() / 2, -y + terrainMap.getHeight() / 2, (int)element);
	}
	
	public void Looper(){
		
		for (int x = 0; x < terrainMap.getWidth(); x++)
		{
			for (int y = 1; y < terrainMap.getHeight(); y++)
			{
				headProcess.Process(x,y, terrainMap.getTerrainMapPointByIndex(x,y));
			}
		}
	}
	
	

	private  void initPos()
	{
		for (int x = 0; x < terrainMap.getWidth(); x++)
		{
			for (int y = 0; y < terrainMap.getHeight(); y++)
			{
				pixelMap.SetCell(-x + terrainMap.getWidth() / 2, -y + terrainMap.getHeight() / 2, (int)Elements.Blank);
				terrainMap.setTerrainMapDataValue(x, y, new Blank());
			}

		}

	}




	public void clearMap(bool complete)
	{
		pixelMap.Clear();
		
		if (complete)
		{
			terrainMap = null;
		}
	}


}
