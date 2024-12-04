using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour {
    public Text bossHealthText;
    public GameObject boss;

    void Update() {
        bossHealthText.GetComponent<Text>().text = boss.GetComponent<BossBehaviour>().health.ToString();
    }
}
