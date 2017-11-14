using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIHandler : MonoBehaviour {
    public GameObject moveB1;
    public GameObject moveB2;
    public GameObject moveB3;
    public GameObject moveB4;

    public Vector3 move1XYZ = new Vector3(-163.5f, -179.2f, 0);
    public Vector3 move2XYZ = new Vector3(158f, -179.2f, 0);
    public Vector3 move3XYZ = new Vector3(479.2f, -179.2f, 0);
    public Vector3 move4XYZ = new Vector3(-484.7f, -179.2f, 0);

    public GameObject pokemonObject;
    private PokemonObject po;

    public GameObject battleHandler;
    private BattleHandler bh;

    public Sprite moveNullDesel;
    public Sprite moveNullSel;

    public int moveSel;

    public void Start() {
        moveSel = 1;

        po = pokemonObject.GetComponent<PokemonObject>();
        bh = battleHandler.GetComponent<BattleHandler>();


        moveB1.GetComponentInChildren<Text>().text = po.moves[0].moveName;
        moveB1.GetComponent<Image>().sprite = po.moves[0].type.moveButtonDesel;


        if (bh.currentPhase == battlePhase.PLAYER_ATKSELECTION && po.moves[1] == null) {
            moveB2.GetComponent<Button>().interactable = false;
        } else {
            moveB2.GetComponentInChildren<Text>().text = po.moves[1].moveName;
            moveB2.GetComponent<Image>().sprite = po.moves[1].type.moveButtonDesel;
        }


        if (bh.currentPhase == battlePhase.PLAYER_ATKSELECTION && po.moves[2] == null) {
            moveB3.GetComponent<Button>().interactable = false;
        } else {
            moveB3.GetComponentInChildren<Text>().text = po.moves[2].moveName;
            moveB3.GetComponent<Image>().sprite = po.moves[2].type.moveButtonDesel;
        }


        if (bh.currentPhase == battlePhase.PLAYER_ATKSELECTION && po.moves[3] == null) {
            moveB4.GetComponent<Button>().interactable = false;
        } else {
            moveB4.GetComponentInChildren<Text>().text = po.moves[3].moveName;
            moveB4.GetComponent<Image>().sprite = po.moves[3].type.moveButtonDesel;
        }


    }

    public void Update() {
        //This part could use a loop
        switch (moveSel) {
            case 1:
                moveB1.GetComponent<RectTransform>().anchoredPosition = move1XYZ; //First Move selected
                moveB2.GetComponent<RectTransform>().anchoredPosition = move2XYZ;
                moveB3.GetComponent<RectTransform>().anchoredPosition = move3XYZ;
                moveB4.GetComponent<RectTransform>().anchoredPosition = move4XYZ;

                //Move Sprites

                moveB1.GetComponent<Image>().sprite = po.moves[0].type.moveButtonSel;

                if (po.moves[1] != null)
                    moveB2.GetComponent<Image>().sprite = po.moves[1].type.moveButtonDesel;
                else
                    moveB2.GetComponent<Image>().sprite = moveNullDesel;

                if (po.moves[2] != null)
                    moveB3.GetComponent<Image>().sprite = po.moves[2].type.moveButtonDesel;
                else
                    moveB3.GetComponent<Image>().sprite = moveNullDesel;

                if (po.moves[3] != null)
                    moveB4.GetComponent<Image>().sprite = po.moves[3].type.moveButtonDesel;
                else
                    moveB4.GetComponent<Image>().sprite = moveNullDesel;


                break;
            case 2:
                moveB1.GetComponent<RectTransform>().anchoredPosition = move4XYZ;
                moveB2.GetComponent<RectTransform>().anchoredPosition = move1XYZ; //Second Move selected
                moveB3.GetComponent<RectTransform>().anchoredPosition = move2XYZ;
                moveB4.GetComponent<RectTransform>().anchoredPosition = move3XYZ;

                //Move Sprites

                moveB1.GetComponent<Image>().sprite = po.moves[0].type.moveButtonDesel;

                if (po.moves[1] != null)
                    moveB2.GetComponent<Image>().sprite = po.moves[1].type.moveButtonSel;
                else
                    moveB2.GetComponent<Image>().sprite = moveNullSel;

                if (po.moves[2] != null)
                    moveB3.GetComponent<Image>().sprite = po.moves[2].type.moveButtonDesel;
                else
                    moveB3.GetComponent<Image>().sprite = moveNullDesel;

                if (po.moves[3] != null)
                    moveB4.GetComponent<Image>().sprite = po.moves[3].type.moveButtonDesel;
                else
                    moveB4.GetComponent<Image>().sprite = moveNullDesel;

                break;
            case 3:
                moveB1.GetComponent<RectTransform>().anchoredPosition = move3XYZ;
                moveB2.GetComponent<RectTransform>().anchoredPosition = move4XYZ;
                moveB3.GetComponent<RectTransform>().anchoredPosition = move1XYZ; //Third Move selected
                moveB4.GetComponent<RectTransform>().anchoredPosition = move2XYZ;

                //Move Sprites

                moveB1.GetComponent<Image>().sprite = po.moves[0].type.moveButtonDesel;

                if (po.moves[1] != null)
                    moveB2.GetComponent<Image>().sprite = po.moves[1].type.moveButtonDesel;
                else
                    moveB2.GetComponent<Image>().sprite = moveNullDesel;

                if (po.moves[2] != null)
                    moveB3.GetComponent<Image>().sprite = po.moves[2].type.moveButtonSel;
                else
                    moveB3.GetComponent<Image>().sprite = moveNullSel;

                if (po.moves[3] != null)
                    moveB4.GetComponent<Image>().sprite = po.moves[3].type.moveButtonDesel;
                else
                    moveB4.GetComponent<Image>().sprite = moveNullDesel;

                break;
            case 4:
                moveB1.GetComponent<RectTransform>().anchoredPosition = move2XYZ;
                moveB2.GetComponent<RectTransform>().anchoredPosition = move3XYZ;
                moveB3.GetComponent<RectTransform>().anchoredPosition = move4XYZ;
                moveB4.GetComponent<RectTransform>().anchoredPosition = move1XYZ; //Fourth Move selected

                //Move Sprites

                moveB1.GetComponent<Image>().sprite = po.moves[0].type.moveButtonDesel;

                if (po.moves[1] != null)
                    moveB2.GetComponent<Image>().sprite = po.moves[1].type.moveButtonDesel;
                else
                    moveB2.GetComponent<Image>().sprite = moveNullDesel;

                if (po.moves[2] != null)
                    moveB3.GetComponent<Image>().sprite = po.moves[2].type.moveButtonDesel;
                else
                    moveB3.GetComponent<Image>().sprite = moveNullDesel;

                if (po.moves[3] != null)
                    moveB4.GetComponent<Image>().sprite = po.moves[3].type.moveButtonSel;
                else
                    moveB4.GetComponent<Image>().sprite = moveNullSel;

                break;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (moveSel == 4) {
                moveSel = 1;
            } else {
                moveSel++;
            }
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (moveSel == 1) {
                moveSel = 4;
            } else {
                moveSel--;
            }
        }

        if (bh.currentPhase == battlePhase.PLAYER_ATKSELECTION) {
            moveB1.GetComponent<Button>().interactable = true;
        } else {
            moveB1.GetComponent<Button>().interactable = false;
        }

        if (bh.currentPhase == battlePhase.PLAYER_ATKSELECTION && po.moves[1] != null) {
            moveB2.GetComponent<Button>().interactable = true;
        } else {
            moveB2.GetComponent<Button>().interactable = false;
        }


        if (bh.currentPhase == battlePhase.PLAYER_ATKSELECTION && po.moves[2] != null) {
            moveB3.GetComponent<Button>().interactable = true;
        } else {
            moveB3.GetComponent<Button>().interactable = false;
        }


        if (bh.currentPhase == battlePhase.PLAYER_ATKSELECTION && po.moves[3] != null) {
            moveB4.GetComponent<Button>().interactable = true;
        } else {
            moveB4.GetComponent<Button>().interactable = false;
        }
    }
}
