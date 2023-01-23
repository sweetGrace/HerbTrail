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

    public static CreateRange instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public Image CreateRangeHighlight(Vector3Int position)
    {
        return Instantiate(rangeImage, tileMap.CellToWorld(position), Quaternion.identity, this.transform);
    }

}
