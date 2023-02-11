using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : Singleton<MouseManager>
{

    [SerializeField]
    private UnityEvent<Vector3Int> curMousePosEv;
    [SerializeField]
    private UnityEvent<Transform> stopDragging;
    [SerializeField]
    private UnityEvent<Transform> rightMouseClickOnTile;
    private Vector3Int curMousePos;
    private bool isDraggingTile;
    private Transform pickedPosBoxTrans;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            cancelDrag();
            RightMouseClick();
            return;
        }
        if (!isDraggingTile)
            return;
        if (Input.GetMouseButtonUp(0))
            SetOnStopDrag();
        if (Input.GetMouseButton(0))
            SetOnDragTile();
    }

    private void RightMouseClick()
    {
        RaycastHit cameraRayHit = shootRayCast();
        if (cameraRayHit.transform == null)
            return;
        string hitTag = cameraRayHit.transform.tag;
        if (!isTile(hitTag))
            return;
        rightMouseClickOnTile.Invoke(cameraRayHit.transform);
    }

    private void SetOnDragTile()
    {
        RaycastHit cameraRayHit = shootRayCast();
        if (cameraRayHit.transform == null)
            return;
        string hitTag = cameraRayHit.transform.tag;
        if (!isTileTag(hitTag))
            return;
        Vector3Int newMousePos = new Vector3Int(Mathf.FloorToInt(cameraRayHit.point.x), Mathf.FloorToInt(cameraRayHit.point.y) + 1, Mathf.FloorToInt(cameraRayHit.point.z));
        if (curMousePos == newMousePos)
            return;
        curMousePos = newMousePos;
        if (hitTag == "Tile")
        {
            pickedPosBoxTrans = cameraRayHit.transform;
            Vector3Int tileVect = Vectors.intVectValues(pickedPosBoxTrans.position);
            Vector3Int hitTileVect = new Vector3Int(tileVect.x, tileVect.y + 1, tileVect.z);
            curMousePosEv.Invoke(hitTileVect);
        }
        else
        {
            pickedPosBoxTrans = null;
            curMousePosEv.Invoke(curMousePos);
        }
    }

    private RaycastHit shootRayCast()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit cameraRayHit;
        Physics.Raycast(cameraRay, out cameraRayHit);
        return cameraRayHit;
    }

    private void SetOnStopDrag()
    {
        stopDragging.Invoke(pickedPosBoxTrans);
        cancelDrag();
    }

    private void cancelDrag()
    {
        isDraggingTile = false;
        pickedPosBoxTrans = null;
    }

    private bool isTile(string tag)
    {
        return tag == "Tile";
    }

    private bool isTileTag(string tag)
    {
        return tag == "Ground" || isTile(tag);
    }

    public void SetisDraggingTile() { isDraggingTile = true; }

}