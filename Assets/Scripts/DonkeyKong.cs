using UnityEngine;

public class DonkeyKong : MonoBehaviour
{
    public GameObject barrelPrefab;
    public Transform spawnPoint;

    public void SpawnBarrel()
    {
        Debug.Log("Barrel Released!");

        //creates a copy of the barrel prefab and spawns it at DK's spawn point
        Instantiate(barrelPrefab,spawnPoint.position,Quaternion.identity);
    }
}