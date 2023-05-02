public class Salt : Movable{
    public float friction;
    public float disolveRate;
    public Salt() {
        tileId = (int) Elements.Salt;
        friction = 0.30f;
        disolveRate = 0.3f;
    }
}