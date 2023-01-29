using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class CreateRange: MonoBehaviour
{
    [SerializeField]
    Image rangeImage,redRangeImage;

    [SerializeField]
    Tilemap tileMap;

    List<Image> ranges;

    public static CreateRange Instance = null;

    private void Awake()
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

    public void CreateWarningRange()
    {
        foreach(Lattice l in Map.Instance.generateWaterList)
        {
            ranges.Add(Instantiate(redRangeImage, tileMap.CellToWorld(new Vector3Int((int)l.position.x, (int)l.position.y,0)), Quaternion.identity, this.transform));
        }
    }

    public void DeleteWarningRange()
    {
        if(ranges != null)
        {
            foreach (Image i in ranges)
            {
                Destroy(i.gameObject);
            }
            ranges.Clear();
        }
    }


    public Vector3 CubeTreeTranslate(Vector3 worldPos)
    {
        Vector3Int offset = new Vector3Int(3, 3, 0);
        Vector3Int cellPos = tileMap.WorldToCell(worldPos);
        Vector3Int offsetCellPos = cellPos + offset;
        return tileMap.CellToWorld(offsetCellPos);

    }

}
