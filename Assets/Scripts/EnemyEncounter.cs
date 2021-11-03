using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//int currentEnemy;

public class EnemyEncounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3[] TerrainScales = new Vector3[37];//37 blocks
        //Terrain Layout
        GameObject map = GameObject.Find("Terrain");
        MapGeneration mapTerrain = map.GetComponent<MapGeneration>();
        GameObject[] terrain = mapTerrain.Terrain;
        //Terrain Layout

        //Enemies
        GameObject enemies = GameObject.Find("Enemies");

        //Enemies
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
