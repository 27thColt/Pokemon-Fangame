using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Type", menuName = "Pokemon Type", order = 4)]
[System.Serializable]
public class Type : ScriptableObject {
    public string typeName = "New Type";
    public int index = 0;

    // Refers to the index
    public float[] offensives = new float[18] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
    public float[] defensives = new float[18] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};


    //for the editor
    public bool[] offensiveSE = new bool[18];
    public bool[] offensiveNVE = new bool[18];
    public bool[] offensiveNE = new bool[18];
}

    /* INDEXES
     * 00 - NORMAL
     * 01 - GRASS
     * 02 - FIRE
     * 03 - WATER
     * 04 - ELECTRIC
     * 05 - ICE
     * 06 - BUG
     * 07 - POISON
     * 08 - FLYING
     * 09 - FIGHTING
     * 10 - GROUND 
     * 11 - ROCK
     * 12 - STEEL
     * 13 - PSYCHIC
     * 14 - DARK
     * 15 - GHOST
     * 16 - FAIRY 
     * 17 - DRAGON
     */