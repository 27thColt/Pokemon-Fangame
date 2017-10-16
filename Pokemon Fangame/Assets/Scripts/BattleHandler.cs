using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour {

    public GameObject playerMon;
    public GameObject enemyMon;

    private PokemonObject po;
    private PlayermonHandler ph;

    private PokemonObject eo;

    public int poSelectedMove;
    public int eoSelectedMove;

    public int phase = 1;
    // 1 - Attack Selection, 2 - Enemy Attack Selection, 3 - Attack Execution

	void Start () {
        Debug.Log("Battle Phase: " + phase);

        po = playerMon.GetComponent<PokemonObject>();
        ph = playerMon.GetComponent<PlayermonHandler>();

        eo = enemyMon.GetComponent<PokemonObject>();

	}
	
	void Update () {
        if (phase == 3) {
            po_executeMove();
        }
	}

    public void po_executeMove() {
        int damage;

        if (po.moves[poSelectedMove].physical == true) {
            damage = (((2 * po.level) / 5) * po.moves[poSelectedMove].damage * (po.statCalc[1] / eo.statCalc[2]) / 50); //* po.moves[poSelectedMove].type;
            StartCoroutine(decreaseHP(damage));

        } else if (po.moves[poSelectedMove].special == true) {
            damage = (((2 * po.level) / 5) * po.moves[poSelectedMove].damage * (po.statCalc[3] / eo.statCalc[4]) / 50); //* po.moves[poSelectedMove].type;
            StartCoroutine(decreaseHP(damage));
        }
    }

    public void eo_executeMove() {
        int damage;

        if (eo.moves[eoSelectedMove].physical == true) {
            damage = (((2 * eo.level) / 5) * eo.moves[poSelectedMove].damage * (eo.statCalc[1] / po.statCalc[2]) / 50); //* eo.moves[eoSelectedMove].type;
            StartCoroutine(decreaseHP(damage));

        } else if (eo.moves[eoSelectedMove].special == true) {
            damage = (((2 * eo.level) / 5) * eo.moves[poSelectedMove].damage * (eo.statCalc[3] / po.statCalc[4]) / 50); //* eo.moves[eoSelectedMove].type;
            StartCoroutine(decreaseHP(damage));
        }
    }

    private IEnumerator decreaseHP(int damage) {
        int x = 0;
        while (x < damage && po.battleHP != 0) {
            x++;
            eo.battleHP--;
            yield return new WaitForSeconds(3 / damage);
            if (eo.battleHP == 0) {
                break;
            }
        }
    }
}