// using directives that include namespaces necessary for the code to work with various standard libraries and data structures.
using System;
using System.Collections.Generic;

// The AssetData class represents a single asset in the game.
[Serializable]
public class AssetData
{
    // The unique identifier of the asset.
    public int Id { get; set; }

    // A short description of the asset.
    public string Description { get; set; }

    // The URL to the asset's image.
    public string Image { get; set; }

    // The price of the asset in different regions.
    public Price Price { get; set; }

    // Constructor for the AssetData class.
    public AssetData(int id, string description, string image, Price price)
    {
        Id = id;
        Description = description;
        Image = image;
        Price = price;
    }
}

// The Price class represents the price of an asset in different regions.
public class Price
{
    // A list of all the regions where the asset is available.
    public List<RegionPrice> Regions { get; set; }

    // Constructor for the Price class.
    public Price(List<RegionPrice> regions)
    {
        Regions = regions;
    }
}

// The RegionPrice class represents the price of an asset in a single region.
public class RegionPrice
{
    // The name of the region.
    public string Name { get; set; }

    // The currency used in the region.
    public string Currency { get; set; }

    // The currency symbol used in the region.
    public string CurrencySymbol { get; set; }

    // The amount of the asset in the region's currency.
    public decimal Amount { get; set; }

    // Constructor for the RegionPrice class.
    public RegionPrice(string name, string currency, string currencySymbol, decimal amount)
    {
        Name = name;
        Currency = currency;
        CurrencySymbol = currencySymbol;
        Amount = amount;
    }
}
