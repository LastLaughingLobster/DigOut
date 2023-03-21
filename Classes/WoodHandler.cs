using System;
using Godot;

public class WoodHandler : Handler{

	private TerrainMap terrainMap;
	private Random rnd;

	public WoodHandler(TerrainMap terrainMap, Random rnd) {
		this.terrainMap = terrainMap;
		this.rnd = rnd;
	}
	
	public void process(int x, int y) {


		//To burn in the future
		
	}
}
