using UnityEngine;

public class DonkeyKong : MonoBehaviour
{
    public GameObject barrelPrefab;
    public Transform spawnPoint;

    public void SpawnBarrel()
    {
        Debug.Log("Barrel Released!");
        Instantiate(barrelPrefab,spawnPoint.position,Quaternion.identity);
    }
}