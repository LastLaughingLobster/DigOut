using Godot;
using System;

public class World : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	[Export]
	int BrushSize;

	[Export(PropertyHint.Range, "1,10,1")]
	int numR;

	[Export]
	public Godot.TileMap pixelMap;

	[Export]
	public Vector2 tMapSize;

	[Export]
	public float gravity;

	[Export]
	public float terminalVelocity;

	int width, height;
	Random rnd;

	Vector2[] BoundsInt;

	bool processState;

	//int[,] terrainMap;

	public TerrainMap terrainMap;

	public HeadProcess headProcess;


	// Called when the node enters the scene tree for the first tllime.
	public override void _Ready()
	{
		rnd = new Random();
		BoundsInt = new Vector2[9];
		pixelMap = GetChild(0) as Godot.TileMap;
		processState = true;
		
		terrainMap = new TerrainMap((int)tMapSize.x, (int)tMapSize.y);
		headProcess = new HeadProcess(terrainMap, rnd, gravity, terminalVelocity);
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
			
			DrawSquare(cellPos, BrushSize, Elements.Water);
		}

		if(Input.IsActionPressed("Mouse_right"))
		{
			Vector2 mousePos = GetGlobalMousePosition();
			Vector2 cellPos = pixelMap.WorldToMap(mousePos);
			
			DrawSquare(cellPos, BrushSize, Elements.Salt);
		}

		if(Input.IsActionPressed("sand_button"))
		{
			Vector2 mousePos = GetGlobalMousePosition();
			Vector2 cellPos = pixelMap.WorldToMap(mousePos);
			
			DrawSquare(cellPos, BrushSize, Elements.Sand);
		}

		if (Input.IsActionPressed("Mouse_middle"))
		{
			Vector2 mousePos = GetGlobalMousePosition();
			Vector2 cellPos = pixelMap.WorldToMap(mousePos);
			
			DrawSquare(cellPos, BrushSize, Elements.Wood);
		}
		
	  	
		Looper();
		Renderer();
	}

	public void DrawSquare(Vector2 pos, int size, Elements element)
	{
		for (int x = 0; x < size; x++)
		{
			for (int y = 0; y < size; y++)
			{
				Vector2 cellPos = new Vector2(pos.x + x, pos.y + y);
				pixelMap.SetCellv(cellPos, (int)element);
				terrainMap.setTerrainMapDataValueFromWorldPos(cellPos, GetTerrainElementFromElement(element));
			}
		}
	}

	public Element GetTerrainElementFromElement(Elements element)
	{
		switch (element)
		{
			case Elements.Sand:
				return new Sand();
			case Elements.Water:
				return new Water();
			case Elements.Wood:
				return new Wood();
			case Elements.Salt:
				return new Salt();
			default:
				return new Blank();
		}
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
				}
				else if(terrainMap.getTerrainMapPointByIndex(x,y).tileId == (int)Elements.Salt)
				{
					SetCellLogical(x,y,Elements.Salt);
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
				headProcess.Process(x,y, terrainMap.getTerrainMapPointByIndex(x,y), processState);
			}
		}

		processState = !processState;
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
