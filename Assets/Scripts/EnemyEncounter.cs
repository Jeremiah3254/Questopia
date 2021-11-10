using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//int currentEnemy;

public class EnemyEncounter : MonoBehaviour
{
    bool enemyEncountered;
    GameObject[] terrain;
    GameObject[] enemiesC;
    public GameObject currentOpponent;
    // Start is called before the first frame update
    void Start()
    {
        //Enemies = new GameObject[50];
        Vector3[] TerrainScales = new Vector3[37];//37 blocks
        terrain = GameObject.Find("Terrain").GetComponent<MapGeneration>().Terrain;
        //Terrain Layout
        enemiesC = GameObject.Find("Enemies").GetComponent<EnemyMap>().mobs;
    }

    // Update is called once per frame
    void Update()
    {
        //refreshVariables();
        for (int i = 0; i < enemiesC.Length; i++) {
            if (enemiesC[i] != null) {
                float dist = Vector3.Distance(enemiesC[i].transform.position,transform.position);
                if (dist <= 3) {
                    currentOpponent = enemiesC[i];
                    createMap();
                    Destroy(enemiesC[i]);
                }
            }
        }
    }

    public void createMap() {
        int mapStartingPoint = (int) Mathf.Abs((((Mathf.Abs(Mathf.Floor(transform.localPosition.x))/*+1*/)*100)+(Mathf.Abs((Mathf.Floor(transform.localPosition.z))))));
        Debug.Log(mapStartingPoint);
        Debug.Log(terrain[mapStartingPoint].transform.localPosition.x+","+terrain[mapStartingPoint].transform.localPosition.z);
        terrain[mapStartingPoint].transform.localScale = new Vector3 (terrain[mapStartingPoint].transform.localScale.x,(terrain[mapStartingPoint].transform.localScale.y+24),terrain[mapStartingPoint].transform.localScale.z);
        //Debug.Log(terrain[mapStartingPoint].transform.localScale.y);
        
    }

    public void refreshVariables() {
        terrain = GameObject.Find("Terrain").GetComponent<MapGeneration>().Terrain;
        enemiesC = GameObject.Find("Enemies").GetComponent<EnemyMap>().mobs;
    }

}
