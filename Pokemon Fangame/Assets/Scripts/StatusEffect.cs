using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The actual volatile and non-volatile effects that may be given to a pokemon
 */
[CreateAssetMenu(fileName = "Status Effect", menuName = "Status Effect", order = 6)]
[System.Serializable]
public class StatusEffect : ScriptableObject {
    public string effectName = "Status Effect Name";
    public int index = 0;
    public Sprite icon;

    public effectType type = effectType.NON_VOLATILE;
}
    /* INDEXES
     * _NON_VOLATILE_
     * 00 - BURN
     * 01 - PARALYSIS
     * 02 - POISON
     * 03 - FREEZE
     * 04 - SLEEP
     */
     
public enum effectType {
    NON_VOLATILE, //These do not wear off after battle
    VOLATILE, //These wear off by switching
    VOLATILE_BATTLE //These wear off at the end of the battle
}