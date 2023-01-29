using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    float speed = 5.0f;

    [SerializeField]
    Tilemap map;
    private void Awake()
    {
        transform.position = map.CellToWorld(new Vector3Int(Map._maxMap / 2, Map._maxMap / 2, -10));
    }
    void Update()
    {

        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed; //左右移动
        float y = Input.GetAxis("Vertical") * Time.deltaTime * speed;   //前后移动
        transform.Translate(x, y,0);   //相机移动
    }
}
