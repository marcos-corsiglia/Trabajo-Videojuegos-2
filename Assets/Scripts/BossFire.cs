using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossFire : MonoBehaviour {
    public GameObject player;
    public GameObject boss;

    public int health = 3; // Cantidad de veces que el jugador debe golpear la pelota para que que impacte y haga danno al jefe.
    public float speed = 8; // Velocidad a la que se desplazara la pelota.

    private float distance;
    private bool attackPlayer = true; // Si esta variable es true, el fuego se dirige al jugador. Si es false se dirige al jefe.
    private Vector3 lastPosition;

    void Start() {
        lastPosition = transform.position;
        boss.GetComponent<BossBehaviour>().fireBallAlive = true;
    }

    void Update() {
        Movement();
        CheckIfMoving();
    }

    private void Movement() {
        if (attackPlayer) { // En caso que se deba atacar al jugador.
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;

            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else { // En caso que se deba atacar al jefe.
            distance = Vector2.Distance(transform.position, boss.transform.position);
            Vector2 direction = boss.transform.position - transform.position;

            transform.position = Vector2.MoveTowards(this.transform.position, boss.transform.position, speed * Time.deltaTime);
        }
    }

    private void CheckIfMoving() {
        if (transform.position == lastPosition) {
            Debug.Log("El objeto está quieto");
            TakeDamage(0);
        }
        else {
            lastPosition = transform.position;
        }
    }

    public void TakeDamage(int damage) { // El script tanto del boss como del jugador deberan llamar a esta funcion para hacer danno a la pelota y cambiar de objetivo.
        if (attackPlayer) { // En caso de que la pelota sea golpeada por el jugador.
            health -= damage;
            attackPlayer = !attackPlayer;
        }
        else if (!attackPlayer && health < 1) { // En caso de que la pelota se dirija al jefe y este deba ser impactado por ella. 
            boss.GetComponent<BossBehaviour>().TakeDamage(GameObject.Find("AttackPoint").gameObject.GetComponent<BossFireAttack>().damage);
            DestroyFireBoss();
        }
        else { // En caso de que el jefe deba devolver la pelota.
            attackPlayer = !attackPlayer;
        }
    }

    public void DestroyFireBoss() {
        boss.GetComponent<BossBehaviour>().fireBallAlive = false;
        Destroy(gameObject);
    }
}
