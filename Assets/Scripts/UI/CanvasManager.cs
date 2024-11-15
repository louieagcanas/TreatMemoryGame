using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject gameHUD;

    [SerializeField]
    private GameObject gameBoard;

    [SerializeField]
    private GameObject popupOverlay;

    private void Start()
    {
        mainMenu.SetActive(true);
        gameHUD.SetActive(false);
        gameBoard.SetActive(false);
    }

    public void ChangeToGameMode()
    {
        mainMenu.SetActive(false);
        gameHUD.SetActive(true);
        gameBoard.SetActive(true);
    }
}
