using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalBehaviour : MonoBehaviour {
    public int cantEnemies;
    private static int level = 1;
    public static bool[] playedRoom = {true, true, true}; // Cada index del array representa una sala del nivel segun su orden. [0] es la sala 1,
                                                          // [1] es la sala 2 y [2] es la sala 3. Si el valor es true significa que el jugador puede acceder
                                                          // mediante el portal, en caso contrario es false.

    private bool portalEnabled = false;

    void Start() {
        Debug.Log(playedRoom[0] + " " + playedRoom[1] + " " + playedRoom[2]);
    }

    void Update() {
        if (cantEnemies < 1) { 
            TurnOnPortal();
        }
    }

    private void TurnOnPortal() {
        portalEnabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other) { 
        if (portalEnabled)
            loadRandomRoom();
    }

    private void loadRandomRoom() {
        if (playedRoom[0] == false && playedRoom[1] == false && playedRoom[2] == false) {
            if (level == 3) {
                SceneManager.LoadScene("Main Menu");
                return;
            }

            for (int i = 0; i < 3; i++) {
                PortalBehaviour.playedRoom[i] = true;
            }

            level++;
        }

        while (true) {
            int randomNumber = UnityEngine.Random.Range(1, 4);

            switch (randomNumber) {
                case 1:
                    if (playedRoom[0] == true) {
                        Debug.Log("Cargando Room " + level + "-1");
                        PortalBehaviour.playedRoom[0] = false;
                        LoadRoomScene(1);
                        return;
                    }
                    break;

                case 2:
                    if (playedRoom[1] == true) {
                        Debug.Log("Cargando Room " + level + "-2");
                        PortalBehaviour.playedRoom[1] = false;
                        LoadRoomScene(2);
                        return;
                    }
                    break;

                case 3:
                    if (playedRoom[2] == true) {
                        Debug.Log("Cargando Room " + level + "-3");
                        PortalBehaviour.playedRoom[2] = false;
                        LoadRoomScene(3);
                        return;
                    }
                    break;
            }
        }
    }

    private void LoadRoomScene(int room) {
        if (level == 1) { // Nivel 1
            if (room == 1)
                SceneManager.LoadScene("Room 1-1");
            else if (room == 2)
                SceneManager.LoadScene("Room 1-2");
            else if (room == 3)
                SceneManager.LoadScene("Room 1-3");
        }
        else if (level == 2) { // Nivel 2
            if (room == 1)
                SceneManager.LoadScene("Room 2-1");
            else if (room == 2)
                SceneManager.LoadScene("Room 2-2");
            else if (room == 3)
                SceneManager.LoadScene("Room 2-3");
        }
        else if (level == 3) { // Nivel 3
            if (room == 1)
                SceneManager.LoadScene("Room 3-1");
            else if (room == 2)
                SceneManager.LoadScene("Room 3-2");
            else if (room == 3)
                SceneManager.LoadScene("Room 3-3");
        }
    }
}
