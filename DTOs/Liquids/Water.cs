using Godot;

public class Water : Liquid{
    public Water() {
        tileId = (int) Elements.Water;
        disperseRate = 5;
        pressure = 1.0f;
        direction = Vector2.Up;
        absSpeed = 1.0f;
    }
}