using System;

[Serializable]
public class GameData
{
    public bool hasSavedGame;
    public int hiScore;
    public string recordHolderName;
    public int columns;
    public int rows;
    public int turn;
    public int score;
    public int bonus;
    public int comboCount;
    public int cardLeft;
    public int lastMatchTurn;
    public int previousCardColumn;
    public int previousCardRow;
}
