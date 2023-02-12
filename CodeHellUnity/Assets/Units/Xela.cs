using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xela : Unit
{
    public int specialDamage;

    public int LowBarrierSpecial;

    public int HighBarrierSpecial;

    public int timeForAttack;

    private void nextSpecialInit() {
        timeForAttack = Roll.roll(LowBarrierSpecial, HighBarrierSpecial);
    }

    void Start() {
        nextSpecialInit();
    }

    public override void Attack(GameObject thisObject, Unit target, ref string text) {
        // TODO: add animations
        int dmg = damage;
        if (timeForAttack == 0) {
            nextSpecialInit();
            dmg = getDamage(specialDamage, text);
            thisObject.GetComponent<Animator>().SetTrigger("onAttack");
            text += "Xela strikes with fire!";
        } else {
            --timeForAttack;
            dmg = getDamage(damage, text);
            thisObject.GetComponent<Animator>().SetTrigger("onAttack");
            text += "Xela hits with his claws!";
        }
        target.TakeDamage(dmg);
    }
}
