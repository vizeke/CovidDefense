using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFund
{
    private int _amount = 0;
    public int Amount { get => _amount; }

    public PlayerFund(int initialAmount = 0)
    {
        _amount = initialAmount;
    }

    public void Earn(int value)
    {
        _amount += Mathf.Abs(value);
    }

    public bool Spend(int cost)
    {
        cost = Mathf.Abs(cost);

        if (cost > _amount) return false;

        _amount -= cost;

        return true;
    }
}
