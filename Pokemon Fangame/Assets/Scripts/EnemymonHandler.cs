using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Handles with the side of the AI's pokemon in tandem with the BattleHandler
 */
public class EnemymonHandler : MonoBehaviour {

    private SpriteRenderer sr;
    private PokemonObject eo;

    public GameObject playerMon;
    //private PokemonObject po;

    public GameObject battleObject;
    private BattleHandler bh;

    public Text displayName;
    public Text displayHP;

    public int totalSelectableMoves; //Helps with automated move selection

    void Start() {
        eo = gameObject.GetComponent<PokemonObject>();
        //po = playerMon.GetComponent<PokemonObject>();

        bh = battleObject.GetComponent<BattleHandler>();

        foreach (Transform child in transform) {
            if (child.tag == "Pokemon Sprite") {
                sr = child.GetComponent<SpriteRenderer>();
            }
        }

        displayName.text = eo.species.pokemonName;

        transform.localPosition = new Vector3(4.5f, 0.5f, 0);
        sr.sprite = Sprite.Create(eo.species.frontSprite, new Rect(0, 0, eo.species.frontSprite.width, eo.species.frontSprite.height), new Vector2(0.5f, 0.5f));

        for (int i = 0; i < eo.moves.Length; i++) {
            if (eo.moves[i] != null)
                totalSelectableMoves++;
        }
    }
	
	// Update is called once per frame
	void Update () {
        displayHP.text = "HP: " + eo.currentHP.ToString() + "/" + eo.statCalc[0].ToString();

        if (bh.currentPhase == BattleHandler.battlePhase.ENEMY_ATKSELECTION) {
            Debug.Log("Automatic Enemy Move Selection Initiated");
            bh.eoSelectedMove = Random.Range(0, totalSelectableMoves);
            Debug.Log("Selected Move: " + eo.moves[bh.eoSelectedMove].moveName + " with move index of " + bh.eoSelectedMove);
            bh.currentPhase = BattleHandler.battlePhase.ATKEXECUTION;
            Debug.Log("Battle Phase: " + (int)bh.currentPhase);
        }
    }
}
