using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour {
    public Text text;
    public Text textShadow;

    public bool textOn;

    public void StartDialogue(Dialogue dialogue, bool referencesOn, string[] references = null) {
        string message = "";

        references = references ?? new string[] {"pokemon"};

        if (referencesOn) {
            message = Parse(dialogue.message[0], references);
        } else {
            message = dialogue.message[0];
        }

        textOn = true;

        text.text = "";
        textShadow.text = "";

        dialogue.index = 0;

        StartCoroutine(TypeText(message));

        textOn = false;
    }

    public bool ContinueDialogue(Dialogue dialogue, bool referencesOn, string[] references = null) {
        string message = "";
        textOn = true;

        if (dialogue.index == dialogue.message.Length - 1) {
            Debug.Log("Dialogue has Ended");
            
            textOn = false;
            return true;
        } else {
            dialogue.index++;

            references = references ?? new string[] { "pokemon" };

            if (referencesOn) {
                message = Parse(dialogue.message[dialogue.index], references);
            } else {
                message = dialogue.message[dialogue.index];
            }

            text.text = "";
            textShadow.text = "";

            StartCoroutine(TypeText(message));

            textOn = false;
            return false;
        }
        
    }

    private IEnumerator TypeText(string dialogue) {
        foreach (char letter in dialogue.ToCharArray()) {
            text.text += letter;
            textShadow.text += letter;
            yield return new WaitForSeconds(0.02f);

            if (Input.GetKeyUp(KeyCode.Space)) {
                break;
            }
        }
    }

    /* REPLACING A CERTAIN CHARACTER IN A STRING WITH A TARGET REPLACEMENT
     * 
     * Input = "My name is %"
     * replacement = "colt"
     * 
     * Output = "My name is colt"
     * 
     * MAY ALSO WORK WITH MULTIPLE REPLACEMENTS
     * 
     * Input = "Hello, %, I am %"
     * replacements = { "colt", "bob" }
     * 
     * Output = "Hello, colt, I am bob"
     */
    public string Parse(string origText, string[] replacement) {
        char[] origTextArr = origText.ToCharArray();
        List<char[]> replacements = new List<char[]>();
        
        foreach (string text in replacement) {
            replacements.Add(text.ToCharArray());
        }

        int bkmrk = -1;
        string final = "";

        //Repeats for how many replacements there are
        for (int k = 0; k < replacement.Length; k++) {
            bkmrk = -1;
            final = "";

            //First loop runs through the original text searching for any '%' character
            for (int i = 0; i < origTextArr.Length; i++) {
                if (origTextArr[i] == '%') {
                    bkmrk = i;
                    //the first '%' has been found; Every character before that will be stored in a separate string
                    for (int j = 0; j < i; j++) {
                        final += origTextArr[j];

                        if (j == i - 1)
                            break;
                    }
                }

                if (bkmrk != -1)
                    break;
            }

            //Second loop adds the replacement in place of the '%'
            for (int i = 0; i < replacements[k].Length; i++) {
                final += replacements[k][i];
            }

            //Third loops adds the final
            for (int i = bkmrk + 1; i < origTextArr.Length; i++) {
                final += origTextArr[i];
            }


            origTextArr = final.ToCharArray();
        }

        return final;
    }
}
