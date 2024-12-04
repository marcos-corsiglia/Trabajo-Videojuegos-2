using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSelector : MonoBehaviour {
    void Start() {
        // Elimino los niveles de la lista
        for (int i=0; i<3; i++)
            MenuSelector.floor[i].Clear();

        // Agrego nuevamente todos los niveles del juego
        MenuSelector.roomsLevel1.Add("Room 1-1");
        MenuSelector.roomsLevel1.Add("Room 1-2");
        MenuSelector.roomsLevel1.Add("Room 1-3");

        MenuSelector.roomsLevel2.Add("Room 2-1");
        MenuSelector.roomsLevel2.Add("Room 2-2");
        MenuSelector.roomsLevel2.Add("Room 2-3");

        MenuSelector.roomsLevel3.Add("Room 3-1");
        MenuSelector.roomsLevel3.Add("Room 3-2");
        MenuSelector.roomsLevel3.Add("Room 3-3");
    }

    public void StartGame()
    {
        Debug.Log("Se ejecuto StartGame");

        int randomNumber = UnityEngine.Random.Range(0, 3);

        switch (randomNumber)
        {
            case 0:
                SceneManager.LoadScene(MenuSelector.floor[0][0]);
                MenuSelector.floor[0].Remove(MenuSelector.floor[0][0]);
                break;

            case 1:
                SceneManager.LoadScene(MenuSelector.floor[0][1]);
                MenuSelector.floor[0].Remove(MenuSelector.floor[0][1]);
                break;

            case 2:
                SceneManager.LoadScene(MenuSelector.floor[0][2]);
                MenuSelector.floor[0].Remove(MenuSelector.floor[0][2]);
                break;
        }
    }

    public void ExitGame() {
        Application.Quit();
    }
}