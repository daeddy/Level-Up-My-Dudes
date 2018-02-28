using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private const string typeName = "UniqueGameName";
    private const string gameName = "RoomName";

    public GameObject playerPrefab;

    private void SpawnPlayer()
    {
        Network.Instantiate(playerPrefab, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
    }

    private void StartServer()
    {
        Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
        MasterServer.RegisterHost(typeName, gameName);
    }

    void OnServerInitialized()
    {
        Debug.Log("Server Initializied");
        SpawnPlayer();
        //MasterServer.ipAddress = “127.0.0.1″;
    }

    private static int BUTTON_WIDTH = 200;
    private static int BUTTON_HEIGHT = 100;

    private HostData[] hostList;

    private void RefreshHostList()
    {
        MasterServer.RequestHostList(typeName);
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.HostListReceived)
            hostList = MasterServer.PollHostList();
    }

    private void JoinServer(HostData hostData)
    {
        Network.Connect(hostData);
    }

    void OnConnectedToServer()
    {
        Debug.Log("Server Joined");
        SpawnPlayer();
    }

    void OnGUI()
    {
        if (!Network.isClient && !Network.isServer)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - BUTTON_WIDTH / 2, Screen.height / 2 - BUTTON_HEIGHT * 2, 200, 100), "Start Server"))
                StartServer();

            if (GUI.Button(new Rect(Screen.width / 2 - BUTTON_WIDTH / 2, Screen.height / 2 - BUTTON_HEIGHT, 200, 100), "Refresh Hosts"))
                RefreshHostList();

            if (hostList != null)
            {
                for (int i = 0; i < hostList.Length; i++)
                {
                    if (GUI.Button(new Rect(
                        Screen.width / 2 - BUTTON_WIDTH / 2,
                        Screen.height / 2 + BUTTON_HEIGHT * i,
                        BUTTON_WIDTH,
                        BUTTON_HEIGHT), hostList[i].gameName))
                    {
                        JoinServer(hostList[i]);
                        Debug.Log("Joined server " + hostList[i].connectedPlayers);
                    }
                }
            }
        }
    }
}