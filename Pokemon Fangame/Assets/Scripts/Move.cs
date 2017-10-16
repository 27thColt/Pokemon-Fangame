using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Pokemon Move", order = 3)]
[System.Serializable]
public class Move : ScriptableObject {
    public string moveName = "Move Name";
    public int index = 0;

    public int damage = 0;
    public float accuracy = 1;
    public int pp = 0;
    public Type type;

    public bool physical = true;
    public bool special = false;
}
