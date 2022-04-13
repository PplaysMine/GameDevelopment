using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{

    int playerHP = 10;
    int aiHP = 10;

    bool playerIsDefending = false;
    // bool aiIsCharging = false;
    bool aiTurn = false;
    bool end = false;

    // Start is called before the first frame update
    void Start()
    {
        ShowMessage();
    }

    // Update is called once per frame
    void Update()
    {
        if(!end) {
            // Player's turn
            if(Input.GetKeyUp(KeyCode.Alpha1)) {
                PlayerAttack();
            } else if(Input.GetKeyUp(KeyCode.Alpha2)) {
                PlayerHeal();
            } else if(Input.GetKeyUp(KeyCode.Alpha3)) {
                PlayerDefend();
            }

            CheckHealth();

            // AI's turn
            if(aiTurn && !end) {
                // if(aiIsCharging && !playerIsDefending) {
                //     playerHP = 0;
                // } else {
                //     int prob = Random.Range(1, 101);
                //     if(prob > 65) {
                //         AiCharge();
                //     } else {
                //         AiAttack();
                //     }
                // }
                int prob = Random.Range(1, 101);
                if(prob > 80) {
                    AiCrit();
                } else if(prob > 50 && prob <= 80 && aiHP <= 5) {
                    AiHeal();
                } else {
                    AiAttack();
                }
                aiTurn = false;
                playerIsDefending = false;
            }
        }
    }

    void ShowMessage() {
        if(!end) Debug.Log("Choose your action: (1) Attack, (2) Heal, (3) Defend");
    }

    void CheckHealth() {
        if(aiHP <= 0) {
            Debug.Log("You defeated the AI!");
            end = true;
        } else if(playerHP <= 0) {
            Debug.Log("You died!");
            end = true;
        }
    }

    void Reset() {
        playerHP = 10;
        aiHP = 10;
        playerIsDefending = false;
        // aiIsCharging = false;
    }

    void DisplayStats() {
        Debug.Log($"Your HP: { playerHP }, AI HP: { aiHP }");
    }

    void PlayerAttack() {
        int attackPoints = Random.Range(1, 3);
        Debug.Log($"You attack and do { attackPoints } damage!");
        aiHP -= attackPoints;
        DisplayStats();
        aiTurn = true;
    }

    void PlayerHeal() {
        if(playerHP < 9) {
            playerHP += 2;
            Debug.Log("You healed yourself!");
            DisplayStats();
        } else {
            Debug.Log("You are already healthy!");
        }
        aiTurn = true;
    }

    void PlayerDefend() {
        playerIsDefending = true;
        Debug.Log("You are now defending the next attack!");
        DisplayStats();
        aiTurn = true;
    }

    void AiAttack() {
        int attackPoints = Random.Range(1, 3);
        if(playerIsDefending) {
            Debug.Log("AI is attacking! You defended the attack! You loose no HP.");
            int rand = Random.Range(1, 101);
            if(rand > 80) {
                Debug.Log($"Uh oh, the AI broke your defense! The AI does { attackPoints } damage!");
                playerHP -= attackPoints;
            }
        } else {
            Debug.Log($"The AI attacks and does { attackPoints } damage!");
            playerHP -= attackPoints;
        }
        DisplayStats();
        ShowMessage();
    }

    void AiHeal() {
        aiHP += 2;
        Debug.Log("The AI healed itself!");
        DisplayStats();
        ShowMessage();
    }

    void AiCrit() {
        int attackPoints = Random.Range(2, 5);
        if(playerIsDefending) {
            Debug.Log("AI is attacking! You defended the attack! You loose no HP.");
            int rand = Random.Range(1, 101);
            if(rand > 50) {
                Debug.Log($"Uh oh, the AI broke your defense! The AI does { attackPoints } damage!");
                playerHP -= attackPoints;
            }
        } else {
            Debug.Log($"The AI attacks and does { attackPoints } damage!");
            playerHP -= attackPoints;
        }
        DisplayStats();
        ShowMessage();
    }

    // void AiCharge() {
    //     Debug.Log("The AI is charging energy!");
    //     aiIsCharging = true;
    //     DisplayStats();
    //     ShowMessage();
    // }
}
