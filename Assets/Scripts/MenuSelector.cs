using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelector : MonoBehaviour {
    public static List<List<String>> floor = new List<List<String>>();
    public static List<String> roomsLevel1 = new List<String>();
    public static List<String> roomsLevel2 = new List<String>();
    public static List<String> roomsLevel3 = new List<String>();

    void Start() {
        roomsLevel1.Add("Room 1-1");
        roomsLevel1.Add("Room 1-2");
        roomsLevel1.Add("Room 1-3");

        roomsLevel2.Add("Room 2-1");
        roomsLevel2.Add("Room 2-2");
        roomsLevel2.Add("Room 2-3");

        roomsLevel3.Add("Room 3-1");
        roomsLevel3.Add("Room 3-2");
        roomsLevel3.Add("Room 3-3");

        floor.Add(roomsLevel1);
        floor.Add(roomsLevel2);
        floor.Add(roomsLevel3);

        Debug.Log("Start");
    }

    public void StartGame() {
        Debug.Log("Se ejecuto StartGame");

        int randomNumber = UnityEngine.Random.Range(0, 3);

        switch (randomNumber) {
            case 0:
                //PortalBehaviour.playedRoom[0] = false;
                SceneManager.LoadScene(floor[0][0]);
                floor[0].Remove(floor[0][0]);
                break;

            case 1:
                //PortalBehaviour.playedRoom[1] = false;
                SceneManager.LoadScene(floor[0][1]);
                floor[0].Remove(floor[0][1]);
                break;

            case 2:
                //PortalBehaviour.playedRoom[2] = false;
                SceneManager.LoadScene(floor[0][2]);
                floor[0].Remove(floor[0][2]);
                break;
        }
    }

    public void ExitGame() {
        Application.Quit();
    }
}
