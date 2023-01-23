using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twig : MonoBehaviour
{
    public PlantOrgan fatherNode{get; private set;} = null;
    public SpriteRenderer statePicRenderer;
    public Sprite[] statePics;
    public void ChangeStatePic(PlantType type){
        statePicRenderer.sprite = statePics[(int)type];
    } 
    public void InitMe(PlantOrgan mfatherNode){
        this.fatherNode = mfatherNode;
    }
    // Start is called before the first frame update
    void Start()
    {
        statePicRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
