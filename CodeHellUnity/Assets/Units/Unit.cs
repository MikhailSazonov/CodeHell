using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

abstract public class Unit : MonoBehaviour
{
	public string unitName;
	public int unitLevel;

	public int damage;

	public int maxHP;
	public int currentHP;
	
	public int chanceCritical;
	public int chanceRepeat;

	public int percentDamageTaken;

	public bool isDead() {
		return currentHP <= 0;
	}

	protected int getDamage(int dmg, string text) {
		if (Roll.luckyMe(chanceCritical)) {
			dmg *= 2;
			text += "CRITICAL STRIKE! ";
		}
		return dmg;
	}

	public virtual void InitTurn() {}

	public abstract void Attack(GameObject thisObject, Unit target, ref string text);

	public void TakeDamage(int dmg)
	{
		currentHP -= dmg * percentDamageTaken / 100;
		Debug.Log((dmg * percentDamageTaken / 100).ToString() + " damage taken.");
	}
}
