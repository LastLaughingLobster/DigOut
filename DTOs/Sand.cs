public class Sand : Movable{
    public float friction;
    public Sand() {
        tileId = (int) Elements.Sand;
        friction = 0.90f;
    }
}