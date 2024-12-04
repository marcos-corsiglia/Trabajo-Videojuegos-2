using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireAttack : MonoBehaviour {
    public GameObject player;
    public GameObject fireBoss;
    public int damage = 1; // Cantidad de danno que realizara la pelota.

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            player.GetComponent<PlayerCombatBoss>().health -= damage;
            fireBoss.GetComponent<BossFire>().DestroyFireBoss();
        }
    }
}
