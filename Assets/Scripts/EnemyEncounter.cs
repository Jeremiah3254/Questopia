using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//int currentEnemy;

public class EnemyEncounter : MonoBehaviour
{
    bool enemyEncountered;
    public GameObject[] enemiesC;
    // Start is called before the first frame update
    void Start()
    {
        //Enemies = new GameObject[50];
        Vector3[] TerrainScales = new Vector3[37];//37 blocks
        //Terrain Layout
        GameObject map = GameObject.Find("Terrain");
        MapGeneration mapTerrain = map.GetComponent<MapGeneration>();
        GameObject[] terrain = mapTerrain.Terrain;
        //Terrain Layout

        //Enemies
        /*GameObject EnemyMapHolder = GameObject.Find("Enemies");
        EnemyMap yep = EnemyMapHolder.GetComponent<EnemyMap>();
        enemiesC = yep.mobs;*/
        enemiesC = GameObject.Find("Enemies").GetComponent<EnemyMap>().mobs;
        //Enemies
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < Enemies.Length; i++) {
            float dist = Vector3.Distance(transform.position, enemiesC[0].transform.position);
            //Debug.Log(Enemies[1].transform.position);
        //}
    }
}
