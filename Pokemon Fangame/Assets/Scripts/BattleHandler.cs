using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour {

    public GameObject playerMon;
    public GameObject enemyMon;

    private PokemonObject po;
    //private PlayermonHandler ph;

    private PokemonObject eo;

    public int poSelectedMove;
    public int eoSelectedMove;

    public enum battlePhase {
        PLAYER_ATKSELECTION,
        ENEMY_ATKSELECTION,
        ATKEXECUTION
    };

    public battlePhase currentPhase;

    int atkpoDmg; //Damage of player pokemon
    int atkeoDmg; //Damage of enemy pokemon

    float atkExTimer = 5;

    void Start() {
        Debug.Log("Battle Phase: " + (int)currentPhase);

        po = playerMon.GetComponent<PokemonObject>();
        //ph = playerMon.GetComponent<PlayermonHandler>();

        eo = enemyMon.GetComponent<PokemonObject>();



        Debug.Log("Battle Phase: " + (int)currentPhase);
    }

    void Update() {
        if (currentPhase == battlePhase.ATKEXECUTION) {
            atkpoDmg = calculateDamage(po, eo, poSelectedMove); 
            atkeoDmg = calculateDamage(eo, po, eoSelectedMove);
            Debug.Log("Player Damage: " + atkpoDmg);
            Debug.Log("Enemy Damage: " + atkeoDmg);

            StartCoroutine(battleSequence());
            currentPhase = battlePhase.PLAYER_ATKSELECTION;
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

        /*
         * Calculation for Damage 
         */
        if (atk.moves[selectedMove].physical == true) {
            Debug.Log("Damage = ((((((2 * " + atk.level + ") / 5) + 2)" + " * " + atk.moves[selectedMove].damage + " * (" + atk.statCalc[1] + " / " + def.statCalc[2] + ")) / 50) + 2) * " + dmgModifier);
            damage = Mathf.RoundToInt(((((((2 * atk.level) / 5) + 2) * atk.moves[selectedMove].damage * (atk.statCalc[1] / def.statCalc[2])) / 50) + 2) * dmgModifier);
            return (int)damage;
        } else if (atk.moves[selectedMove].special == true) {
            Debug.Log("Damage = ((((((2 * " + atk.level + ") / 5) + 2)" + " * " + atk.moves[selectedMove].damage + " * (" + atk.statCalc[3] + " / " + def.statCalc[4] + ")) / 50) + 2) * " + dmgModifier);
            damage = Mathf.RoundToInt(((((((2 * atk.level) / 5) + 2) * atk.moves[selectedMove].damage * (atk.statCalc[3] / def.statCalc[4])) / 50) + 2) * dmgModifier);
            return (int)damage;
        } else {
            return 0;
        }


    }

    private IEnumerator decreaseHP(int damage, PokemonObject def) {
        int x = def.currentHP;
        while (true) {
            def.currentHP--;
            yield return new WaitForSeconds(4 / damage);


            if (def.currentHP == x - damage || def.currentHP <= 0) {
                break;
            }
        }
        
    }

    private IEnumerator battleSequence() {
        //If the player is faster than the enemy
        if (po.statCalc[5] > eo.statCalc[5]) {
            yield return StartCoroutine(decreaseHP(atkpoDmg, eo));
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(decreaseHP(atkeoDmg, po));

            //If the enemy is faster than the player
        } else if (po.statCalc[5] < eo.statCalc[5]) {
            yield return StartCoroutine(decreaseHP(atkeoDmg, po));
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(decreaseHP(atkpoDmg, eo));

            //If both the enemy and player have equal speed
        } else if (po.statCalc[5] == eo.statCalc[5]) {
            //50% of either pokemon going first
            if (Random.Range(1, 11) > 5) {
                yield return StartCoroutine(decreaseHP(atkpoDmg, eo));
                yield return new WaitForSeconds(0.5f);
                yield return StartCoroutine(decreaseHP(atkeoDmg, po));
            } else {
                yield return StartCoroutine(decreaseHP(atkeoDmg, po));
                yield return new WaitForSeconds(0.5f);
                yield return StartCoroutine(decreaseHP(atkpoDmg, eo));
            }
        }
    }
}