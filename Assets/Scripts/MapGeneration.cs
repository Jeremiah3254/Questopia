using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public GameObject[] Terrain;
    Transform parent;
    public int bottomBorder;
    public int CurrentRow;
    
    // Start is called before the first frame update
    void Awake() {
        CurrentRow = 0;
        Terrain = new GameObject[10000];
        for (int i = 0; i<Terrain.Length; i++) {
            if (i >= ((CurrentRow+1)*bottomBorder) ) {
                parent = gameObject.transform;
                CurrentRow = CurrentRow+1;
                Terrain[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Terrain[i].transform.position = new Vector3(CurrentRow,0,(bottomBorder*CurrentRow)-i);
                if (Random.Range(1,3) == 1 && i > bottomBorder && (((Terrain[i-1].transform.localScale.y+Terrain[i-bottomBorder].transform.localScale.y)/2) >= 2f)) {
                    Terrain[i].transform.localScale = new Vector3(1,((((Terrain[i-1].transform.localScale.y)+(Terrain[i-bottomBorder].transform.localScale.y))/2)-1),1);// change 1 to Random.Range(1,2)
                }else if (i > bottomBorder) {
                    Terrain[i].transform.localScale = new Vector3(1,((((Terrain[i-1].transform.localScale.y)+(Terrain[i-bottomBorder].transform.localScale.y))/2)+1),1);// change 1 to Random.Range(1,2)
                }
                Terrain[i].layer = 6;
                //Terrain[i].GetComponent<Renderer>().material.color = new Color(0,1f,0);   
            }else {
                Terrain[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Terrain[i].transform.position = new Vector3(CurrentRow,0,(bottomBorder*CurrentRow)-i);
                if (Random.Range(1,3) == 1 && i > bottomBorder && (((Terrain[i-1].transform.localScale.y+Terrain[i-bottomBorder].transform.localScale.y)/2) >= 2f)) {
                    Terrain[i].transform.localScale = new Vector3(1,((((Terrain[i-1].transform.localScale.y)+(Terrain[i-bottomBorder].transform.localScale.y))/2)-1),1);// change 1 to Random.Range(1,2)
                }else if (i > bottomBorder) {
                    Terrain[i].transform.localScale = new Vector3(1,((((Terrain[i-1].transform.localScale.y)+(Terrain[i-bottomBorder].transform.localScale.y))/2)+1),1);// change 1 to Random.Range(1,2)
                }else {
                    Terrain[i].transform.localScale = new Vector3(1,Random.Range(1f,3f),1);
                }
                Terrain[i].layer = 6;
                //Terrain[i].GetComponent<Renderer>().material.color = new Color(0,1f,0);
            }
            Terrain[i].transform.SetParent(parent);
            //insert any preset block height
        }
    }

    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
