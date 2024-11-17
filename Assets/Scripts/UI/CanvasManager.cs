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
    private GameObject resultScreen;

    private void Start()
    {
        SwitchToMainMenu();
    }

    public void SwitchToMainMenu()
    {
        mainMenu.SetActive(true);
        gameHUD.SetActive(false);
        gameBoard.SetActive(false);
        resultScreen.SetActive(false);
    }

    public void SwitchToGameMode()
    {
        mainMenu.SetActive(false);
        gameHUD.SetActive(true);
        gameBoard.SetActive(true);
        resultScreen.SetActive(false);
    }
}
