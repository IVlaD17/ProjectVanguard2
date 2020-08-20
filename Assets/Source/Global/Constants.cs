using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public static string GAME_MANAGER = "GameManager";
    public static string SESS_MANAGER = "SessionManager";

    public static string LOADING_SCENE = "LoadingScene";
    public static string MENU_SCENE = "MenuScene";
    public static string GAME_SCENE = "GameScene";

    public static string PAWN_DATA_PATH = "";
    public static string ROOK_DATA_PATH = "";
    public static string KING_DATA_PATH = "";
    public static string QUEEN_DATA_PATH = "";
    public static string KNIGHT_DATA_PATH = "";
    public static string BISHOP_DATA_PATH = "";

    public static string NO_GAME_MANAGER_FOUND = "ERROR!!! Failed to find Game Object named " + GAME_MANAGER;
    public static string NO_SESS_MANAGER_FOUND = "ERROR!!! Failed to find Game Object named " + SESS_MANAGER;
    public static string NO_PLAYER_OBJECT_FOUND = "ERROR!!! Failed to find Game Object named ";

    public static int NUMBER_OF_ROWS = 8;
    public static int NUMBER_OF_COLS = 8;

    public static int SQUARE_SIZE = 20;

    public static int NUMBER_OF_PLAYERS = 2;
    public static int NUMBER_OF_PIECES = 16;

    public static Vector3 LOCAL_CAM_POS = new Vector3(0, 0.5f, 0);
    public static Vector3 LOCAL_CAM_ROT_W = new Vector3(0, 0, 0);
    public static Vector3 LOCAL_CAM_ROT_B = new Vector3(0, -180, 0);

    public static Vector3 TOP_CAM_POS_W = new Vector3(0, 3, 5.5f);
    public static Vector3 TOP_CAM_ROT_W = new Vector3(90, 0, 0);
    public static Vector3 TOP_CAM_POS_B = new Vector3(0, 3, -5.5f);
    public static Vector3 TOP_CAM_ROT_B = new Vector3(90, -180, 0);

    public static float MAP_MIN_X = -15f;
    public static float MAP_MAX_X = 15f;
    public static float MAP_MIN_Y = -15f;
    public static float MAP_MAX_Y = 15f;

    public static Color32 COLOR_BLACK = new Color32(0, 0, 0, 255);
    public static Color32 COLOR_WHITE = new Color32(255, 255, 255, 255);
    public static Color32 COLOR_YELLOW = new Color32(250, 185, 0, 255);
    public static Color32 COLOR_L_BROWN = new Color32(135, 120, 110, 255);
    public static Color32 COLOR_D_BROWN = new Color32(55, 25, 0, 255);
}
