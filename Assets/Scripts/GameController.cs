using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject prefabWithScript;

    private void Start()
    {
        // Instantiate the prefab with the script
        GameObject instantiatedPrefab = Instantiate(prefabWithScript);

        // You can also access the script directly and call methods if needed
        BuyItems buyItemScript = instantiatedPrefab.GetComponent<BuyItems>();
        if (buyItemScript != null)
        {
            // Call any methods if needed
            buyItemScript.Buy();
        }
    }
}
