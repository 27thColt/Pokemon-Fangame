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
    public float[] statCalc = new float[6] { 0, 0, 0, 0, 0, 0 }; //Permanent values that exist outside of battle


    //Changing Values
    public float[] battleStats = new float[6] { 0, 0, 0, 0, 0, 0 }; //Used in-battle and changed temporarily
    public int[] statBattleModif = new int[6] { 0, 0, 0, 0, 0, 0 };
    public int level = 1;
    public StatusEffect currentEffect;

    public Move[] moves = new Move[4];

    public int[] currentPP = new int[4];

    public void calculateStats() {
        for (int i = 0; i < statCalc.Length; i++) {
            if (i == 0) {
                //HP has a different stat calculation
                statCalc[i] = Mathf.RoundToInt((((2 * species.statsBase[i] + statsIV[i] + (statsEV[i] / 4)) * level) / 100) + level + 10);
                battleStats[i] = (int)statCalc[i];
            } else {
                statCalc[i] = Mathf.RoundToInt(((((2 * species.statsBase[i] + statsIV[i] + (statsEV[i] / 4)) * level) / 100) + 5) * nat.statModifiers[i]);
                battleStats[i] = (int)statCalc[i];
            }
        }
    }

    public void recalculateBattleStat(int stat) {
        float x = 0;

        if (statBattleModif[stat] >= 0) {
            x = (2 + (float)statBattleModif[stat]) / 2;
        } else if (statBattleModif[stat] < 0) {
            x = 2 / (2 + Mathf.Abs((float)statBattleModif[stat]));
        }

        battleStats[stat] = Mathf.RoundToInt(((((2 * species.statsBase[stat] + statsIV[stat] + (statsEV[stat] / 4)) * level) / 100) + 5) * nat.statModifiers[stat]);
        battleStats[stat] *= x;
        Mathf.RoundToInt(battleStats[stat]);
    }
}