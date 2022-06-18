using UnityEngine;

public static class Helper 
{
    public static bool SCENE_IS_LOADING = false;
    public const string P_ANIM_MOVEMENT = "magnitude";
    public const string P_ANIM_DEAD = "isDead";
    public const string P_ANIM_WIN = "isWin";
    public const string P_ANIM_EQUIP = "equipGun";
    public const string P_ANIM_FIRE = "fire";

    public const int MAX_GEM_OBJECTS = 10;
    public const int MAX_ENEMY_OBJECTS = 5;
    public const int POINTS_TO_WIN = 100;
    public const int GAME_DURATION = 10;

    public const int FRAME_RATE = 30;
    public static int CURRENT_SCORE = 0;

    public static void SaveHighScore(int _s) 
    {
        int saved_scr = PlayerPrefs.GetInt("_hs_", 0);
        if(_s > saved_scr) {
            PlayerPrefs.SetInt("_hs_", _s);
            PlayerPrefs.Save();
        } 
    }

    public static int GetHighScore() 
    {
        return  PlayerPrefs.GetInt("_hs_", 0);
    }
}