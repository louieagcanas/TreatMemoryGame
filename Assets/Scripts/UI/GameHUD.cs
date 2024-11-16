using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moveCounter;

    public void UpdateMoveCounter(int moves)
    {
        moveCounter.text = moves.ToString();
    }

    public void Restart()
    {

    }

    public void ReturnToMainMenu()
    {

    }
}
