using System;
using Godot;

//It handles shit
public class HeadProcess{
    
    private readonly SandHandler sandHandler;

    private readonly SaltHandler saltHandler;

    private readonly WaterHandler waterHandler;

    private readonly WoodHandler woodHandler;

    public HeadProcess (TerrainMap terrainMap, Random rnd, float gravity, float terminalVelocity){
        sandHandler = new SandHandler(terrainMap, rnd, gravity, terminalVelocity);
        saltHandler = new SaltHandler(terrainMap, rnd, gravity, terminalVelocity);
        waterHandler = new WaterHandler(terrainMap, rnd, gravity, terminalVelocity);
        woodHandler = new WoodHandler(terrainMap, rnd);
    }

    public void Process(int x, int y, Element element, bool processState){
        if (element is Sand) {
            sandHandler.Process(x,y, (Sand) element, processState);
        } 
        if (element is Water) {
            waterHandler.Process(x,y, (Water) element, processState);
        } 
        if (element is Wood) {
            woodHandler.Process(x,y, (Wood) element, processState);
        } 
        if (element is Salt) {
            saltHandler.Process(x,y, (Salt) element, processState);
        } 
    }


}