using Godot;

public class Movable : Element{
    public Vector2 velocity;
    public bool isFalling;
    public Movable() {
        velocity = new Vector2(0,0);
        isFalling = true;
    }
}