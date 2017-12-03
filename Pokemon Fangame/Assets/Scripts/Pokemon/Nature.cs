using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nature", menuName = "Pokemon Nature", order = 5)]
[System.Serializable]
public class Nature : ScriptableObject {
    public string natureName = "Nature Name";
    public int index = 0;

    public float[] statModifiers = new float[6] { 1, 1, 1, 1, 1, 1 };

    //For editor
    public bool atkb;
    public bool atkn;

    public bool defb;
    public bool defn;

    public bool spab;
    public bool span;

    public bool spdb;
    public bool spdn;

    public bool speb;
    public bool spen;
}
    /* INDEXES
     * 00 - ADAMANT
     * 01 - BASHFUL
     * 02 - BOLD
     * 03 - BRAVE
     * 04 - CALM
     * 05 - CAREFUL
     * 06 - DOCILE
     * 07 - GENTLY
     * 08 - HARDY
     * 09 - HASTY
     * 10 - IMPISH 
     * 11 - JOLLY
     * 12 - LAX
     * 13 - LONELY
     * 14 - MILD
     * 15 - MODEST
     * 16 - NAIVE 
     * 17 - NAUGHTY
     * 18 - QUIET
     * 19 - QUIRKY
     * 20 - RASH
     * 21 - RELAXED
     * 22 - SASSY
     * 23 - SERIOUS
     * 24 - TIMID
     */
