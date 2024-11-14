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

    public float GetDifficultyLevel()
    {
        return PlayerPrefs.GetFloat(DIFFICULTY_SAVE_KEY, 1);
    }

    public void SetDifficulty(float difficultyLevel)
    {
        PlayerPrefs.SetFloat(DIFFICULTY_SAVE_KEY, difficultyLevel);
        PlayerPrefs.Save();
    }
}
