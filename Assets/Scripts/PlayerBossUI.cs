using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBossUI : MonoBehaviour {
    public Text playerHealthText;
    public GameObject player;

    void Update() {
        playerHealthText.GetComponent<Text>().text = player.GetComponent<PlayerCombatBoss>().health.ToString();
    }
}
