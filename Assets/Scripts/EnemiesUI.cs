using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesUI : MonoBehaviour {
    public Text enemiesRemainText;
    public GameObject portal;

    void Update() {
        enemiesRemainText.GetComponent<Text>().text = portal.GetComponent<PortalBehaviour>().cantEnemies.ToString();
    }
}