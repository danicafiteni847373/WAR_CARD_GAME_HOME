using System.Collections.Generic;
using System.Xml.Linq;

public static class AssetDataReader
{
    // Reads a list of AssetData objects from an XML manifest file
    public static List<AssetData> ReadAssetsFromXml(string xmlPath, out string baseUrl)
    {
        // Create a list to store the parsed assets
        List<AssetData> assets = new List<AssetData>();

        // Initialize the base URL
        baseUrl = string.Empty;

        // Load the XML document
        XDocument doc = XDocument.Load(xmlPath);

        // Get the base URL from the XML document
        baseUrl = doc.Root.Element("baseUrl").Value;

        // Iterate over all asset elements in the XML document
        foreach (XElement assetElement in doc.Root.Element("assets").Elements("asset"))
        {
            // Parse the asset ID
            int id = int.Parse(assetElement.Element("itemId").Value);

            // Parse the asset description
            string description = assetElement.Element("itemDescription").Value;

            // Get the price element for the asset
            var priceElement = assetElement.Element("itemPrice");

            // Parse the asset image URL
            string image = assetElement.Element("backgroundImageUrl").Value;

            string imagePreview = assetElement.Element("previewImageURL").Value;

            // Create a list to store the parsed region prices
            List<RegionPrice> regions = new List<RegionPrice>();

            // Iterate over all region elements in the price element
            foreach (XElement regionElement in priceElement.Elements("region"))
            {
                // Parse the region name
                string regionName = regionElement.Element("name").Value;

                // Parse the region currency
                string regionCurrency = regionElement.Element("currency").Value;

                // Parse the region currency symbol
                string regionCurrencySymbol = regionElement.Element("currencySymbol").Value;

                // Parse the region amount
                decimal regionAmount = decimal.Parse(regionElement.Element("amount").Value);

                // Add the region price to the list
                regions.Add(new RegionPrice(regionName, regionCurrency, regionCurrencySymbol, regionAmount));
            }

            // Create a new Price object for the asset
            Price price = new Price(regions);

            // Add the new AssetData object to the list
            assets.Add(new AssetData(id, description, image, price, imagePreview));
        }

        // Return the list of parsed assets
        return assets;
    }
}
