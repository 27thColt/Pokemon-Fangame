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

    private BattleHandler bh;

    public Text displayName, displayNameShadow;
    public Text displayLevel, displayLevelShadow;

    public GameObject HPbar;
    public GameObject statusIcon;

    public int totalSelectableMoves; //Helps with automated move selection

    void Start() {
        eo = gameObject.GetComponent<PokemonObject>();

        bh = FindObjectOfType<BattleHandler>();

        foreach (Transform child in transform) {
            if (child.tag == "Pokemon Sprite") {
                sr = child.GetComponent<SpriteRenderer>();
            }
        }

        //Text business
        displayName.text = eo.species.pokemonName;
        displayNameShadow.text = eo.species.pokemonName;
        displayLevel.text = eo.level.ToString();
        displayLevelShadow.text = eo.level.ToString();

        transform.localPosition = new Vector3(0.62f, 0.06f, 0);
        sr.sprite = Sprite.Create(eo.species.frontSprite, new Rect(0, 0, eo.species.frontSprite.width, eo.species.frontSprite.height), new Vector2(0.5f, 0.5f));

        for (int i = 0; i < eo.moves.Length; i++) {
            if (eo.moves[i] != null)
                totalSelectableMoves++;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (bh.currentPhase == battlePhase.ENEMY_ATKSELECTION) {
            Debug.Log("Automatic Enemy Move Selection Initiated");
            bh.eoSelectedMove = Random.Range(0, totalSelectableMoves);
            Debug.Log("Selected Move: " + eo.moves[bh.eoSelectedMove].moveName + " with move index of " + bh.eoSelectedMove);
            bh.currentPhase = battlePhase.ATKEXECUTION;
            Debug.Log("Battle Phase: " + (int)bh.currentPhase);
        }

        //Status Icons
        if (eo.currentEffect == null) {
            statusIcon.GetComponent<Image>().enabled = false;
        } else {
            statusIcon.GetComponent<Image>().enabled = true;
            statusIcon.GetComponent<Image>().sprite = eo.currentEffect.icon;
        }

        //HP Bar
        HPbar.GetComponent<Image>().fillAmount = eo.battleStats[0] / eo.statCalc[0];

        
        if (eo.battleStats[0] / eo.statCalc[0] >= 0.5) {
            HPbar.GetComponent<Image>().color = new Color32(22, 171, 16, 255);
        } else if (eo.battleStats[0] / eo.statCalc[0] < 0.5 && eo.battleStats[0] / eo.statCalc[0] >= 0.2) {
            HPbar.GetComponent<Image>().color = new Color32(248, 215, 31, 255);
        } else if (eo.battleStats[0] / eo.statCalc[0] < 0.2) {
            HPbar.GetComponent<Image>().color = new Color32(244, 63, 63, 255);
        }
    }
}
