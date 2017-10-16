using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PokemonSpecies))]
public class PokemonSpeciesEditor : Editor {
    public override void OnInspectorGUI() {
        PokemonSpecies ps = (PokemonSpecies)target;

        GUILayout.Label("Pokemon Info", EditorStyles.boldLabel);

        GUILayout.Space(5);

        ps.pokemonName = EditorGUILayout.TextField("Pokemon Name: ", ps.pokemonName);

        ps.index = EditorGUILayout.IntField("Index: ", ps.index);
        ps.dexnum = EditorGUILayout.IntField("Dex Number: ", ps.dexnum);
        ps.height = EditorGUILayout.FloatField("Height: ", ps.height);
        ps.weight = EditorGUILayout.FloatField("Weight: ", ps.weight);

        GUILayout.Space(15);

        GUILayout.Label("Types", EditorStyles.boldLabel);

        GUILayout.Space(5);

        ps.type1 = (Type)EditorGUILayout.ObjectField("Type 1", ps.type1, typeof(Type), false);
        ps.type2 = (Type)EditorGUILayout.ObjectField("Type 2", ps.type2, typeof(Type), false);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Find Type 1")) {
            EditorGUIUtility.PingObject(AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GetAssetPath(ps.type1)));
        }
        if (ps.type2 != null) {
            if (GUILayout.Button("Find Type 2")) {
                EditorGUIUtility.PingObject(AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GetAssetPath(ps.type2)));
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        GUILayout.Label("Stats", EditorStyles.boldLabel);

        GUILayout.Space(5);

        ps.statsBase[0] = EditorGUILayout.IntField("HP: ", ps.statsBase[0]);
        ps.statsBase[1] = EditorGUILayout.IntField("Attack: ", ps.statsBase[1]);
        ps.statsBase[2] = EditorGUILayout.IntField("Defense: ", ps.statsBase[2]);
        ps.statsBase[3] = EditorGUILayout.IntField("Sp. Attack: ", ps.statsBase[3]);
        ps.statsBase[4] = EditorGUILayout.IntField("Sp. Defense: ", ps.statsBase[4]);
        ps.statsBase[5] = EditorGUILayout.IntField("Speed: ", ps.statsBase[5]);

        GUILayout.Space(15);

        GUILayout.Label("Sprites", EditorStyles.boldLabel);

        GUILayout.Space(5);

        ps.frontSprite = (Texture2D)EditorGUILayout.ObjectField("Front: ", ps.frontSprite, typeof(Texture2D), false);
        ps.backSprite = (Texture2D)EditorGUILayout.ObjectField("Back: ", ps.backSprite, typeof(Texture2D), false);
        ps.miniSprite = (Texture2D)EditorGUILayout.ObjectField("Mini: ", ps.miniSprite, typeof(Texture2D), false);


        EditorUtility.SetDirty(ps);
    }

    void OnInspectorUpdate() {
        this.Repaint();
    }
}
