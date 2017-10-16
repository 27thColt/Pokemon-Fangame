using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon Species", menuName = "Pokemon", order = 1)]
[System.Serializable]
public class PokemonSpecies : ScriptableObject {
    public string pokemonName = "Pokemon Species Name";
    public int index = 0; //for referencing
    public int dexnum = 0;
    public float height = 0; //in meters
    public float weight = 0; //in kilograms 

    public Texture2D frontSprite;
    public Texture2D backSprite;
    public Texture2D miniSprite;

    public Type type1 = null;
    public Type type2 = null;

    public int[] statsBase = new int[6] { 0, 0, 0, 0, 0, 0 };
}
