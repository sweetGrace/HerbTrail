using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twig : MonoBehaviour
{
    public PlantOrgan fatherNode{get; private set;} = null;
    public SpriteRenderer statePicRenderer;
    public Sprite[] stateWitheringPics;
    public Sprite[] stateSpreadingPics;
    public bool isWithering = false;
    public void ChangeStatePic(PlantType type, Sprite[] pics){
        statePicRenderer.sprite = pics[(int)type];
    } 
    public void ChangeStatePic(Sprite[] pics){
        statePicRenderer.sprite = pics[(int)fatherNode?.type];
    }
    public void InitMe(PlantOrgan mfatherNode){
        this.fatherNode = mfatherNode;
    }
    public void Wither()
    { 
        isWithering = true;
        ChangeStatePic(stateWitheringPics)
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
