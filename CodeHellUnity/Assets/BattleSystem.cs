using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState { START, PLAYERTURN = 1, ENEMYTURN = 2, WON, LOST }

public class BattleSystem : MonoBehaviour
{
	public GameObject playerPrefab;
	public GameObject enemyPrefab;
	public GameObject button;

	public GameObject playerObj;
	public GameObject enemyObj;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	GG playerUnit;
	Unit enemyUnit;

	int turn;

	public Text dialogueText;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	private delegate void Action(ref string text);

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
		playerObj = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerObj.GetComponent<GG>();

		enemyObj = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyObj.GetComponent<Unit>();

		dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);
		playerUnit.StartBattle();

		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	private IEnumerator ChangeTurn(string text) {
		state = (BattleState)(3 - (int)state);

		playerHUD.SetHP(playerUnit.currentHP);

		enemyHUD.SetHP(enemyUnit.currentHP);

		if (enemyUnit.isDead()) {
			state = BattleState.WON;
			EndBattle();
			yield return new WaitForSeconds(1f);
		} else if (playerUnit.isDead()) {
			state = BattleState.LOST;
			EndBattle();
			yield return new WaitForSeconds(1f);
		} else {
			dialogueText.text = text;

			yield return new WaitForSeconds(2f);

			if (state == BattleState.ENEMYTURN) {
				if (playerUnit.twice) {
					dialogueText.text = "You can strike twice!";
					state = BattleState.PLAYERTURN;
					yield return new WaitForSeconds(2f);
					playerUnit.twice = false;
					PlayerTurn();
				} else {
					EnemyTurn();
				}
			} else {
				PlayerTurn();
			}
		}
	}

	void PlayerAttack(ref string text)
	{
		playerUnit.Attack(playerObj, enemyUnit, ref text);
	}

	void EnemyAttack(ref string text)
	{	
		Debug.Log("EnemyAttack");

		enemyUnit.Attack(enemyObj, playerUnit, ref text);

		StartCoroutine(ChangeTurn(text));
	}

	void EndBattle()
	{
		if (state == BattleState.WON)
		{
			dialogueText.text = "You won the battle!";
		} else if (state == BattleState.LOST)
		{
			playerObj.GetComponent<Animator>().SetTrigger("onDie");
			dialogueText.text = "You were defeated.";
		}
	}

	void PlayerTurn()
	{
		playerUnit.InitTurn();

		dialogueText.text = "Choose an action:";
	}

	void EnemyTurn()
	{
		enemyUnit.InitTurn();

		string text = "";

		EnemyAttack(ref text);
	}

	private void CallWithCheck(ref string text, Action func)
	{
		if (state != BattleState.PLAYERTURN)
			return;
		func(ref text);
		StartCoroutine(ChangeTurn(text));
	}

	public void OnAttackButton()
	{
		string text = "";
		CallWithCheck(ref text, PlayerAttack);
	}

	public void OnHealButton()
	{
		string text = "";
		CallWithCheck(ref text, playerUnit.Heal);
	}

	public void OnDrinkCoffeeButton()
	{
		string text = "coffee";
		CallWithCheck(ref text, playerUnit.Drink);
	}

	public void OnDrinkRedBullButton()
	{
		string text = "energy";
		CallWithCheck(ref text, playerUnit.Drink);
	}

	public void OnSleepButton()
	{
		string text = "";
		CallWithCheck(ref text, playerUnit.Sleep);
	}
}
