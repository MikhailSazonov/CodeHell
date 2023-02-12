using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG : Unit
{
	public int heal;

	List<BuffBase> buffs;

	public void StartBattle() {
    	buffs = new List<BuffBase>();
		buffs.Add(GameObject.FindWithTag("coffee").GetComponent(typeof(CoffeeBuff)) as CoffeeBuff);
		buffs.Add(GameObject.FindWithTag("redbull").GetComponent(typeof(RedBullBuff)) as RedBullBuff);
		currentHP = maxHP;
	}

	private void useBuffs() {
		foreach (var _drink in buffs) {
			_drink.Drink(this);
		}
	}

	public override void InitTurn()
	{
		percentDamageTaken = 100;
		chanceRepeat = 0;
		useBuffs();
	}

	public bool TakeChance()
	{
		if (Roll.luckyMe(chanceRepeat)) {
			chanceRepeat = 0;
			return true;
		}
		return false;
	}

    public override void Attack(GameObject thisObject, Unit target, ref string text) {
        int dmg = getDamage(damage, text);
        text += unitName + " attacks.";
        thisObject.GetComponent<Animator>().SetTrigger("onAttack");
        target.TakeDamage(dmg);
    }

	public void Heal(ref string text)
	{
		currentHP += heal;
		if (currentHP > maxHP)
			currentHP = maxHP;
        text += "You feel renewed strength!";
        Debug.Log(text);
	}

	public void Sleep(ref string text)
	{
		percentDamageTaken = 20;
        text += "You take a rest...";
	}

	public void Drink(ref string text)
	{
		foreach (var drink in buffs) {
			if (drink.buffName == text) {
                if (text == "coffee") {
                    text = "Your strikes become more accurate!";
                }
                if (text == "energy") {
                    text = "You are feeling burst of energy!";
                }
				drink.fill();
                break;
			}
		}
	}

}
