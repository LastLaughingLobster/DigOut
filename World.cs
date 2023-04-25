using Godot;
using System;
using System.Collections.Generic;

public class World : Node2D
{
	[Export(PropertyHint.Range, "1,10,1")]
	int numR;

	[Export]
	public Vector2 tMapSize;

	[Export]
	public Vector2 cellSize;

	int width, height;
	Random rnd;

	Vector2[] BoundsInt;

	public TerrainMap terrainMap;

	public HeadProcess headProcess;

	private Dictionary<Vector2, ColorRect> colorRects;

	Color sandColor = new Color(1, 1, 0);
	Color waterColor = new Color(0, 0, 1);
	Color woodColor = new Color(0.5f, 0.25f, 0);

	Color black = new Color(0, 0, 0);

	public override void _Ready()
	{
		(GetNode("ViewportContainer/Viewport") as Viewport).Size = new Vector2(1280, 720);

		colorRects = new Dictionary<Vector2, ColorRect>();

		rnd = new Random();
		BoundsInt = new Vector2[9];
		terrainMap = new TerrainMap((int)tMapSize.x, (int)tMapSize.y);
		headProcess = new HeadProcess(terrainMap, rnd);
		initPos();
	}

	public override void _Process(float delta)
	{
		Vector2 mousePos = GetGlobalMousePosition();
		Vector2 cellPos = WorldToCellPos(mousePos);
		Vector2 terrainPos = TileMapToTerrainPos(cellPos);

		if (Input.IsActionPressed("Mouse_left"))
		{
			terrainMap.setTerrainMapDataValueFromWorldPos(terrainPos, new Sand());
		}

		if (Input.IsActionPressed("Mouse_right"))
		{
			terrainMap.setTerrainMapDataValueFromWorldPos(terrainPos, new Water());
		}

		if (Input.IsActionPressed("Mouse_middle"))
		{
			terrainMap.setTerrainMapDataValueFromWorldPos(terrainPos, new Wood());
		}

		Renderer();
		Looper();
	}

	public void Renderer()
	{
		for (int y = 0; y < terrainMap.getHeight(); y++)
		{
			for (int x = 0; x < terrainMap.getWidth(); x++)
			{
				Vector2 pos = new Vector2(x, y);
				Element element = terrainMap.getTerrainMapPointByIndex(x, y);

				if (element.tileId == (int)Elements.Sand)
				{
					SetColorRectColor(pos, sandColor);
				}
				else if (element.tileId == (int)Elements.Water)
				{
					SetColorRectColor(pos, waterColor);
				}
				else if (element.tileId == (int)Elements.Wood)
				{
					SetColorRectColor(pos, woodColor);
				}
				else
				{
					SetColorRectColor(pos, black);
				}
			}
		}
	}

	public void Looper()
	{
		for (int y = terrainMap.getHeight(); y >= 1; y--)
		{
			for (int x = 0; x < terrainMap.getWidth(); x++)
			{
				headProcess.Process(x, y, terrainMap.getTerrainMapPointByIndex(x, y));
			}
		}
	}

	public void SetColorRectColor(Vector2 pos, Color color)
	{
		if (colorRects.ContainsKey(pos))
		{
			colorRects[pos].Color = color;
		}
	}

	private void initPos()
	{
		for (int y = 0; y < terrainMap.getHeight(); y++)
		{
			for (int x = 0; x < terrainMap.getWidth(); x++)
			{
				Vector2 pos = new Vector2(x,y);
				CreateColorRect(pos);
				terrainMap.setTerrainMapDataValue(x, y, new Blank());
				SetColorRectColor(pos, black);
			}
		}
	}

	private void CreateColorRect(Vector2 pos)
	{
		ColorRect colorRect = new ColorRect();
		colorRect.RectMinSize = new Vector2();
		colorRect.RectMinSize = new Vector2(cellSize.x, cellSize.y);
		colorRect.RectPosition = pos * colorRect.RectMinSize;
		colorRect.Color = Colors.Transparent;
		AddChild(colorRect);
		colorRects[pos] = colorRect;
	}

	public void clearMap(bool complete)
	{
		foreach (ColorRect colorRect in colorRects.Values)
		{
			colorRect.QueueFree();
		}

		colorRects.Clear();

		if (complete)
		{
			terrainMap = null;
		}
	}

	private Vector2 TerrainToTileMapPos(Vector2 terrainPos)
	{
		return new Vector2(-terrainPos.x + terrainMap.getWidth() / 2, -terrainPos.y + terrainMap.getHeight() / 2);
	}

	private Vector2 TileMapToTerrainPos(Vector2 tileMapPos)
	{
		return new Vector2(-tileMapPos.x + terrainMap.getWidth() / 2, -tileMapPos.y + terrainMap.getHeight() / 2);
	}

	private Vector2 WorldToCellPos(Vector2 worldPos)
	{
		return (worldPos / new Vector2(cellSize.x, cellSize.y)).Floor();
	}
}
