using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class SelectTile : MonoBehaviour
{
    [SerializeField]
    Image selectedImage;

    [SerializeField]
    Tilemap tileMap;

    //Vector3Int previousMousePos = new Vector3Int();
    private void Update()
    {
        Vector3Int mousePos = GetMousePosition();
        //Debug.Log(mousePos);
        Vector3 highPos = tileMap.CellToWorld(GetMousePosition());
        selectedImage.transform.position = highPos;
        //if (!mousePos.Equals(previousMousePos))
        //{
        //    game.SetTile(previousMousePos, null); // Remove old hoverTile
        //    game.SetTile(mousePos, hoverTile);
        //    previousMousePos = mousePos;
        //}
    }

    Vector3Int GetMousePosition()
    {
        int x = 0;
        int y = 0;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = tileMap.WorldToCell(mouseWorldPos);
        x = Mathf.Clamp(cellPos.x,0, Map._maxMap);
        y = Mathf.Clamp(cellPos.y, 0, Map._maxMap);

        return new Vector3Int(x, y, 0);
       
    }
}
