using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class CreateRange: MonoBehaviour
{
    [SerializeField]
    Image rangeImage;

    [SerializeField]
    Tilemap tileMap;

    public static CreateRange Instance = null;

    private void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("CreateRange already exists.");
            return;
        }
        Instance = this;
    }

    public Image CreateRangeHighlight(Vector3Int position)
    {
        return Instantiate(rangeImage, tileMap.CellToWorld(position), Quaternion.identity, this.transform);
    }

    public Vector3 CubeTreeTranslate(Vector3 worldPos)
    {
        Vector3Int offset = new Vector3Int(3, 3, 0);
        Vector3Int cellPos = tileMap.WorldToCell(worldPos);
        Vector3Int offsetCellPos = cellPos + offset;
        return tileMap.CellToWorld(offsetCellPos);

    }

}
