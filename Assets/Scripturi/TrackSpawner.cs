using UnityEngine;
using System.Collections.Generic;

public class TrackSpawner : MonoBehaviour
{
    [SerializeField] private GameObject trackPrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private int numberOfTracks = 5;
    [SerializeField] private float trackLength = 30f;

    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject powerUpPrefab;

    private List<GameObject> activeTracks = new List<GameObject>();
    private float spawnZ = 0f;
    private float[] lanes = { -3f, 0f, 3f };

    private void Start()
    {
        SpawnTrack(false); 

        for (int i = 1; i < numberOfTracks; i++)
        {
            SpawnTrack(true);
        }
    }

    private void Update()
    {
        if (playerTransform == null) return;

        if (playerTransform.position.z - trackLength > spawnZ - (numberOfTracks * trackLength))
        {
            SpawnTrack(true);
            DeleteOldTrack();
        }
    }

    private void SpawnTrack(bool spawnItems)
    {
        GameObject go = Instantiate(trackPrefab, transform.forward * spawnZ, Quaternion.identity);
        activeTracks.Add(go);

        if (spawnItems)
        {
            SpawnItemsOnTrack(spawnZ);
        }

        spawnZ += trackLength;
    }

    private void SpawnItemsOnTrack(float zPosition)
    {
        int obstacleLane = Random.Range(0, lanes.Length);
        Vector3 obstaclePos = new Vector3(lanes[obstacleLane], 1f, zPosition + Random.Range(-10f, 10f));
        GameObject obs = Instantiate(obstaclePrefab, obstaclePos, Quaternion.identity);
        obs.transform.SetParent(activeTracks[activeTracks.Count - 1].transform);

        int itemLane = Random.Range(0, lanes.Length);
        if (itemLane != obstacleLane)
        {
            Vector3 itemPos = new Vector3(lanes[itemLane], 1f, zPosition + Random.Range(-10f, 10f));
            
            GameObject prefabToSpawn = (Random.value < 0.50f) ? powerUpPrefab : coinPrefab;
            
            GameObject item = Instantiate(prefabToSpawn, itemPos, Quaternion.identity);
            item.transform.SetParent(activeTracks[activeTracks.Count - 1].transform);
        }
    }

    private void DeleteOldTrack()
    {
        Destroy(activeTracks[0]);
        activeTracks.RemoveAt(0);
    }
}