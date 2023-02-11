public abstract class BuffBase
{
    public int duration;

    public abstract void makeEffect(Unit unit);

    public int reduceDuration() {
        return --duration;
    }
}
