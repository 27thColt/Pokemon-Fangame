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

    void Start() {
        Debug.Log("Battle Phase: " + phase);

        po = playerMon.GetComponent<PokemonObject>();
        ph = playerMon.GetComponent<PlayermonHandler>();

        eo = enemyMon.GetComponent<PokemonObject>();



        Debug.Log("Battle Phase: " + phase);
    }

    void Update() {
        if (phase == 3) {
            StartCoroutine(battleSequence());

        }
    }

    //Move execution, atk - attacking pokemon; def - defending pokemon; selectedMove - the chosen move of the attacking pokemon
    public int calculateDamage(PokemonObject atk, PokemonObject def, int selectedMove) {
        int damage; //total damage done
        float dmgModifier = 1; //damage modifier (type effectiveness, STAB, Item modifiers, etc.)

        /*
         * Calculation for Damage Modifier 
         */

        //Type Effectiveness
        if (def.species.type2 == null) {
            //if defending pokemon has one type
            dmgModifier *= atk.moves[selectedMove].type.offensives[def.species.type1.index];

        } else {
            //if defending pokemon has two types
            dmgModifier *= atk.moves[selectedMove].type.offensives[def.species.type1.index] * atk.moves[selectedMove].type.offensives[def.species.type2.index];
        }

        //STAB
        if (atk.species.type2 != null) {
            //if attacking pokemon has one type
            if (atk.species.type1 == atk.moves[selectedMove].type)
                dmgModifier *= 1.5f;
        } else {
            //if attacking pokemon has two types
            if (atk.species.type1 == atk.moves[selectedMove].type || atk.species.type2 == atk.moves[selectedMove])
                dmgModifier *= 1.5f;
        }

        Debug.Log(dmgModifier);

        /*
         * Calculation for Damage 
         */
        if (atk.moves[selectedMove].physical == true) {
            Debug.Log("Damage = ((((((2 * " + atk.level + ") / 5) + 2)" + " * " + atk.moves[selectedMove].damage + " * (" + atk.statCalc[1] + " / " + def.statCalc[2] + ")) / 50) + 2) * " + dmgModifier);
            damage = Mathf.RoundToInt(((((((2 * atk.level) / 5) + 2) * atk.moves[selectedMove].damage * (atk.statCalc[1] / def.statCalc[2])) / 50) + 2) * dmgModifier);
            Debug.Log(damage);
            return (int)damage;
        } else if (atk.moves[selectedMove].special == true) {
            Debug.Log("Damage = ((((((2 * " + atk.level + ") / 5) + 2)" + " * " + atk.moves[selectedMove].damage + " * (" + atk.statCalc[3] + " / " + def.statCalc[4] + ")) / 50) + 2) * " + dmgModifier);
            damage = Mathf.RoundToInt(((((((2 * atk.level) / 5) + 2) * atk.moves[selectedMove].damage * (atk.statCalc[3] / def.statCalc[4])) / 50) + 2) * dmgModifier);
            Debug.Log(damage);
            return (int)damage;
        } else {
            return 0;
        }


    }

    private IEnumerator decreaseHP(int damage, PokemonObject atk, PokemonObject def) {
        int x = 0;
        while (x < damage && atk.battleHP > 0) {
            x++;
            def.battleHP--;
            yield return new WaitForSeconds(5 / damage);
            if (def.battleHP <= 0) {
                break;
            }
        }
    }

    private IEnumerator battleSequence() {
        int atk1Dmg; //Damage of the first pokemon
        int atk2Dmg; //Damage of the second pokemon

        //If the player is faster than the enemy
        if (po.statCalc[5] > eo.statCalc[5]) {
            atk1Dmg = calculateDamage(po, eo, poSelectedMove);
            atk2Dmg = calculateDamage(eo, po, eoSelectedMove);

            yield return StartCoroutine(decreaseHP(atk1Dmg, po, eo));
            yield return new WaitForSeconds(1);
            yield return StartCoroutine(decreaseHP(atk2Dmg, eo, po));

            //phase = 1;
            //If the enemy is faster than the player
        } else if (eo.statCalc[5] > po.statCalc[5]) {
            atk1Dmg = calculateDamage(eo, po, eoSelectedMove);
            atk2Dmg = calculateDamage(po, eo, poSelectedMove);

            yield return StartCoroutine(decreaseHP(atk2Dmg, eo, po));
            yield return new WaitForSeconds(1);
            yield return StartCoroutine(decreaseHP(atk1Dmg, po, eo));

            //phase = 1;
            //If both pokemon have equal speed, 50% chance of either going first
        } else if (eo.statCalc[5] == po.statCalc[5]) {
            if (Random.Range(1, 11) > 5) {
                atk1Dmg = calculateDamage(eo, po, eoSelectedMove);
                atk2Dmg = calculateDamage(po, eo, poSelectedMove);

                yield return StartCoroutine(decreaseHP(atk2Dmg, eo, po));
                yield return new WaitForSeconds(1);
                yield return StartCoroutine(decreaseHP(atk1Dmg, po, eo));

                phase = 1;
            } else {
                atk1Dmg = calculateDamage(po, eo, poSelectedMove);
                atk2Dmg = calculateDamage(eo, po, eoSelectedMove);

                yield return StartCoroutine(decreaseHP(atk1Dmg, po, eo));
                yield return new WaitForSeconds(1);
                yield return StartCoroutine(decreaseHP(atk2Dmg, eo, po));

                //phase = 1;
            }
        } else {
            yield return 0;
            //phase = 1;
        }
    }
}