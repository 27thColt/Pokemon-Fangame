using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Type))]
public class PokemonTypeEditor : Editor {
    public string typename;
    public bool[] showTypeData = new bool[18] {false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false};
    
    public override void OnInspectorGUI() {
        Type type = (Type)target;

        type.typeName = EditorGUILayout.TextField("Name: ", type.typeName);
        type.index = EditorGUILayout.IntField("Index: ", type.index);

        GUILayout.Space(15);
        GUILayout.Label("Offensives", EditorStyles.boldLabel);

        for (int i = 0; i < type.offensives.Length; i++) {
            switch (i) {
                case 0:
                    typename = "NORMAL: ";
                    break;
                case 1:
                    typename = "GRASS: ";
                    break;
                case 2:
                    typename = "FIRE: ";
                    break;
                case 3:
                    typename = "WATER: ";
                    break;
                case 4:
                    typename = "ELECTRIC: ";
                    break;
                case 5:
                    typename = "ICE: ";
                    break;
                case 6:
                    typename = "BUG: ";
                    break;
                case 7:
                    typename = "POISON: ";
                    break;
                case 8:
                    typename = "FLYING: ";
                    break;
                case 9:
                    typename = "FIGHTING: ";
                    break;
                case 10:
                    typename = "GROUND: ";
                    break;
                case 11:
                    typename = "ROCK: ";
                    break;
                case 12:
                    typename = "STEEL: ";
                    break;
                case 13:
                    typename = "PSYCHIC: ";
                    break;
                case 14:
                    typename = "DARK: ";
                    break;
                case 15:
                    typename = "GHOST: ";
                    break;
                case 16:
                    typename = "FAIRY: ";
                    break;
                case 17:
                    typename = "DRAGON: ";
                    break;
            }
            
            if (type.offensiveSE[i] && !type.offensiveNVE[i] && !type.offensiveNE[i]) {
                showTypeData[i] = EditorGUILayout.Foldout((showTypeData[i] || type.offensiveSE[i] || type.offensiveNVE[i] || type.offensiveNE[i]), typename + " [SUPER EFFECTIVE]");
            } else if (type.offensiveNVE[i] && !type.offensiveSE[i] && !type.offensiveNE[i]) {
                showTypeData[i] = EditorGUILayout.Foldout((showTypeData[i] || type.offensiveSE[i] || type.offensiveNVE[i] || type.offensiveNE[i]), typename + " [NOT VERY EFFECTIVE]");
            } else if (type.offensiveNE[i] && !type.offensiveSE[i] && !type.offensiveNVE[i]) {
                showTypeData[i] = EditorGUILayout.Foldout((showTypeData[i] || type.offensiveSE[i] || type.offensiveNVE[i] || type.offensiveNE[i]), typename + " [NO EFFECT]");
            } else {
                showTypeData[i] = EditorGUILayout.Foldout((showTypeData[i] || type.offensiveSE[i] || type.offensiveNVE[i] || type.offensiveNE[i]), typename);
            }
            if (showTypeData[i] || type.offensiveSE[i] || type.offensiveNVE[i] || type.offensiveNE[i]) {
                EditorGUI.indentLevel++;
                type.offensiveSE[i] = EditorGUILayout.Toggle("2x: ", type.offensiveSE[i]);
                type.offensiveNVE[i] = EditorGUILayout.Toggle("0.5x: ", type.offensiveNVE[i]);
                type.offensiveNE[i] = EditorGUILayout.Toggle("0x: ", type.offensiveNE[i]);
                EditorGUI.indentLevel--;
            }
            


            if (type.offensiveSE[i] && !type.offensiveNVE[i] && !type.offensiveNE[i]) {
                type.offensives[i] = 2;
            } else if (type.offensiveNVE[i] && !type.offensiveSE[i] && !type.offensiveNE[i]) {
                type.offensives[i] = 0.5f;
            } else if (type.offensiveNE[i] && !type.offensiveSE[i] && !type.offensiveNVE[i]) {
                type.offensives[i] = 0;
            } else if ((type.offensiveNVE[i] && type.offensiveSE[i]) || (type.offensiveNVE[i] && type.offensiveNE[i]) || (type.offensiveSE[i] && type.offensiveNE[i])) {
                EditorGUILayout.HelpBox("This type has multiple modifiers!", MessageType.Error);
            } else {
                type.offensives[i] = 1;
            }


        }
       
        

        EditorUtility.SetDirty(type);
    }


    void OnInspectorUpdate() {
        this.Repaint();
    }
}
