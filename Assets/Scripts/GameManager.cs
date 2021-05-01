using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int infectionPercentage = 20;
    private PlayerFund playerFund = new PlayerFund(100);


    #region [ Health ]
    public int GetInfection()
    {
        return infectionPercentage;
    }

    public int ApplyInfectionDelta(int delta)
    {
        infectionPercentage += delta;
        return infectionPercentage;
    }
    #endregion

    #region [ Funds ]
    public int GetFund()
    {
        return playerFund.Amount;
    }

    public bool Spend(int value)
    {
        return playerFund.Spend(value);
    }

    public void Earn(int value)
    {
        playerFund.Earn(value);
    }
    #endregion
}
