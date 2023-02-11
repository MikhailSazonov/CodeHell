using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

	public string unitName;
	public int unitLevel;

	public int damage;

	public int maxHP;
	public int currentHP;

	public int heal;

	public int damageBonus;

	List<BuffBase> buffs;

	public Unit() {
    	buffs = new List<BuffBase>();
	}

	private void useBuffs() {
		List<int> remove = new List<int>();
		for (int i = 0; i < buffs.Count; ++i) {
			buffs[i].makeEffect(this);
			if (buffs[i].reduceDuration() == 0) {
				remove.Add(i);
			}
		}
		foreach (int i in remove) {
			buffs.RemoveAt(i);
		}
	}

	public void InitTurn()
	{
		damageBonus = 0;
		useBuffs();
	}

	public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}

	public void Heal()
	{
		currentHP += heal;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

	public void DrinkCoffee()
	{
		buffs.Add(new CoffeeBuff());
	}

}
