public class Water : Liquid{
    public float salinity;
    public Water() {
        tileId = (int) Elements.Water;
        disperseRate = 5;
        salinity = 0f;
    }
}