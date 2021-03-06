﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The possible effects as a result of moves or abilities.
 */
 [System.Serializable]
public class Effect {
    public float chance;
    public StatusEffect givenEffect;
}

/*
 * The possible stat change as a result of moves or abilities
 */
[System.Serializable]
public class StatChange {
    public float chance;
    public int statIndex;
    public int increment;
}
