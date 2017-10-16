using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Deals with the specific instance of a pokemon.
 */
public class PokemonObject : MonoBehaviour {
    public PokemonSpecies species;

    public Nature nat;

    //HP - Atk - Def - Spa - Spd - Spe
    public int[] statsEV = { 0, 0, 0, 0, 0, 0 };
    public int[] statsIV = { 0, 0, 0, 0, 0, 0 };
    public int[] statCalc = { 0, 0, 0, 0, 0, 0 };


    public int level = 1;
    public int battleHP;

    public Move[] moves = new Move[4];

    void Start () {
        calculateStats();
        Debug.Log(statCalc[0]);
    }

    void Update() {

    }

    public void calculateStats() {
        for (int i = 0; i < statCalc.Length; i++) {
            if (i == 0) {
                //HP has a different stat calculation
                statCalc[i] = (((2 * species.statsBase[i] + statsIV[i] + (statsEV[i] / 4)) * level) / 100) + level + 10;
                battleHP = statCalc[i];
            } else {
                statCalc[i] = Mathf.RoundToInt(((((2 * species.statsBase[i] + statsIV[i] + (statsEV[i] / 4)) * level) / 100) + 5) * nat.statModifiers[i]);
            }
        }
    }
}