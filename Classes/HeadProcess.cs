using System;
using Godot;

//It handles shit
public class HeadProcess{
    
    private readonly SandHandler sandHandler;

    private readonly WaterHandler waterHandler;

    private readonly WoodHandler woodHandler;

    public HeadProcess (TerrainMap terrainMap, Random rnd){
        sandHandler = new SandHandler(terrainMap, rnd);
        waterHandler = new WaterHandler(terrainMap, rnd);
        woodHandler = new WoodHandler(terrainMap, rnd);
    }

    public void Process(int x, int y, Element element){
        if (element is Sand) {
            sandHandler.process(x,y);
        } 
        if (element is Water) {
            waterHandler.process(x,y, (Water) element);
        } 
        if (element is Wood) {
            woodHandler.process(x,y);
        } 
    }


}