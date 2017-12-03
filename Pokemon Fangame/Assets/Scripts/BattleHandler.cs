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
    private PokemonObject eo;

    private BattleUIHandler buih;
    private DialogueHandler dh;

    public int poSelectedMove;
    public int eoSelectedMove;

    public battlePhase currentPhase;

    public int turnNumber;

    private int atkpoDmg; //Damage of player pokemon
    private int atkeoDmg; //Damage of enemy pokemon

    private float probability;

    private bool success;

    void Start() {
        turnNumber = 1;

        po = playerMon.GetComponent<PokemonObject>();

        eo = enemyMon.GetComponent<PokemonObject>();

        buih = FindObjectOfType<BattleUIHandler>();
        dh = FindObjectOfType<DialogueHandler>();

        po.calculateStats();
        eo.calculateStats();

        Debug.Log("Battle Phase: " + (int)currentPhase);
        Debug.Log("Turn: " + turnNumber);
    }

    void Update() {
        if (currentPhase == battlePhase.ATKEXECUTION) {
            atkpoDmg = CalculateDamage(po, eo, poSelectedMove); 
            atkeoDmg = CalculateDamage(eo, po, eoSelectedMove);
            Debug.Log("Player Damage: " + atkpoDmg);
            Debug.Log("Enemy Damage: " + atkeoDmg);

            StartCoroutine(executeTurn());

            turnNumber++;

            Debug.Log("Turn: " + turnNumber);

            buih.encounterMessage.index = 0;
            currentPhase = battlePhase.START_PHASE;
        }
    }

    //Move execution, atk - attacking pokemon; def - defending pokemon; selectedMove - the chosen move of the attacking pokemon
    public int CalculateDamage(PokemonObject atk, PokemonObject def, int selectedMove) {
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

    private IEnumerator DecreaseHP(int damage, PokemonObject def) {
        int x = (int)def.battleStats[0];
        while (true) {
            def.battleStats[0]--;
            yield return new WaitForSeconds(4 / damage);

            if (def.battleStats[0] == x - damage || def.battleStats[0] <= 0) {
                break;
            }
        }
    }

    private float r; //for status effects & stat changes
    private int n; //for stat change dialogues
    private string n2;
    private IEnumerator ExecuteMove(Move move, int damage, PokemonObject def, PokemonObject atk) {


        //Accuracy
        probability = move.accuracy;
        if (Random.Range(0, 1.01f) <= probability) {
            //Status Effects
            if (atk.currentEffect != null) {
                switch (atk.currentEffect.index) {
                    //PARALYSIS
                    case 1:
                        if (Random.Range(0, 1.01f) < 0.25f) {
                            success = false;
                            Debug.Log(atk.name + " is fully paralyzed!");
                        }

                        break;
                    default:
                        success = true;
                        break;
                }
            } else {
                success = true;
            }

        } else {
            success = false;
            Debug.Log(atk.name + "'s attacked missed!");
        }









        if (success) {
            yield return StartCoroutine(DecreaseHP(damage, def));

            yield return new WaitForSeconds(0.2f);

            //Status Effects
            if (move.effects.Length != 0) {
                foreach (Effect effect in move.effects) {
                    r = Random.Range(0, 1.01f);

                    if (r <= effect.chance) {
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

            //Stat Changes
            if (move.statChanges.Length != 0) {
                foreach (StatChange change in move.statChanges) {
                    r = Random.Range(0, 1.01f);

                    if (r <= change.chance) {

                        switch(change.increment) {
                            case -3:
                                n = 5;
                                break;
                            case -2:
                                n = 4;
                                break;
                            case -1:
                                n = 3;
                                break;
                            case 1:
                                n = 0;
                                break;
                            case 2:
                                n = 1;
                                break;
                            case 3:
                                n = 2;
                                break;
                        }

                        switch(change.statIndex) {
                            case 1:
                                n2 = "attack";
                                break;
                            case 2:
                                n2 = "defense";
                                break;
                            case 3:
                                n2 = "special attack";
                                break;
                            case 4:
                                n2 = "special defense";
                                break;
                            case 5:
                                n2 = "speed";
                                break;
                        }


                        dh.StartDialogue(buih.statChange[n], true, new string[] { def.species.pokemonName, n2 });
                        def.statBattleModif[change.statIndex] += change.increment;
                        def.recalculateBattleStat(change.statIndex);
                        
                    }
                }
            }
        } else {

        } 
    }

    //The cycle of each turn in the battle
    private IEnumerator executeTurn() {
        //If the player is faster than the enemy
        if (po.battleStats[5] > eo.battleStats[5]) {
            dh.StartDialogue(buih.attackMessage, true, new string[] { po.species.name, po.moves[poSelectedMove].name });
            yield return StartCoroutine(ExecuteMove(po.moves[poSelectedMove], atkpoDmg, eo, po));

            yield return new WaitForSeconds(0.3f);

            dh.StartDialogue(buih.attackMessage, true, new string[] { eo.species.name, eo.moves[eoSelectedMove].name });
            yield return StartCoroutine(ExecuteMove(eo.moves[eoSelectedMove], atkeoDmg, po, eo));

            yield return new WaitForSeconds(0.3f);

            //If the enemy is faster than the player
        } else if (po.battleStats[5] < eo.battleStats[5]) {
            dh.StartDialogue(buih.attackMessage, true, new string[] { eo.species.name, eo.moves[eoSelectedMove].name });
            yield return StartCoroutine(ExecuteMove(eo.moves[eoSelectedMove], atkeoDmg, po, eo));

            yield return new WaitForSeconds(0.5f);

            dh.StartDialogue(buih.attackMessage, true, new string[] { po.species.name, po.moves[poSelectedMove].name });
            yield return StartCoroutine(ExecuteMove(po.moves[poSelectedMove], atkpoDmg, eo, po));

            yield return new WaitForSeconds(0.3f);

            
            //If both the enemy and player have equal speed
        } else if (po.battleStats[5] == eo.battleStats[5]) {
            //50% of either pokemon going first
            if (Random.Range(1, 11) > 5) {
                dh.StartDialogue(buih.attackMessage, true, new string[] { po.species.name, po.moves[poSelectedMove].name });
                yield return StartCoroutine(ExecuteMove(po.moves[poSelectedMove], atkpoDmg, eo, po));

                yield return new WaitForSeconds(0.3f);

                dh.StartDialogue(buih.attackMessage, true, new string[] { eo.species.name, eo.moves[eoSelectedMove].name });
                yield return StartCoroutine(ExecuteMove(eo.moves[eoSelectedMove], atkeoDmg, po, eo));

                yield return new WaitForSeconds(0.3f);

                
            } else {
                dh.StartDialogue(buih.attackMessage, true, new string[] { eo.species.name, eo.moves[eoSelectedMove].name });
                yield return StartCoroutine(ExecuteMove(eo.moves[eoSelectedMove], atkeoDmg, po, eo));

                yield return new WaitForSeconds(0.3f);

                dh.StartDialogue(buih.attackMessage, true, new string[] { po.species.name, po.moves[poSelectedMove].name });
                yield return StartCoroutine(ExecuteMove(po.moves[poSelectedMove], atkpoDmg, eo, po));

                yield return new WaitForSeconds(0.3f);

                
            }
        }
    }
}

public enum battlePhase {
    START_PHASE,
    PLAYER_ATKSELECTION,
    ENEMY_ATKSELECTION,
    ATKEXECUTION
};