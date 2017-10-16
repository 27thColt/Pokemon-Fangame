using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Nature))]
public class PokemonNatureEditor : Editor {
    public override void OnInspectorGUI() {
        Nature nat = (Nature)target;

        GUILayout.Label("Nature Info", EditorStyles.boldLabel);

        GUILayout.Space(5);

        nat.natureName = EditorGUILayout.TextField("Nature Name: ", nat.natureName);
        nat.index = EditorGUILayout.IntField("Index: ", nat.index);

        GUILayout.Space(15);

        GUILayout.Label("Stat Modifiers", EditorStyles.boldLabel);

        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        GUILayout.Label("Buff");
        nat.atkb = EditorGUILayout.Toggle("Attack", nat.atkb);
        if (nat.atkb) {
            nat.statModifiers[1] = 1.1f;
            nat.atkn = false;
        }

        nat.defb = EditorGUILayout.Toggle("Defense", nat.defb);
        if (nat.defb) {
            nat.statModifiers[2] = 1.1f;
            nat.defn = false;
        }

        nat.spab = EditorGUILayout.Toggle("Sp. Attack", nat.spab);
        if (nat.spab) {
            nat.statModifiers[3] = 1.1f;
            nat.span = false;
        }

        nat.spdb = EditorGUILayout.Toggle("Sp. Defense", nat.spdb);
        if (nat.spdb) {
            nat.statModifiers[4] = 1.1f;
            nat.spdn = false;
        }

        nat.speb = EditorGUILayout.Toggle("Speed", nat.speb);
        if (nat.speb) {
            nat.statModifiers[5] = 1.1f;
            nat.spen = false;
        }

        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        GUILayout.Label("Nerf");
        nat.atkn = EditorGUILayout.Toggle("Attack", nat.atkn);
        if (nat.atkn) {
            nat.statModifiers[1] = 0.9f;
            nat.atkb = false;
        }

        nat.defn = EditorGUILayout.Toggle("Defense", nat.defn);
        if (nat.defn) {
            nat.statModifiers[2] = 0.9f;
            nat.defb = false;
        }

        nat.span = EditorGUILayout.Toggle("Sp. Attack", nat.span);
        if (nat.span) {
            nat.statModifiers[3] = 0.9f;
            nat.spab = false;
        }

        nat.spdn = EditorGUILayout.Toggle("Sp. Defense", nat.spdn);
        if (nat.spdn) {
            nat.statModifiers[4] = 0.9f;
            nat.spdb = false;
        }

        nat.spen = EditorGUILayout.Toggle("Speed", nat.spen);
        if (nat.spen) {
            nat.statModifiers[5] = 0.9f;
            nat.speb = false;
        }

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        if (!nat.atkn && !nat.atkb) {
            nat.statModifiers[1] = 1;
        }

        if (!nat.defn && !nat.defb) {
            nat.statModifiers[2] = 1;
        }

        if (!nat.span && !nat.spab) {
            nat.statModifiers[3] = 1;
        }

        if (!nat.spdn && !nat.spdb) {
            nat.statModifiers[4] = 1;
        }

        if (!nat.spen && !nat.speb) {
            nat.statModifiers[5] = 1;
        }

        if (GUILayout.Button("Reset Modifiers")) {
            nat.atkb = false;
            nat.atkn = false;
            nat.defb = false;
            nat.defn = false;
            nat.spab = false;
            nat.span = false;
            nat.spdb = false;
            nat.spdn = false;
            nat.speb = false;
            nat.spen = false;
        }

        EditorUtility.SetDirty(nat);
    }

    void OnInspectorUpdate() {
        this.Repaint();
    }
}
