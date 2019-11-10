using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawn : MonoBehaviour
{
    [SerializeField] Transform[] trashSpawns;
    [SerializeField] GameObject[] trashs;
    [SerializeField] int trashsToStart;
    void Start()
    {
        for (int i = 0; i < trashsToStart; i++)
        {
            SpawnTrash();
        }
    }

    public void SpawnTrash()
    {
        int randomTrash;
        randomTrash = Random.Range(0, trashs.Length);
        int randomTrashSpawn;
        randomTrashSpawn = Random.Range(0, trashSpawns.Length);

        Instantiate(trashs[randomTrash], trashSpawns[randomTrashSpawn].position, Quaternion.identity);
    }
}
