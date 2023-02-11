using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField]
    private List<GameObject> gridTiles;
    [SerializeField]
    private GameObject tilePrefab;

    [SerializeField]
    private List<GameObject> emptyGOs;
    [SerializeField]
    private GameObject emptyGO;

    public GameObject GetTile()
    {
        if (gridTiles == null)
            gridTiles = new List<GameObject>();
        for (int i = 0; i < gridTiles.Count; i++)
            if (!gridTiles[i].activeSelf)
            {
                gridTiles[i].SetActive(true);
                return gridTiles[i];
            }
        GameObject newTile = Instantiate(tilePrefab, transform);
        gridTiles.Add(newTile);
        return newTile;
    }

    public GameObject GetEmptyObj()
    {
        if (emptyGOs == null)
            emptyGOs = new List<GameObject>();
        for (int i = 0; i < emptyGOs.Count; i++)
            if (!emptyGOs[i].activeSelf)
            {
                emptyGOs[i].SetActive(true);
                return emptyGOs[i];
            }
        GameObject newEmptyGO = Instantiate(emptyGO, transform);
        emptyGOs.Add(newEmptyGO);
        return newEmptyGO;
    }

    public void SetGameObjBack(GameObject gameObject)
    {
        gameObject.gameObject.SetActive(false);
        gameObject.transform.parent = transform;
    }

}