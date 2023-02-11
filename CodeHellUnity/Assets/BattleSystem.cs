using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	Unit playerUnit;
	Unit enemyUnit;

	int turn;

	public Text dialogueText;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
		turn = 0;
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

	IEnumerator SetupBattle()
	{
		GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<Unit>();

		GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();

		dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	private delegate IEnumerator Action();

	IEnumerator PlayerAttack()
	{
		string text = "";

		int dmg = playerUnit.getDamage();
		if (dmg > playerUnit.damage + playerUnit.damageBonus) {
			text += "CRITICAL STRIKE! ";
		}

		bool isDead = enemyUnit.TakeDamage(dmg);

		enemyHUD.SetHP(enemyUnit.currentHP);

		text += playerUnit.unitName + " attacks!";

		dialogueText.text = text;

		yield return new WaitForSeconds(2f);

		if (isDead) {
			state = BattleState.WON;
			EndBattle();
		} else if (playerUnit.TakeChance()) {
			dialogueText.text = "Strike twice!";
		} else {
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator EnemyTurn()
	{
		string text = "";
		int dmg = enemyUnit.getDamage();
		if (dmg > enemyUnit.damage + enemyUnit.damageBonus) {
			text += "CRITICAL STRIKE! ";
		}

		yield return new WaitForSeconds(1f);

		bool isDead = playerUnit.TakeDamage(dmg);

		playerHUD.SetHP(playerUnit.currentHP);

		text += enemyUnit.unitName + " attacks!";

		dialogueText.text = text;

		yield return new WaitForSeconds(1f);

		if (isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		} else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}

	}

	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogueText.text = "You won the battle!";
		} else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
		}
	}

	void PlayerTurn()
	{
		playerUnit.InitTurn();

		dialogueText.text = "Choose an action:";
	}

	IEnumerator PlayerHeal()
	{
		playerUnit.Heal();

		playerHUD.SetHP(playerUnit.currentHP);
		dialogueText.text = "You feel renewed strength!";

		yield return new WaitForSeconds(2f);

		if (playerUnit.TakeChance()) {
			dialogueText.text = "Strike twice!";
		} else {
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator PlayerDrinkCoffee()
	{
		playerUnit.Drink<CoffeeBuff>();

		// TODO: create HUD
		dialogueText.text = "Now your attacks are powerful!";

		yield return new WaitForSeconds(2f);

		if (playerUnit.TakeChance()) {
			dialogueText.text = "Strike twice!";
		} else {
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator PlayerDrinkRedBull()
	{
		playerUnit.Drink<RedBullBuff>();

		// TODO: create HUD
		dialogueText.text = "You are feeling burst of energy!";

		yield return new WaitForSeconds(2f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

	private void CallWithCheck(Action func)
	{
		if (state != BattleState.PLAYERTURN)
			return;
		StartCoroutine(func());
	}

	public void OnAttackButton()
	{
		CallWithCheck(PlayerAttack);
	}

	public void OnHealButton()
	{
		CallWithCheck(PlayerHeal);
	}

	public void OnDrinkCoffeeButton()
	{
		CallWithCheck(PlayerDrinkCoffee);
	}

	public void OnDrinkRedBullButton()
	{
		CallWithCheck(PlayerDrinkRedBull);
	}
}
