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

    private BattleHandler bh;

    public Text displayName, displayNameShadow;
    public Text displayCurrentHP, displayCurrentHPShadow;
    public Text displayTotalHP, displayTotalHPShadow;
    public Text displayLevel, displayLevelShadow;

    public GameObject HPbar;
    public GameObject statusIcon;

    void Start () {
        po = gameObject.GetComponent<PokemonObject>();

        bh = FindObjectOfType<BattleHandler>();

        foreach (Transform child in transform) {
            if (child.tag == "Pokemon Sprite") {
                sr = child.GetComponent<SpriteRenderer>();
            }
        }

        /*
         * UI
         */

      
        //Text Business
        displayName.text = po.species.pokemonName;
        displayNameShadow.text = po.species.pokemonName;
        displayTotalHP.text = po.statCalc[0].ToString();
        displayTotalHPShadow.text = po.statCalc[0].ToString();
        displayLevel.text = po.level.ToString();
        displayLevelShadow.text = po.level.ToString();

        transform.localPosition = new Vector3(-0.64f, -0.41f, 0);
        sr.sprite = Sprite.Create(po.species.backSprite, new Rect(0, 0, po.species.backSprite.width, po.species.backSprite.height), new Vector2(0.5f, 0.5f));
    }


    void Update() {
        /*
         * 
         * UI Stuff
         * 
         */

        displayCurrentHP.text = po.battleStats[0].ToString();
        displayCurrentHPShadow.text = po.battleStats[0].ToString();
        
        //Status Icons
        if (po.currentEffect == null) {
            statusIcon.GetComponent<Image>().enabled = false;
        } else {
            statusIcon.GetComponent<Image>().enabled = true;
            statusIcon.GetComponent<Image>().sprite = po.currentEffect.icon;
        }


        //HP Bar
        HPbar.GetComponent<Image>().fillAmount = po.battleStats[0] / po.statCalc[0];

        if (po.battleStats[0] / po.statCalc[0] >= 0.5) {
            HPbar.GetComponent<Image>().color = new Color32(22, 171, 16, 255);
        } else if (po.battleStats[0] / po.statCalc[0] < 0.5 && po.battleStats[0] / po.statCalc[0] >= 0.2) {
            HPbar.GetComponent<Image>().color = new Color32(248, 215, 31, 255);
        } else if (po.battleStats[0] / po.statCalc[0] < 0.2) {
            HPbar.GetComponent<Image>().color = new Color32(244, 63, 63, 255);
        }
    }

    public void selectMove(int moveNum) {
        if (GetComponent<PokemonObject>().moves[moveNum] != null) {
            bh.poSelectedMove = moveNum;
            bh.currentPhase = battlePhase.ENEMY_ATKSELECTION;
            Debug.Log("Battle Phase: " + (int)bh.currentPhase);
        }
    }
}
