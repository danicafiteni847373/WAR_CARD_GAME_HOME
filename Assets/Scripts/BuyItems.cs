using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyItems : MonoBehaviour
{
    public int itemCost = 200;
    public TextMeshProUGUI currencyText;
    

    private void Update()
    {
        UpdateCurrencyText();
    }

    private void UpdateCurrencyText()
    {
      
      currencyText.text = "Currency: " + PlayerCurrency.CurrencyAmount.ToString();
      Debug.Log(PlayerCurrency.CurrencyAmount);
        
    }

    public void Buy()
    {
        if (PlayerCurrency.CurrencyAmount >= itemCost)
        {
            // Subtract the cost of the item from the shared currency amount
            PlayerCurrency.CurrencyAmount -= itemCost;

            // Add your logic here to provide the purchased item to the player
            // For example, you can enable a new weapon, unlock a feature, etc.

            // Update the TextMeshProUGUI text displaying currency
            UpdateCurrencyText();
        }
        else
        {
            Debug.Log("Not enough currency to buy the item!");
        }
    }

}
