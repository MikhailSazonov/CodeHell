using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class BuffBase : MonoBehaviour
{
    public int totalDuration;

    private int left;

    public string buffName;

    public abstract void makeEffect(Unit unit);

    public void Drink(Unit unit) {
        if (left > 0) {
            makeEffect(unit);
            reduceDuration();
        }
    }

    public void fill() {
        left = totalDuration;
        GetComponent<Image>().sprite = Resources.Load<Sprite>(buffName + (left).ToString());
    }

    private void reduceDuration() {
        left = Math.Max(0, left-1);
        Debug.Log(buffName + (left).ToString());
        GetComponent<Image>().sprite = Resources.Load<Sprite>(buffName + (left).ToString());
    }
}
