using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PokemonObject))]
public class PokemonObjectEditor : Editor {
    private string x;
    private int y;

    public override void OnInspectorGUI() {
        PokemonObject pkmn = (PokemonObject)target;

   
        GUILayout.Label("Species Info", EditorStyles.boldLabel);

        GUILayout.Space(5);

        pkmn.species = (PokemonSpecies)EditorGUILayout.ObjectField("Species: ", pkmn.species, typeof(PokemonSpecies), false);

        if (GUILayout.Button("Find Species")) {
            EditorGUIUtility.PingObject(AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GetAssetPath(pkmn.species)));
        }


        if (pkmn.species != null) {
            EditorGUILayout.LabelField("Species Name: ", pkmn.species.pokemonName);
            EditorGUILayout.LabelField("Species Dex Num: ", pkmn.species.dexnum.ToString());
            EditorGUILayout.LabelField("Species Index: ", pkmn.species.index.ToString());
            EditorGUILayout.LabelField("Species Height: ", pkmn.species.height.ToString());
            EditorGUILayout.LabelField("Species Weight: ", pkmn.species.weight.ToString());

            EditorGUILayout.LabelField("Type 1: ", pkmn.species.type1.typeName);
            if (pkmn.species.type2 != null)
                EditorGUILayout.LabelField("Type 2: ", pkmn.species.type2.typeName);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Find Type 1")) {
                EditorGUIUtility.PingObject(AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GetAssetPath(pkmn.species.type1)));
            }
            if (pkmn.species.type2 != null) {
                if (GUILayout.Button("Find Type 2")) {
                    EditorGUIUtility.PingObject(AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GetAssetPath(pkmn.species.type2)));
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(15);

            GUILayout.Label("Base Stats");

            GUILayout.Space(5);

            EditorGUILayout.LabelField("HP: ", pkmn.species.statsBase[0].ToString());
            EditorGUILayout.LabelField("Attack: ", pkmn.species.statsBase[1].ToString());
            EditorGUILayout.LabelField("Defense: ", pkmn.species.statsBase[2].ToString());
            EditorGUILayout.LabelField("Sp. Attack: ", pkmn.species.statsBase[3].ToString());
            EditorGUILayout.LabelField("Sp. Defense: ", pkmn.species.statsBase[4].ToString());
            EditorGUILayout.LabelField("Speed: ", pkmn.species.statsBase[5].ToString());

        }


        

        /*
         * 
         * POKEMON INFO
         * 
         */

        GUILayout.Space(15);

        GUILayout.Label("Pokemon Info", EditorStyles.boldLabel);

        GUILayout.Space(5);

        pkmn.level = Mathf.RoundToInt(EditorGUILayout.Slider("Level: ", pkmn.level, 1, 100));
        pkmn.nat = (Nature)EditorGUILayout.ObjectField("Nature: ", pkmn.nat, typeof(Nature), false);

        for (int i = 1, y = 0; i < pkmn.nat.statModifiers.Length; i++) {
            switch (i) {
                case 1:
                    x = "Attack";
                    break;
                case 2:
                    x = "Defense";
                    break;
                case 3:
                    x = "Sp. Attack";
                    break;
                case 4:
                    x = "Sp. Defense";
                    break;
                case 5:
                    x = "Speed";
                    break;
            }

            if (pkmn.nat.statModifiers[i] == 1.1f) {
                GUILayout.Label("Buffed Stat: " + x);   
            }
            if (pkmn.nat.statModifiers[i] == 0.9f) {
                GUILayout.Label("Nerfed Stat: " + x);
            }

            if (pkmn.nat.statModifiers[i] == 1f) {
                y++;
            }

            if (y == 5)
                GUILayout.Label("No Nerfed/Buffed Stats");
        }

        

        if (GUILayout.Button("Find Nature")) {
            EditorGUIUtility.PingObject(AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GetAssetPath(pkmn.nat)));
        }


        /* 
         * Individual Values
         */

        GUILayout.Space(15);

        GUILayout.Label("Individual Values");

        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("HP: ", pkmn.statsIV[0].ToString());
        pkmn.statsIV[0] = EditorGUILayout.IntSlider(pkmn.statsIV[0], 0, 31);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Attack: ", pkmn.statsIV[1].ToString());
        pkmn.statsIV[1] = EditorGUILayout.IntSlider(pkmn.statsIV[1], 0, 31);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Defense: ", pkmn.statsIV[2].ToString());
        pkmn.statsIV[2] = EditorGUILayout.IntSlider(pkmn.statsIV[2], 0, 31);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Sp. Attack: ", pkmn.statsIV[3].ToString());
        pkmn.statsIV[3] = EditorGUILayout.IntSlider(pkmn.statsIV[3], 0, 31);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Sp. Defense: ", pkmn.statsIV[4].ToString());
        pkmn.statsIV[4] = EditorGUILayout.IntSlider(pkmn.statsIV[4], 0, 31);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Speed: ", pkmn.statsIV[5].ToString());
        pkmn.statsIV[5] = EditorGUILayout.IntSlider(pkmn.statsIV[5], 0, 31);
        GUILayout.EndHorizontal();

        /* 
         * Effort Values
         */

        GUILayout.Space(15);

        GUILayout.Label("Effort Values");

        GUILayout.Space(5);

        
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("HP: ", pkmn.statsEV[0].ToString());
        pkmn.statsEV[0] = EditorGUILayout.IntSlider(pkmn.statsEV[0], 0, 252);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Attack: ", pkmn.statsEV[1].ToString());
        pkmn.statsEV[1] = EditorGUILayout.IntSlider(pkmn.statsEV[1], 0, 252);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Defense: ", pkmn.statsEV[2].ToString());
        pkmn.statsEV[2] = EditorGUILayout.IntSlider(pkmn.statsEV[2], 0, 252);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Sp. Attack: ", pkmn.statsEV[3].ToString());
        pkmn.statsEV[3] = EditorGUILayout.IntSlider(pkmn.statsEV[3], 0, 252);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Sp. Defense: ", pkmn.statsEV[4].ToString());
        pkmn.statsEV[4] = EditorGUILayout.IntSlider(pkmn.statsEV[4], 0, 252);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Speed: ", pkmn.statsEV[5].ToString());
        pkmn.statsEV[5] = EditorGUILayout.IntSlider(pkmn.statsEV[5], 0, 252);
        GUILayout.EndHorizontal();

        GUILayout.Label("Stat Total: " + (pkmn.statsEV[0] + pkmn.statsEV[1] + pkmn.statsEV[2] + pkmn.statsEV[3] + pkmn.statsEV[4] + pkmn.statsEV[5]).ToString());
        if (pkmn.statsEV[0] + pkmn.statsEV[1] + pkmn.statsEV[2] + pkmn.statsEV[3] + pkmn.statsEV[4] + pkmn.statsEV[5] > 510) {
            EditorGUILayout.HelpBox("EVs exceed stat total of 510!", MessageType.Error);
        }

        /* 
         * Calculated Stats
         */

        GUILayout.Space(15);

        GUILayout.Label("Calculated Stats");

        GUILayout.Space(5);

        pkmn.calculateStats();
        EditorGUILayout.LabelField("HP: ", pkmn.statCalc[0].ToString());
        EditorGUILayout.LabelField("Attack: ", pkmn.statCalc[1].ToString());
        EditorGUILayout.LabelField("Defense: ", pkmn.statCalc[2].ToString());
        EditorGUILayout.LabelField("Sp. Attack: ", pkmn.statCalc[3].ToString());
        EditorGUILayout.LabelField("Sp. Defense: ", pkmn.statCalc[4].ToString());
        EditorGUILayout.LabelField("Speed: ", pkmn.statCalc[5].ToString());

        GUILayout.Space(15);

        GUILayout.Label("Moves");

        GUILayout.Space(5);

        pkmn.moves[0] = (Move)EditorGUILayout.ObjectField("Move 1: ", pkmn.moves[0], typeof(Move), false);
        pkmn.moves[1] = (Move)EditorGUILayout.ObjectField("Move 2: ", pkmn.moves[1], typeof(Move), false);
        pkmn.moves[2] = (Move)EditorGUILayout.ObjectField("Move 3: ", pkmn.moves[2], typeof(Move), false);
        pkmn.moves[3] = (Move)EditorGUILayout.ObjectField("Move 4: ", pkmn.moves[3], typeof(Move), false);

        GUILayout.Space(15);

        GUILayout.Label("In-battle Values", EditorStyles.boldLabel);

        GUILayout.Space(5);



        EditorGUILayout.LabelField("Battle HP: ", pkmn.battleHP.ToString());



        EditorUtility.SetDirty(pkmn);
    }

    void OnInspectorUpdate() {
        this.Repaint();
    }
}
