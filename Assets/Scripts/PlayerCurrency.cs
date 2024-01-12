using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    public static int currencyAmount = 1000;  // Shared currency amount

    public static int CurrencyAmount
    {
        get { return currencyAmount; }
        set { currencyAmount = value; }
    }
}