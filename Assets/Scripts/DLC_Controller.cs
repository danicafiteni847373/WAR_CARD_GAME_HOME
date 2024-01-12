using System;
using System.Collections.Generic;
using System.Threading; //because of these we can use an aynchronous task
using System.Threading.Tasks; //because of these we can use an aynchronous task
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine;
using UnityEngine.UI;

public class DLC_Controller : MonoBehaviour
{

    // Initialize Firebase 
    private FirebaseStorage _instance;

    // Transform of the container for all sale items
    [SerializeField] private GameObject saleItems;

    // Prefab for each sale item
    [SerializeField] private GameObject saleItemPrefab;
    // private RawImage _rawImage; //leads to import using UnityEngine.UI;
    void Start()
    {
        //Initialize Firebase
        _instance = FirebaseStorage.DefaultInstance;

        //Get a reference of the raw image component
        // _rawImage = GameObject.Find("DownloadedImage").GetComponent<RawImage>();

        //Download the manifest and any file that i want and read it
        //gs://dlc-cg-shop.appspot.com/manifest.xml the url
        //manifest.xml the filename
        DownloadFileAsync(_instance.GetReferenceFromUrl("gs://war-card-game-13fc1.appspot.com/manifest.xml"), "manifest.xml");

        // DownloadImageAsync(_instance.GetReferenceFromUrl("gs://dlc-system-cg.appspot.com/images/Asset_10@10x.png"));
    }

    // Asynchronously downloads a file to the specified local path
    public void DownloadFileAsync(StorageReference reference, string filename)
    {
        // Create local filesystem URL and won't be deleted when the pc is turned off
        //after build you won't ave acess to assets
        string localFile = Application.persistentDataPath + "/" + filename;
        Debug.Log(localFile);
        // Start downloading a file
        Task task = reference.GetFileAsync(localFile,
            new StorageProgress<DownloadState>(state => {
                // called periodically during the download
                Debug.Log(String.Format(
                    "Progress: {0} of {1} bytes transferred.",
                    state.BytesTransferred,
                    state.TotalByteCount
                ));
            }), CancellationToken.None);

        task.ContinueWithOnMainThread(resultTask => {
            if (!resultTask.IsFaulted && !resultTask.IsCanceled)
            {
                Debug.Log("Download finished.");
                //Read the manifest
                ReadManifest(Application.persistentDataPath + "/manifest.xml");
                Debug.Log("Read Manifest.");
            }
        });
    }

