public abstract class Element{
    public int tileId { get; set; }
    public bool processed { get; set; }

    public Element(){
        processed = false;
    }
}