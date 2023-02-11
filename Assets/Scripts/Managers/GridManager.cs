using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

public class GridManager : Singleton<GridManager>
{

    private GridO grid;
    [SerializeField]
    private DragTile dragTileScr;
    [SerializeField]
    private Material[] tilesMaterials;
    private Vector3Int cPickedVect;
    private TileType cSelectedTileType;
    [SerializeField]
    private Transform groundTrans;
    private PoolManager poolManager;
    private RunTimeDataStorage runTimeDataStorage;
    [SerializeField]
    private UnityEvent<int> tileAdded;
    [SerializeField]
    private UnityEvent<int> tileRemoved;
    private const string gridPrefsKey = "grid";

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        SetRefs();
        LoadGrid();
    }

    private void LoadGrid()
    {
        if (runTimeDataStorage.HasData("grid"))
            LoadSavedGrid();
        else
            CreateEmptyGrid();
    }

    private void CreateEmptyGrid()
    {
        grid = new GridO();
    }

    private JsonSerializerSettings getJSONSettings()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.Formatting = Formatting.Indented;
        settings.ContractResolver = new DictionaryAsArrayResolver();
        return settings;
    }

    private void LoadSavedGrid()
    {
        JsonSerializerSettings settings = getJSONSettings();
        GridO grid = JsonConvert.DeserializeObject<GridO>(runTimeDataStorage.GetData(gridPrefsKey), settings);
        GenerateLevelFromGrid(grid);
    }

    private void SetRefs()
    {
        if (poolManager == null)
            poolManager = PoolManager.Instance;
        if (runTimeDataStorage == null)
            runTimeDataStorage = RunTimeDataStorage.Instance;
    }

    public void SetDragTileType(int itemNu)
    {
        cSelectedTileType = (TileType)itemNu;
        dragTileScr.Activate(tilesMaterials[itemNu]);
    }

    public void SetDragTilePos(Vector3Int newVector3)
    {
        Tuple<int, int> posXZ = getXZPos(newVector3);
        if (thereIsGroundTile(newVector3))
        {
            newVector3 = new Vector3Int(newVector3.x, getHighOfTilesList(posXZ) + 1, newVector3.z);
            dragTileScr.SetPos(newVector3);
        }
        else
            dragTileScr.SetPos(newVector3);
        cPickedVect = newVector3;
    }

    private int getHighOfTilesList(Tuple<int, int> posXZ)
    {
        return grid.groundTiles[posXZ].tiles.Count;
    }

    private bool thereIsGroundTile(Vector3Int vector3)
    {
        return grid.groundTiles.ContainsKey(getXZPos(vector3));
    }

    private static Tuple<int, int> getXZPos(Vector3Int vector3)
    {
        return new Tuple<int, int>(vector3.x, vector3.z);
    }

    public void StopDrag(Transform tileTrans)
    {
        dragTileScr.DisActivate();
        Transform groundTileTrans = tileTrans;
        if (groundTileTrans == null)
        {
            GameObject newEmptyGO = createNewGroundTile(getXZPos(cPickedVect));
            groundTileTrans = newEmptyGO.transform;
            AddNewTile(groundTileTrans);
        }
        else
            AddNewTile(groundTileTrans.parent);
    }

    public void RemoveTile(Transform tile)
    {
        cSelectedTileType = tile.gameObject.GetComponent<GridTile>().GetTile();
        RemoveTileFromGrid(tile);
        ArrangeTileColumn(tile);
        RemoveTile3D(tile);
        tileRemoved.Invoke((int)cSelectedTileType);
    }

    private void RemoveTile3D(Transform removedTileTrans)
    {
        if (removedTileTrans.parent.childCount == 1)
            poolManager.SetGameObjBack(removedTileTrans.parent.gameObject);
        poolManager.SetGameObjBack(removedTileTrans.gameObject);
    }

    private void RemoveTileFromGrid(Transform tile)
    {
        Tuple<int, int> PosXZ = getXZPos(Vectors.intVectValues(tile.transform.position));
        int tileIndex = tile.GetSiblingIndex();
        grid.groundTiles[PosXZ].tiles.RemoveAt(tileIndex);
        if (grid.groundTiles[PosXZ].tiles.Count == 0)
            grid.groundTiles.Remove(PosXZ);

    }

    private void ArrangeTileColumn(Transform removedTileTrans)
    {
        int tileIndex = removedTileTrans.GetSiblingIndex();
        Transform tileParentTrans = removedTileTrans.parent;
        for (int i = tileIndex + 1; i < tileParentTrans.childCount; i++)
        {
            Vector3Int curChildPos = Vectors.intVectValues(tileParentTrans.GetChild(i).position);
            tileParentTrans.GetChild(i).position = new Vector3(curChildPos.x, curChildPos.y - 1, curChildPos.z);
        }
    }

    private void AddNewTile(Transform parentTrans)
    {
        TileO newTileObj = new TileO(cSelectedTileType);
        AddNewTileToGridObj(newTileObj);
        AddNewTileToWorldGrid(newTileObj, parentTrans);
    }

    private void AddNewTileToGridObj(TileO tileObj)
    {
        if (!thereIsGroundTile(cPickedVect))
            AddNewGroundTile(getXZPos(cPickedVect));
        grid.groundTiles[getXZPos(cPickedVect)].tiles.Add(tileObj);
    }

    private void AddNewTileToWorldGrid(TileO tile, Transform parentTrans)
    {
        GameObject newTile = CreateNewTile(tile);
        newTile.transform.SetParent(parentTrans);
        newTile.GetComponent<Tile>().SetPos(cPickedVect);
        tileAdded.Invoke((int)cSelectedTileType);
    }

    private GameObject CreateNewTile(TileO tile)
    {
        GameObject newTileGO = poolManager.GetTile();
        newTileGO.GetComponent<GridTile>().Activate(tile, tilesMaterials[(int)tile.tileType]);
        return newTileGO;
    }

    private void AddNewGroundTile(Tuple<int, int> posXZ)
    {
        GroundTileO newGroundtile = new GroundTileO();
        grid.groundTiles.Add(posXZ, newGroundtile);
    }

    public void GenerateLevelFromGrid(GridO grid)
    {
        this.grid = new GridO(grid.groundTiles);
        foreach (var groundTile in this.grid.groundTiles)
        {
            GameObject newGroundTile = createNewGroundTile(groundTile.Key);
            for (int i = 0; i < groundTile.Value.tiles.Count; i++)
            {
                cSelectedTileType = groundTile.Value.tiles[i].GetTileType();
                cPickedVect = new Vector3Int((int)newGroundTile.transform.position.x, i + 1, (int)newGroundTile.transform.position.z);
                AddNewTileToWorldGrid(groundTile.Value.tiles[i], newGroundTile.transform);
            }
        }
    }

    private GameObject createNewGroundTile(Tuple<int, int> posXZ)
    {
        GameObject newGroundObj = poolManager.GetEmptyObj();
        newGroundObj.transform.position = new Vector3(posXZ.Item1, 0, posXZ.Item2);
        newGroundObj.transform.SetParent(groundTrans);
        return newGroundObj;
    }

    public void SaveGrid()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.Formatting = Formatting.Indented;
        settings.ContractResolver = new DictionaryAsArrayResolver();
        string json = JsonConvert.SerializeObject(grid, settings);
        runTimeDataStorage.SaveData(gridPrefsKey, json);

    }
}