    //save it on the ram and not on the hard drive since it is using a byte array
    // Asynchronously downloads an image to the specified RawImage component
    public void DownloadImageAsync(StorageReference reference, RawImage rawImage)
    {
        // Download in memory with a maximum allowed size of 2MB (2 * 1024 * 1024 bytes)
        const long maxAllowedSize = 2 * 1024 * 1024;
        reference.GetBytesAsync(maxAllowedSize).ContinueWithOnMainThread(task => {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogException(task.Exception);
                // Uh-oh, an error occurred!
            }
            else
            {
                byte[] fileContents = task.Result;

                // Create a new Texture2D object.
                Texture2D texture = new Texture2D(483, 617);
                //loading the image from the texture in the byte array
                texture.LoadImage(fileContents);

                //Displaying the texture on screen
                rawImage.texture = texture;

                Debug.Log("Finished downloading image in memory!");
            }
        });
    }

    // Asynchronously reads the manifest file and downloads all sale items
    private void ReadManifest(string path)
    {
        List<AssetData> assets = new List<AssetData>();
        // Read the manifest file
        assets = AssetDataReader.ReadAssetsFromXml(path, out string imageBaseUrl);
        // Download each sale item
        foreach (AssetData asset in assets)
        {
            GameObject saleItem = Instantiate(saleItemPrefab, new Vector3(0, 0, 0), Quaternion.identity, saleItems.transform);
            Vector3 saleItemPos = saleItem.transform.localPosition;
            saleItem.transform.localPosition = new Vector3(saleItemPos.x, saleItemPos.y, 0);
            // Set the sale item's description
            saleItem.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = asset.Description;
            // Set the sale item's price
            saleItem.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text = $"{asset.Price.Regions[0].CurrencySymbol} {asset.Price.Regions[0].Amount}";
            // Download the sale item's image
            DownloadImageAsync(_instance.GetReferenceFromUrl(imageBaseUrl + asset.Image + ".png"),
                saleItem.transform.GetChild(0).GetComponent<RawImage>());
            // Debug.Log(imageBaseUrl + asset.Image);
            // DownloadImageAsync(_instance.GetReferenceFromUrl(imageBaseUrl + asset.Image + ".png"));
        }
    }


}
/*private FirebaseStorage _instance;
//private RawImage _rawImage;

[SerializeField] private GameObject saleItems;
[SerializeField] private GameObject saleItemPrefab;

void Start()
{
    // Initialize Firebase
    _instance = FirebaseStorage.DefaultInstance;

    //Get a references to the raw image component
    //_rawImage = GameObject.Find("DownloadedImage").GetComponent<RawImage>();

    //Downloads the manifest
    DownloadFileAsync(_instance.GetReferenceFromUrl("gs://dlc-cg-shop.appspot.com/manifest.xml"), "manifest.xml");
}

public void DownloadFileAsync(StorageReference reference, string filename)
{
    // Create local filesystem URL
    string localFile = Application.persistentDataPath + "/" + filename;
    Debug.Log(localFile);
    // Start downloading a file
    Task task = reference.GetFileAsync(localFile,
        new StorageProgress<DownloadState>(state => {
            // called periodically during the download
            Debug.Log(String.Format(
                "Progress: {0} of {1} bytes transferred.",
                state.BytesTransferred,
                state.TotalByteCount
            ));
        }), CancellationToken.None);

    task.ContinueWithOnMainThread(resultTask => {
        if (!resultTask.IsFaulted && !resultTask.IsCanceled)
        {
            Debug.Log("Download finished.");
            //Read the manifest
            ReadManifest(Application.persistentDataPath + "/manifest.xml");
        }
    });
}

public void DownloadImageAsync(StorageReference reference, RawImage rawImage)
{
    // Download in memory with a maximum allowed size of 2MB (2 * 1024 * 1024 bytes)
    const long maxAllowedSize = 2 * 1024 * 1024;
    reference.GetBytesAsync(maxAllowedSize).ContinueWithOnMainThread(task => {
        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.LogException(task.Exception);
            // Uh-oh, an error occurred!
        }
        else
        {
            byte[] fileContents = task.Result;

            // Create a new Texture2D object.
            Texture2D texture = new Texture2D(483, 617);
            texture.LoadImage(fileContents);

            //Displaying the texture on screen
            //_rawImage.texture = texture;

            Debug.Log("Finished downloading image in memory!");
        }
    });
}

private void ReadManifest(string path)
{
    List<AssetData> assets = new List<AssetData>();
    assets = AssetDataReader.ReadAssetsFromXml(path, out string imageBaseUrl);
    foreach (AssetData asset in assets)
    {
        //Debug.Log(imageBaseUrl + asset.Image);
        //DownloadImageAsync(_instance.GetReferenceFromUrl(imageBaseUrl + asset.Image + ".png"))
        GameObject saleItem = Instantiate(saleItemPrefab, new Vector3(0, 0, 0), Quaternion.identity,saleItems.transform);

        //Setting the instanted prefab's z position to 0;
        Vector3 salesItemPos = saleItems.transform.localPosition;
        saleItem.transform.localPosition = new Vector3(salesItemPos.x, salesItemPos.y, 0);
        saleItem.transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = asset.Description;
        saleItems.transform.GetChild(2).GetComponent<TMPro.TMP_Text>().text =
            $"{asset.Price.Regions[0].CurrencySymbol} {asset.Price.Regions[0].Amount}";
        DownloadImageAsync(_instance.GetReferenceFromUrl(imageBaseUrl + asset.Image + ".png"),
            saleItem.transform.GetChild(0).GetComponent<RawImage>());
    }
}
}


void Start()
{
DownloadFile();
}

public void DownloadFile()
{
// Create a reference to the file you want to download.
StorageReference storageReference = FirebaseStorage.DefaultInstance.GetReferenceFromUrl("gs://dlc-cg-shop.appspot.com/manifest.xml");

// Create local filesystem URL
//persistent, the file won't be deleted when the pc shuts down
string localUrl = Application.persistentDataPath + "/manifest.xml";
Debug.Log("Downloading to: " + localUrl);

// Start downloading a file
Task task = storageReference.GetFileAsync(localUrl,
    new StorageProgress<DownloadState>(state => {
        // called periodically during the download
        Debug.Log(String.Format(
            "Progress: {0} of {1} bytes transferred.",
            state.BytesTransferred,
            state.TotalByteCount
        ));
    }), CancellationToken.None);

task.ContinueWithOnMainThread(resultTask => {
    if (!resultTask.IsFaulted && !resultTask.IsCanceled)
    {
        Debug.Log("Download finished.");
    }
});

//synchronous progrogram blocking
//asynchronous program non blocking
}*/






