using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit : MonoBehaviour
{

	public string unitName;
	public int unitLevel;

	public int damage;

	public int maxHP;
	public int currentHP;

	public int heal;

	public int damageBonus;
	public int accuracyBonus;
	
	public int chanceRepeat;
	public int chanceCritical;

	List<BuffBase> buffs;

	public Unit() {
    	buffs = new List<BuffBase>();
		currentHP = maxHP;
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
		chanceRepeat = 0;
		damageBonus = 0;
		useBuffs();
	}

	public int getDamage()
	{
		if (Roll.luckyMe(chanceCritical)) {
			return 2 * (damage + damageBonus);
		}
		return damage + damageBonus; 
	}

	public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;
		if (currentHP <= 0)
			return true;
		else
			return false;
	}

	public bool TakeChance()
	{
		if (Roll.luckyMe(chanceRepeat)) {
			chanceRepeat = 0;
			return true;
		}
		return false;
	}

	public void Heal()
	{
		currentHP += heal;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

	public void Drink<T>() where T : BuffBase, new()
	{
		buffs.Add(new T());
	}
}
