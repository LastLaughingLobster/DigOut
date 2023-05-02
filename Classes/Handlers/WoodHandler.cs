using System;
using Godot;

public class WoodHandler : IHandler{

	private TerrainMap terrainMap;
	private Random rnd;

	public WoodHandler(TerrainMap terrainMap, Random rnd) {
		this.terrainMap = terrainMap;
		this.rnd = rnd;
	}
	
	public void Process(int x, int y, Element wood, bool processState) {


		//To burn in the future
		
	}
}
