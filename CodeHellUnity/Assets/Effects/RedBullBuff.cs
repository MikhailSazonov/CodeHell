public class RedBullBuff : BuffBase
{
    public int bonusChance = 50;

    public RedBullBuff() {
        totalDuration = 4;
        buffName = "energy";
    }

    public override void makeEffect(Unit unit) {
        unit.chanceRepeat += bonusChance;
    }
}
