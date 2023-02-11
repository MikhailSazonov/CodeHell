public class RedBullBuff : BuffBase
{
    public int bonusChance = 50;

    public RedBullBuff() {
        duration = 4;
    }

    public override void makeEffect(Unit unit) {
        unit.chanceRepeat += bonusChance;
    }
}
