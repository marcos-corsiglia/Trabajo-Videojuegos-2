using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelector : MonoBehaviour {
    void Start() {
        Debug.Log("Start");
    }
        
    void Update() {
        
    }

    public void StartGame() {
        Debug.Log("Se ejecuto StartGame");

        int randomNumber = UnityEngine.Random.Range(1, 4);

        switch (randomNumber) {
            case 1:
                PortalBehaviour.playedRoom[0] = false;
                SceneManager.LoadScene("Room 1-1");
                break;

            case 2:
                PortalBehaviour.playedRoom[1] = false;
                SceneManager.LoadScene("Room 1-2");
                break;

            case 3:
                PortalBehaviour.playedRoom[2] = false;
                SceneManager.LoadScene("Room 1-3");
                break;
        }
    }

    public void ExitGame() {
        Application.Quit();
    }
}
