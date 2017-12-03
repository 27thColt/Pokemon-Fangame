using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIHandler : MonoBehaviour {
    public GameObject[] moveButtons;

    public Vector3 move1XYZ = new Vector3(-163.5f, -179.2f, 0);
    public Vector3 move2XYZ = new Vector3(158f, -179.2f, 0);
    public Vector3 move3XYZ = new Vector3(479.2f, -179.2f, 0);
    public Vector3 move4XYZ = new Vector3(-484.7f, -179.2f, 0);

    public GameObject pokemonObject;
    private PokemonObject po;

    public GameObject enemyObject;
    private PokemonObject eo;

    private BattleHandler bh;

    private DialogueHandler dh;

    public Sprite moveNullDesel;
    public Sprite moveNullSel;

    public int moveSel;

    public Dialogue encounterMessage;
    public Dialogue attackMessage;
    public Dialogue[] statChange;

    public void Start() {
        moveSel = 1;

        po = pokemonObject.GetComponent<PokemonObject>();
        eo = enemyObject.GetComponent<PokemonObject>();
        bh = FindObjectOfType<BattleHandler>();
        dh = FindObjectOfType<DialogueHandler>();

        for (int i = 0; i < moveButtons.Length; i++) {
            if (i == 0) {
                moveButtons[i].GetComponentInChildren<Text>().text = po.moves[i].moveName;
                moveButtons[i].GetComponent<Image>().sprite = po.moves[i].type.moveButtonDesel;
            } else {
                if (po.moves[i] != null) {
                    moveButtons[i].GetComponentInChildren<Text>().text = po.moves[i].moveName;
                    moveButtons[i].GetComponent<Image>().sprite = po.moves[1].type.moveButtonDesel;
                } else {
                    moveButtons[i].GetComponentInChildren<Text>().text = " ";
                }
            }
        }

        dh.StartDialogue(encounterMessage, true, new string[] { eo.species.pokemonName });
       
    }   

    public void Update() {
        if (bh.currentPhase == battlePhase.START_PHASE) {
            


            if (Input.GetKeyUp(KeyCode.Return)) {
                if (dh.ContinueDialogue(encounterMessage, true, new string[] { po.species.pokemonName })) {
                    StartCoroutine(EnableUI(0.5f));
                }

            }
        }

        
        if (bh.currentPhase == battlePhase.PLAYER_ATKSELECTION && !dh.textOn) {
            switch (moveSel) {
                case 1:
                    moveButtons[0].GetComponent<RectTransform>().anchoredPosition = move1XYZ; //First Move selected
                    moveButtons[1].GetComponent<RectTransform>().anchoredPosition = move2XYZ;
                    moveButtons[2].GetComponent<RectTransform>().anchoredPosition = move3XYZ;
                    moveButtons[3].GetComponent<RectTransform>().anchoredPosition = move4XYZ;

                    //Move Sprites

                    for (int i = 0; i < moveButtons.Length; i++) {
                        if (i != 0) {
                            if (po.moves[i] != null) {
                                moveButtons[i].GetComponent<Image>().sprite = po.moves[i].type.moveButtonDesel;
                            } else {
                                moveButtons[i].GetComponent<Image>().sprite = moveNullDesel;
                            }
                        } else {
                            if (po.moves[i] != null) {
                                moveButtons[i].GetComponent<Image>().sprite = po.moves[i].type.moveButtonSel;
                            } else {
                                moveButtons[i].GetComponent<Image>().sprite = moveNullSel;
                            }
                        }
                    }

                    break;
                case 2:
                    moveButtons[0].GetComponent<RectTransform>().anchoredPosition = move4XYZ;
                    moveButtons[1].GetComponent<RectTransform>().anchoredPosition = move1XYZ; //Second Move selected
                    moveButtons[2].GetComponent<RectTransform>().anchoredPosition = move2XYZ;
                    moveButtons[3].GetComponent<RectTransform>().anchoredPosition = move3XYZ;

                    //Move Sprites

                    for (int i = 0; i < moveButtons.Length; i++) {
                        if (i != 1) {
                            if (po.moves[i] != null) {
                                moveButtons[i].GetComponent<Image>().sprite = po.moves[i].type.moveButtonDesel;
                            } else {
                                moveButtons[i].GetComponent<Image>().sprite = moveNullDesel;
                            }
                        } else {
                            if (po.moves[i] != null) {
                                moveButtons[i].GetComponent<Image>().sprite = po.moves[i].type.moveButtonSel;
                            } else {
                                moveButtons[i].GetComponent<Image>().sprite = moveNullSel;
                            }
                        }
                    }

                    break;
                case 3:
                    moveButtons[0].GetComponent<RectTransform>().anchoredPosition = move3XYZ;
                    moveButtons[1].GetComponent<RectTransform>().anchoredPosition = move4XYZ;
                    moveButtons[2].GetComponent<RectTransform>().anchoredPosition = move1XYZ; //Third Move selected
                    moveButtons[3].GetComponent<RectTransform>().anchoredPosition = move2XYZ;

                    //Move Sprites

                    for (int i = 0; i < moveButtons.Length; i++) {
                        if (i != 2) {
                            if (po.moves[i] != null) {
                                moveButtons[i].GetComponent<Image>().sprite = po.moves[i].type.moveButtonDesel;
                            } else {
                                moveButtons[i].GetComponent<Image>().sprite = moveNullDesel;
                            }
                        } else {
                            if (po.moves[i] != null) {
                                moveButtons[i].GetComponent<Image>().sprite = po.moves[i].type.moveButtonSel;
                            } else {
                                moveButtons[i].GetComponent<Image>().sprite = moveNullSel;
                            }
                        }
                    }

                    break;
                case 4:
                    moveButtons[0].GetComponent<RectTransform>().anchoredPosition = move2XYZ;
                    moveButtons[1].GetComponent<RectTransform>().anchoredPosition = move3XYZ;
                    moveButtons[2].GetComponent<RectTransform>().anchoredPosition = move4XYZ;
                    moveButtons[3].GetComponent<RectTransform>().anchoredPosition = move1XYZ; //Fourth Move selected

                    //Move Sprites

                    for (int i = 0; i < moveButtons.Length; i++) {
                        if (i != 3) {
                            if (po.moves[i] != null) {
                                moveButtons[i].GetComponent<Image>().sprite = po.moves[i].type.moveButtonDesel;
                            } else {
                                moveButtons[i].GetComponent<Image>().sprite = moveNullDesel;
                            }
                        } else {
                            if (po.moves[i] != null) {
                                moveButtons[i].GetComponent<Image>().sprite = po.moves[i].type.moveButtonSel;
                            } else {
                                moveButtons[i].GetComponent<Image>().sprite = moveNullSel;
                            }
                        }
                    }
                    break;
            }

            if (Input.GetKeyUp(KeyCode.RightArrow)) {
                if (moveSel == 4) {
                    moveSel = 1;
                } else {
                    moveSel++;
                }
            } else if (Input.GetKeyUp(KeyCode.LeftArrow)) {
                if (moveSel == 1) {
                    moveSel = 4;
                } else {
                    moveSel--;
                }
            }

            if (Input.GetKeyUp(KeyCode.Return)) {
                pokemonObject.GetComponent<PlayermonHandler>().selectMove(moveSel - 1);
            }
        } else {
            for (int i = 0; i < moveButtons.Length; i++) {
                if (po.moves[i] != null) {
                    moveButtons[i].GetComponent<Image>().sprite = po.moves[i].type.moveButtonDesel;
                } else {
                    moveButtons[i].GetComponent<Image>().sprite = moveNullDesel;
                }
            }
        }
    }

    IEnumerator EnableUI(float x) {
        yield return new WaitForSeconds(x);
        bh.currentPhase = battlePhase.PLAYER_ATKSELECTION;
    }
}
