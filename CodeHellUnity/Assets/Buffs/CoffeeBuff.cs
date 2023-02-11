public class CoffeeBuff : BuffBase
{
    public CoffeeBuff() {
        duration = 3;
    }

    public override void makeEffect(Unit unit) {
        unit.damageBonus += 100;
    }
}
