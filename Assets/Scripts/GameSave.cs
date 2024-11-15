using UnityEngine;

public class GameSave : MonoBehaviour
{
    private const string NAME_SAVE_KEY = "playerName";
    private const string DIFFICULTY_SAVE_KEY = "difficultyLevel";

    public string GetPlayerName()
    {
        return PlayerPrefs.GetString(NAME_SAVE_KEY, "");
    }

    public void SetPlayerName(string name)
    {
        PlayerPrefs.SetString(NAME_SAVE_KEY, name);
        PlayerPrefs.Save();
    }

    public int GetDifficultyLevel()
    {
        return PlayerPrefs.GetInt(DIFFICULTY_SAVE_KEY, 1);
    }

    public void SetDifficulty(float difficultyLevel)
    {
        PlayerPrefs.SetInt(DIFFICULTY_SAVE_KEY, (int)difficultyLevel);
        PlayerPrefs.Save();
    }
}
