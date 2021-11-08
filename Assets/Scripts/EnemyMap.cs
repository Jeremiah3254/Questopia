using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMap : MonoBehaviour
{
    public GameObject[] enemyVariations;
    public GameObject[] mobs;
        void Start()
    {
        mobs = new GameObject[50];
        //Add enemies here
        mobs[0] = Instantiate(enemyVariations[0], new Vector3(2,10,-15), enemyVariations[0].transform.rotation);
        //Add enemies here

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
