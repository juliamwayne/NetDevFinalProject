using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public Player prefabPlayer;

    public GameObject goSpawnPoints;

    private int spawnIndex = 0;

    private List<Vector3> listSpawnLocations = new List<Vector3>();

    public void Awake()
    {
        FindRefreshSpawnPointLocations();
    }


    void SpawnPlayers()
    {
        foreach (PlayerInfo playerInfo in GameData.Instance.allPlayers)
        {
            Player playerSpawn = Instantiate(prefabPlayer, GetNewVector3SpawnLocation(), Quaternion.identity);
            playerSpawn.GetComponent<NetworkObject>().SpawnAsPlayerObject(playerInfo.clientId);
            playerSpawn.PlayerColor.Value = playerInfo.color;
            //players.Add(playerSpawn);
        }
    }

    public override void OnNetworkSpawn()
    {
        if (IsHost)
        {
            SpawnPlayers();
        }
    }

    void FindRefreshSpawnPointLocations()
    {
        Transform[] arrayAllSpawnPoints = goSpawnPoints.GetComponentsInChildren<Transform>();
        listSpawnLocations.Clear();

        foreach (Transform spawnPoint in arrayAllSpawnPoints)
        {
            if (spawnPoint != goSpawnPoints.transform)
            {
                listSpawnLocations.Add(spawnPoint.localPosition);
            }
        }
    }

    public Vector3 GetNewVector3SpawnLocation()
    {
        var newPosition = listSpawnLocations[spawnIndex];
        newPosition.y = 1.5f;
        spawnIndex += 1;

        if (spawnIndex > listSpawnLocations.Count - 1)
        {
            spawnIndex = 0;
        }

        return newPosition;
    }
}