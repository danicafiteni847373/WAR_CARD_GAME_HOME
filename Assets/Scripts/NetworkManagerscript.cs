using Unity.Netcode;
using UnityEngine;

public class NetworkManagerScript : NetworkBehaviour
{
    private void Start()
    {
        // Register event handlers
        NetworkManager.Singleton.OnClientStopped += HandleClientStopped;
        NetworkManager.Singleton.OnServerStopped += HandleServerStopped;
    }

    private void OnDestroy()
    {
        // Unregister event handlers when the script is destroyed
        NetworkManager.Singleton.OnClientStopped -= HandleClientStopped;
        NetworkManager.Singleton.OnServerStopped -= HandleServerStopped;
    }

    // Other methods and event handlers go here...

    private void HandleClientStopped(bool isClient)
    {
        Debug.Log("Client stopped.");
        // Additional client-specific cleanup or logic here
    }

    private void HandleServerStopped(bool isHost)
    {
        Debug.Log($"Server stopped. Was host: {isHost}");
        // Additional server-specific cleanup or logic here
    }

    public void StartServerButton()
    {
        if (!NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.StartServer();
            Debug.Log("Server started successfully.");
        }
        else
        {
            Debug.LogWarning("Already running as a server, client, or hosting.");
        }
    }

    public void ConnectToServerButton()
    {
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsHost && !NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.StartClient();
            Debug.Log("Connecting to server...");
        }
        else
        {
            Debug.LogWarning("Already connected as a client, hosting, or running as a server.");
        }
    }

    public void StartHostButton()
    {
        if (!NetworkManager.Singleton.IsHost && !NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient)
        {
            NetworkManager.Singleton.StartHost();
            Debug.Log("Host started successfully.");
        }
        else
        {
            Debug.LogWarning("Already hosting, running as a server, or connected as a client.");
        }
    }

    public void StopHostButton()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.Shutdown();
            Debug.Log("Stopping hosting...");
        }
        else if (NetworkManager.Singleton.IsClient)
        {
            NetworkManager.Singleton.Shutdown();
            Debug.Log("Stopping client...");
        }
        else
        {
            Debug.LogWarning("Not currently hosting or connected as a client.");
        }
    }
}
