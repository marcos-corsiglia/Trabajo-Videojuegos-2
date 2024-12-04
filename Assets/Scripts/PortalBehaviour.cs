using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalBehaviour : MonoBehaviour {
    public int cantEnemies;
    private static int level = 0; // 0 = level 1, 1 = level 2, 2 = level 3.
    //public static bool[] playedRoom = {true, true, true}; // Cada index del array representa una sala del nivel segun su orden. [0] es la sala 1,
                                                            // [1] es la sala 2 y [2] es la sala 3. Si el valor es true significa que el jugador puede acceder
                                                            // mediante el portal, en caso contrario es false.

    private bool portalEnabled = false;

    private AudioSource enemyDeathAudioSource;
    public AudioClip enemyDeathAudioClip;

    void Start() {
        //Agrego componentes y clips de audio a los audio source
        enemyDeathAudioSource = gameObject.AddComponent<AudioSource>();
        enemyDeathAudioSource.clip = enemyDeathAudioClip;
    }

    void Update() {
        if (cantEnemies < 1) { 
            TurnOnPortal();
        }
    }

    public void PlayDeathSound() {
        enemyDeathAudioSource.Play();
    }

    private void TurnOnPortal() {
        portalEnabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other) { 
        if (portalEnabled)
            loadRandomRoom();
    }

    private void loadRandomRoom() {
        if (MenuSelector.floor[level].Count == 0) {
            if (level == 1) {
                SceneManager.LoadScene("Room 3-1");
                return;
            }
            level++;
        }

        while (true) {
            int randomNumber = UnityEngine.Random.Range(0, MenuSelector.floor[level].Count);

            switch (randomNumber) {
                case 0:
                    //if (playedRoom[0] == true) {
                        Debug.Log("Cargando Room " + level + "-1");
                    //PortalBehaviour.playedRoom[0] = false;
                    SceneManager.LoadScene(MenuSelector.floor[level][0]);
                    MenuSelector.floor[level].Remove(MenuSelector.floor[level][0]);
                    return;
                    //}
                    break;

                case 1:
                    Debug.Log("Cargando Room " + level + "-2");
                    SceneManager.LoadScene(MenuSelector.floor[level][1]);
                    MenuSelector.floor[level].Remove(MenuSelector.floor[level][1]);
                    return;

                    break;

                case 2:
                    Debug.Log("Cargando Room " + level + "-3");
                    SceneManager.LoadScene(MenuSelector.floor[level][2]);
                    MenuSelector.floor[level].Remove(MenuSelector.floor[level][2]);
                    return;

                    break;
            }
        }
    }

    
}
