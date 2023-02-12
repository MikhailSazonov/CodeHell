public class CoffeeBuff : BuffBase
{
    public int coef = 2;

    public CoffeeBuff() {
        totalDuration = 8;
        buffName = "coffee";
    }

    public override void makeEffect(Unit unit) {
        unit.chanceCritical *= coef;
    }
}
