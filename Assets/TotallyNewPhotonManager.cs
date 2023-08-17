using Common;
using Oculus.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.IO.Compression;
using UnityEngine;
using PhotonPun = Photon.Pun;
using PhotonRealtime = Photon.Realtime;

public class TotallyNewPhotonManager : PhotonPun.MonoBehaviourPunCallbacks
{
    int roomCount = 0;
    [SerializeField] private bool usePhotonMatchmaking = true;
    private const char Separator = ',';
    private const string UserIdsKey = "userids";
    private readonly HashSet<string> _usernameList = new HashSet<string>();
    private ulong _oculusUserId;
    public string _oculusUsername;
    private List<GameObject> lobbyRowList = new List<GameObject>();


    // Start is called before the first frame update
    private void Start()
    {
        PhotonPun.PhotonNetwork.ConnectUsingSettings();

        Debug.Log("System version: " + OVRPlugin.version);

        Core.Initialize();
        Users.GetLoggedInUser().OnComplete(GetLoggedInUserCallback);


        var offset = 1;
       

        //initialize count
        roomCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetLoggedInUserCallback(Message msg)
    {
        if (msg.IsError)
        {
            Debug.Log("GetLoggedInUserCallback: failed with error: " + msg.GetError());
            return;
        }

        Debug.Log("GetLoggedInUserCallback: success with message: " + msg + " type: " + msg.Type);

        var isLoggedInUserMessage = msg.Type == Message.MessageType.User_GetLoggedInUser;

        if (!isLoggedInUserMessage)
        {
            return;
        }

        _oculusUsername = msg.GetUser().OculusID;
        _oculusUserId = msg.GetUser().ID;

        Debug.Log("GetLoggedInUserCallback: oculus user name: " + _oculusUsername + " oculus id: " +
                                      _oculusUserId);

        if (_oculusUserId == 0)
            Debug.Log(
                "You are not authenticated to use this app. Shared Spatial Anchors will not work.");

        PhotonPun.PhotonNetwork.LocalPlayer.NickName = _oculusUsername;
    }
    public void OnCreateRoomButtonPressed()
    {
        //Debug.Log("OnCreateRoomButtonPressed");
       
        /*if (PhotonPun.PhotonNetwork.IsConnected)
        {*/
           
            if (PhotonPun.PhotonNetwork.NickName != "")
                CreateNewRoomForLobby(PhotonPun.PhotonNetwork.NickName + roomCount);
            else
            {
                UnityEngine.Random.InitState((int)(Time.time * 10000));
                string testName = "TestUser" + UnityEngine.Random.Range(0, 1000) + roomCount;
                PhotonPun.PhotonNetwork.NickName = testName;
                CreateNewRoomForLobby(testName);
               Debug.Log("photon network connected " + testName);
        }


      /**  }
        else
        {
          //  Debug.Log("Attempting to reconnect and rejoin a room");
            PhotonPun.PhotonNetwork.ConnectUsingSettings();
            Debug.Log("photon network connected");
        }*/
        roomCount++;
    }

    public void CreateNewRoomForLobby(string roomToCreate)
    {
        var isValidRoomToJoin = !string.IsNullOrEmpty(roomToCreate);

        if (!isValidRoomToJoin)
        {
            return;
        }

        Debug.Log("JoinRoomFromLobby: attempting to create room: " + roomToCreate);

        var roomOptions = new PhotonRealtime.RoomOptions
        { IsVisible = true, MaxPlayers = 16, EmptyRoomTtl = 0, PlayerTtl = 0 };

        PhotonPun.PhotonNetwork.JoinOrCreateRoom(roomToCreate, roomOptions, PhotonRealtime.TypedLobby.Default);
    }

    #region [Photon Callbacks]
    public override void OnConnectedToMaster()
    {
        Debug.Log("Photon::OnConnectedToMaster: successfully connected to photon: " +
                                      PhotonPun.PhotonNetwork.CloudRegion);

     /*   if (controlPanel)
            controlPanel.ToggleRoomButtons(true);*/

        if (usePhotonMatchmaking)
            PhotonPun.PhotonNetwork.JoinLobby();
            
    }

    public override void OnDisconnected(PhotonRealtime.DisconnectCause cause)
    {
        Debug.Log("Photon::OnDisconnected: failed to connect: " + cause);

        if (cause != PhotonRealtime.DisconnectCause.DisconnectByClientLogic)
        {
            Photon.Pun.PhotonNetwork.ReconnectAndRejoin();
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Photon::OnJoinRoomFailed: " + message);

     /*   if (controlPanel)
            controlPanel.DisplayLobbyPanel();*/
    }
    private static void SaveUserList(HashSet<ulong> userList)
    {
        var userListAsString = string.Join(Separator.ToString(), userList);
        var setValue = new ExitGames.Client.Photon.Hashtable { { UserIdsKey, userListAsString } };

        PhotonPun.PhotonNetwork.CurrentRoom.SetCustomProperties(setValue);
    }
    public static HashSet<ulong> GetUserList()
    {
        if (PhotonPun.PhotonNetwork.CurrentRoom == null ||
            !PhotonPun.PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(UserIdsKey))
        {
            return new HashSet<ulong>();
        }

        var userListAsString = (string)PhotonPun.PhotonNetwork.CurrentRoom.CustomProperties[UserIdsKey];
        var parsedList = userListAsString.Split(Separator).Select(ulong.Parse);

        return new HashSet<ulong>(parsedList);
    }
    private void AddUserToUserListState(ulong userId)
    {
        var userList = GetUserList();
        var isKnownUserId = userList.Contains(userId);

        if (isKnownUserId)
        {
            return;
        }

        userList.Add(userId);
        SaveUserList(userList);
    }
    public void OnFindRoomButtonPressed()
    {
        if (PhotonPun.PhotonNetwork.IsConnected)
        {
          //  SampleController.Instance.Log("There are currently " + lobbyRowList.Count + " rooms in the lobby");
        /*    if (controlPanel)
                controlPanel.ToggleRoomLayoutPanel(true);*/
        }
        else
        {
            SampleController.Instance.Log("Attempting to reconnect and rejoin a room");
            PhotonPun.PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Photon::OnJoinedRoom: joined room: " + PhotonPun.PhotonNetwork.CurrentRoom.Name);

        //controlPanel.RoomText.text = "Room: " + PhotonPun.PhotonNetwork.CurrentRoom.Name;

        AddUserToUserListState(_oculusUserId);

        foreach (var player in PhotonPun.PhotonNetwork.CurrentRoom.Players.Values)
        {
            AddToUsernameList(player.NickName);
        }

        //if (controlPanel)
        //{
        //    controlPanel.DisplayMenuPanel();
        //}
#if UNITY_ANDROID
            if (Debug.automaticCoLocation)
            {
                Photon.Pun.PhotonNetwork.Instantiate("PassthroughAvatarPhoton", Vector3.zero, Quaternion.identity);

                if (PhotonPun.PhotonNetwork.IsMasterClient)
                {
                    Debug.PlaceAnchorAtRoot();
                }
            }
#endif
        GameObject sceneCaptureController = GameObject.Find("SceneCaptureController");
        if (sceneCaptureController)
        {
            if (Photon.Pun.PhotonNetwork.IsMasterClient)
            {
                sceneCaptureController.GetComponent<SceneApiSceneCaptureStrategy>().InitSceneCapture();
            }
            else
            {
                LoadRoomFromProperties();
            }
        }
    }
#if UNITY_ANDROID
    public override void OnPlayerEnteredRoom(PhotonRealtime.Player newPlayer)
    {
        Debug.Log("Photon::OnPlayerEnteredRoom: new player joined room: " + newPlayer.NickName);

        AddToUsernameList(newPlayer.NickName);

        if (Debug.automaticCoLocation)
        {
            Invoke(nameof(WaitToSendAnchor), 1);
        }
        else if (Debug.cachedAnchorSample)
        {
            Invoke(nameof(WaitToReshareAnchor), 1);
        }
    }
#endif
    #endregion [Photon Callbacks]
    private void LoadRoomFromProperties()
    {
        Debug.Log(nameof(LoadRoomFromProperties));

        if (Photon.Pun.PhotonNetwork.CurrentRoom == null)
        {
            Debug.Log("no ROOm?");
            return;
        }

        object data;
        if (Photon.Pun.PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(SceneApiSceneCaptureStrategy.RoomDataKey,
                out data))
        {
            DeserializeToScene((byte[])data);
        }
    }
    void DeserializeToScene(byte[] byteData)
    {
        string jsonData = System.Text.Encoding.UTF8.GetString((byte[])byteData);
        Scene deserializedScene = JsonUtility.FromJson<Scene>(jsonData);
        if (deserializedScene != null)
            Debug.Log("deserializedScene num walls: " + deserializedScene.walls.Length);
        else
            Debug.Log("deserializedScene is NULL");

        GameObject worldGenerationController = GameObject.Find("WorldGenerationController");
        if (worldGenerationController)
            worldGenerationController.GetComponent<WorldGenerationController>().GenerateWorld(deserializedScene);
    }
    private void AddToUsernameList(string username)
    {
        var isKnownUserName = _usernameList.Contains(username);

        if (isKnownUserName)
        {
            return;
        }

        _usernameList.Add(username);
        UpdateUsernameListDebug();
    }
    private void RemoveFromUsernameList(string username)
    {
        var isUnknownUserName = !_usernameList.Contains(username);

        if (isUnknownUserName)
        {
            return;
        }

        _usernameList.Remove(username);
        UpdateUsernameListDebug();
    }

    private void UpdateUsernameListDebug()
    {
        //controlPanel.UserText.text = "Users:";

        var usernameListAsString = string.Join(Separator.ToString(), _usernameList);
        var usernames = usernameListAsString.Split(',');

        /*foreach (var username in usernames)
        {
            controlPanel.UserText.text += "\n" + "- " + username;
        }*/
    }
}
