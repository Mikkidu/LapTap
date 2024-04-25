using System;

[Serializable]
public class GameData
{
    public bool hasSavedGame;
    public int hiScore;
    public string recordHolderName;
    public int turn;
    public int score;
    public int bonus;
    public int comboCount;
    public int cardLeft;
    public int lastMatchTurn;
    public int lastCardColumn;
    public int lastCardRow;
}
