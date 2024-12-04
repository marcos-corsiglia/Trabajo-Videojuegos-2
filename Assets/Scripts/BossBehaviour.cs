using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehaviour : MonoBehaviour {
    // Variables del boss.
    public int health;
    public AudioClip damagedScreamAudioClip;
    
    // Variables para la creacion de la fire ball.
    public GameObject fireBallPrefab;
    public GameObject fireShootPoint;
    public GameObject player;
    public GameObject boss;
    private GameObject fireBall;

    public bool fireBallAlive; // Esta variable hace referencia a si hay una pelota activa en el escenario. Su estado cambia dentro del script BossFire.
    private AudioSource damagedScreamAudioSource;

    void Start() {
        //Agrego componentes y clips de audio al AudioSource.
        damagedScreamAudioSource = gameObject.AddComponent<AudioSource>();
        damagedScreamAudioSource.clip = damagedScreamAudioClip;
    }
    void Update() {
        if (!fireBallAlive) {
            PauseExecution(); // TODO: Tal vez ShootFireBall debe ir dentro de esta funcion?????????????
            ShootFireBall();
        }
        
        checkIfDead();
    }

    private void ShootFireBall() { // TODO: Debe crear el elemento de la pelota
        // Creacion de la bola de fuego.
        fireBall = Instantiate(fireBallPrefab, fireShootPoint.transform.position, Quaternion.identity);

        // Asignacion de variables.
        fireBall.GetComponent<BossFire>().player = player;
        fireBall.GetComponent<BossFire>().boss = boss;

        fireBall.transform.Find("AttackPoint").gameObject.GetComponent<BossFireAttack>().player = player;
        fireBall.transform.Find("AttackPoint").gameObject.GetComponent<BossFireAttack>().fireBoss = fireBall;
    }

    public void TakeDamage(int damage) { 
        health -= damage;
        damagedScreamAudioSource.Play();
    }

    private void checkIfDead() {
        if (health < 1) {
            Destroy(gameObject);
            SceneManager.LoadScene("Main Menu");
        }
    }

    IEnumerator PauseExecution() {
        UnityEngine.Debug.Log("Inicio de la pausa.");
        yield return new WaitForSeconds(5);
        UnityEngine.Debug.Log("Fin de la pausa.");
    }
}