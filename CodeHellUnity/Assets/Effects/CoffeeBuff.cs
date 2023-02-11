public class CoffeeBuff : BuffBase
{
    public int coef = 2;

    public CoffeeBuff() {
        duration = 8;
    }

    public override void makeEffect(Unit unit) {
        unit.chanceCritical *= coef;
    }
}
