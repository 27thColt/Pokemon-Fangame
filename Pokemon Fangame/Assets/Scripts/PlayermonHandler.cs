using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Handles with the side of the player's pokemon in tandem with the BattleHandler
 */
public class PlayermonHandler : MonoBehaviour {

    private SpriteRenderer sr;
    private PokemonObject po;

    public GameObject enemyMon;
    private PokemonObject eo;

    public GameObject battleObject;
    private BattleHandler bh;

    public Button moveB1;
    public Button moveB2;
    public Button moveB3;
    public Button moveB4;

    public Text displayName;
    public Text displayHP;

    void Start () {
        po = gameObject.GetComponent<PokemonObject>();
        eo = enemyMon.GetComponent<PokemonObject>();

        bh = battleObject.GetComponent<BattleHandler>();

        foreach (Transform child in transform) {
            if (child.tag == "Pokemon Sprite") {
                sr = child.GetComponent<SpriteRenderer>();
            }
        }

        /*
         * UI
         */
        moveB1.GetComponentInChildren<Text>().text = po.moves[0].moveName;

        if (bh.phase == 1 && po.moves[1] == null) {
            moveB2.interactable = false;
        } else {
            moveB2.GetComponentInChildren<Text>().text = po.moves[1].moveName;
        }


        if (bh.phase == 1 && po.moves[2] == null) {
            moveB3.interactable = false;
        } else {
            moveB3.GetComponentInChildren<Text>().text = po.moves[2].moveName;
        }


        if (bh.phase == 1 && po.moves[3] == null) {
            moveB4.interactable = false; 
        } else {
            moveB4.GetComponentInChildren<Text>().text = po.moves[3].moveName;
        }

        displayName.text = po.species.pokemonName;
        /*
         * 
         */

        transform.localPosition = new Vector3(-4, -2, 0);
        sr.sprite = Sprite.Create(po.species.backSprite, new Rect(0, 0, po.species.backSprite.width, po.species.backSprite.height), new Vector2(0.5f, 0.5f));

    }


    void Update() {
        if (bh.phase == 1) {
            moveB1.interactable = true;
        } else {
            moveB1.interactable = false;
        }

        if (bh.phase == 1 && po.moves[1] != null) {
            moveB2.interactable = true;
        } else {
            moveB2.interactable = false;
        }


        if (bh.phase == 1 && po.moves[2] != null) {
            moveB3.interactable = true;
        } else {
            moveB3.interactable = false;
        }


        if (bh.phase == 1 && po.moves[3] != null) {
            moveB4.interactable = true;
        } else {
            moveB4.interactable = false;
        }

        displayHP.text = "HP: " + po.battleHP.ToString() + "/" + po.statCalc[0].ToString();
    }

    public void selectMove(int moveNum) {
        bh.poSelectedMove = moveNum;
        bh.phase = 2;
        Debug.Log("Battle Phase: " + bh.phase);
    }

    
}
