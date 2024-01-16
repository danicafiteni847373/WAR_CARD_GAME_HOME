using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyItems : MonoBehaviour
{
    public int itemCost = 200;
    public TextMeshProUGUI currencyText;
    public PlayingCardsSO playingCardsSO;
    public GameManager gameManager;

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

            // Save the updated currency amount to PlayerPrefs
            PlayerPrefs.SetInt("CurrencyAmount", PlayerCurrency.CurrencyAmount);
            PlayerPrefs.Save();

            if (playingCardsSO.Backgrounds.Count > 0)
            {
                playingCardsSO.ActiveBackground = playingCardsSO.Backgrounds[0];
                Debug.Log("Change Background");
            }

            // Update the TextMeshProUGUI text displaying currency
            UpdateCurrencyText();

            // Notify the GameManager to update the background display
            GameManager.Singleton.UpdateBackgroundDisplay();
        }
        else
        {
            Debug.Log("Not enough currency to buy the item!");
        }
    }

}
