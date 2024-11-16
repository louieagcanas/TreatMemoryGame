using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private const string NAME_SAVE_KEY = "playerName";
    private const string DIFFICULTY_SAVE_KEY = "difficultyLevel";

    public static string GetPlayerName()
    {
        return PlayerPrefs.GetString(NAME_SAVE_KEY, "");
    }

    public static void SetPlayerName(string name)
    {
        PlayerPrefs.SetString(NAME_SAVE_KEY, name);
        PlayerPrefs.Save();
    }

    public static int GetDifficultyLevel()
    {
        return PlayerPrefs.GetInt(DIFFICULTY_SAVE_KEY, 1);
    }

    public static void SetDifficultyLevel(float difficultyLevel)
    {
        PlayerPrefs.SetInt(DIFFICULTY_SAVE_KEY, (int)difficultyLevel);
        PlayerPrefs.Save();
    }
}
