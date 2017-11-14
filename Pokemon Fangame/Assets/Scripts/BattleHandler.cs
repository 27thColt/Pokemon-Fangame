using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour {

    /* Battle Handler
     * ===================================
     * Coordinates with PlayermonHandler & EnemymonHandler to make up the battle
     * ===================================
     * THINGS TO DO
     * ===================================
     * - FINISH VOLATILE EFFECTS
     *  - PARALYSIS
     *  - BURN
     *  - FREEZE
     *  - POISON
     *  - SLEEP
     * - TEMPORARY STAT CHANGES
     * 
     */

    public GameObject playerMon;
    public GameObject enemyMon;

    private PokemonObject po;
    //private PlayermonHandler ph;

    private PokemonObject eo; 

    public int poSelectedMove;
    public int eoSelectedMove;

    public battlePhase currentPhase;

    int atkpoDmg; //Damage of player pokemon
    int atkeoDmg; //Damage of enemy pokemon


    
    void Start() {
        Debug.Log("Battle Phase: " + (int)currentPhase);

        po = playerMon.GetComponent<PokemonObject>();
        //ph = playerMon.GetComponent<PlayermonHandler>();

        eo = enemyMon.GetComponent<PokemonObject>();


        po.calculateStats();
        eo.calculateStats();

        Debug.Log("Battle Phase: " + (int)currentPhase);
    }

    void Update() {
        if (currentPhase == battlePhase.ATKEXECUTION) {
            atkpoDmg = calculateDamage(po, eo, poSelectedMove); 
            atkeoDmg = calculateDamage(eo, po, eoSelectedMove);
            Debug.Log("Player Damage: " + atkpoDmg);
            Debug.Log("Enemy Damage: " + atkeoDmg);

            StartCoroutine(executeTurn());

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
        if (atk.moves[selectedMove].hitType == moveType.PHYSICAL) {
            Debug.Log("Damage = ((((((2 * " + atk.level + ") / 5) + 2)" + " * " + atk.moves[selectedMove].damage + " * (" + atk.battleStats[1] + " / " + def.battleStats[2] + ")) / 50) + 2) * " + dmgModifier);
            damage = Mathf.RoundToInt(((((((2 * atk.level) / 5) + 2) * atk.moves[selectedMove].damage * (atk.battleStats[1] / def.battleStats[2])) / 50) + 2) * dmgModifier);
            return (int)damage;
        } else if (atk.moves[selectedMove].hitType == moveType.SPECIAL) {
            Debug.Log("Damage = ((((((2 * " + atk.level + ") / 5) + 2)" + " * " + atk.moves[selectedMove].damage + " * (" + atk.statCalc[3] + " / " + def.statCalc[4] + ")) / 50) + 2) * " + dmgModifier);
            damage = Mathf.RoundToInt(((((((2 * atk.level) / 5) + 2) * atk.moves[selectedMove].damage * (atk.battleStats[3] / def.battleStats[4])) / 50) + 2) * dmgModifier);
            return (int)damage;
        } else {
            return 0;
        }


    }

    private IEnumerator decreaseHP(int damage, PokemonObject def) {
        int x = (int)def.battleStats[0];
        while (true) {
            def.battleStats[0]--;
            yield return new WaitForSeconds(4 / damage);

            if (def.battleStats[0] == x - damage || def.battleStats[0] <= 0) {
                break;
            }
        }
    }

    public float r;
    private IEnumerator executeMove(Move move, int damage, PokemonObject def) {
        yield return StartCoroutine(decreaseHP(damage, def));
    
        yield return new WaitForSeconds(0.2f);
        
        if (move.effects.Length != 0) {
            foreach (Effect effect in move.effects) {
                r = Random.Range(0, 10);
                Debug.Log(r);
                if (r < effect.chance) {
                    def.currentEffect = effect.givenEffect;

                    switch (def.currentEffect.index) {
                        case 0:
                            break;

                        //PARALYSIS
                        case 1:
                            Mathf.RoundToInt(def.battleStats[5] = def.battleStats[5] / 2);
                            break;
                    }
                }
            }
        }
    }

    //The cycle of each turn in the battle
    private IEnumerator executeTurn() {
        //If the player is faster than the enemy
        if (po.battleStats[5] > eo.battleStats[5]) {
            yield return StartCoroutine(executeMove(po.moves[poSelectedMove], atkpoDmg, eo));
            yield return new WaitForSeconds(0.3f);
            yield return StartCoroutine(executeMove(eo.moves[eoSelectedMove], atkeoDmg, po));

            yield return new WaitForSeconds(0.3f);

            //If the enemy is faster than the player
        } else if (po.battleStats[5] < eo.battleStats[5]) {
            yield return StartCoroutine(executeMove(eo.moves[eoSelectedMove], atkeoDmg, po));
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(executeMove(po.moves[poSelectedMove], atkpoDmg, eo));

            yield return new WaitForSeconds(0.3f);

            
            //If both the enemy and player have equal speed
        } else if (po.battleStats[5] == eo.battleStats[5]) {
            //50% of either pokemon going first
            if (Random.Range(1, 11) > 5) {
                yield return StartCoroutine(executeMove(po.moves[poSelectedMove], atkpoDmg, eo));
                yield return new WaitForSeconds(0.3f);
                yield return StartCoroutine(executeMove(eo.moves[eoSelectedMove], atkeoDmg, po));

                yield return new WaitForSeconds(0.3f);

                
            } else {
                yield return StartCoroutine(executeMove(eo.moves[eoSelectedMove], atkeoDmg, po));
                yield return new WaitForSeconds(0.3f);
                yield return StartCoroutine(executeMove(po.moves[poSelectedMove], atkpoDmg, eo));

                yield return new WaitForSeconds(0.3f);

                
            }
        }
    }
}

public enum battlePhase {
    PLAYER_ATKSELECTION,
    ENEMY_ATKSELECTION,
    ATKEXECUTION
};