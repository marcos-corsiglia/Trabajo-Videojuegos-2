using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    public Text playerLifeText;
    public GameObject player;

    void Update() {
        playerLifeText.GetComponent<Text>().text = player.GetComponent<PlayerCombat>().health.ToString();
    }
}