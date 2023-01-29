using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class CreatePanel : MonoBehaviour
{
    [SerializeField]
    GameObject panel, harvestPanel;

    [SerializeField]
    GameObject button;

    [SerializeField]
    AnimationCurve hideCurve, showCurve;

    [SerializeField]
    float animationSpeed;

    [SerializeField]
    Tilemap map;

    Vector2 worldPosition;
    Vector3Int cellPosition;

    Lattice selectedLattice;
    int currentLayer = 1;
    List<PlantOrgan> currentLayerOrgans;
    List<Vector2Int> organPositions = new List<Vector2Int>();
    List<Vector2Int> tilePositions = new List<Vector2Int>();

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            currentLayer = PlayerInfo.Instance.currentLayer;

            worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Tilemap tilemap = map.GetComponent<Tilemap>();
            cellPosition = tilemap.WorldToCell(worldPosition);
            cellPosition.x = Mathf.Clamp(cellPosition.x, 0, Map._maxMap);
            cellPosition.y = Mathf.Clamp(cellPosition.y, 0, Map._maxMap);
            worldPosition = tilemap.CellToWorld(cellPosition);

            selectedLattice = Map.Instance.latticeMap[cellPosition.x, cellPosition.y];
            

            if (harvestPanel == null && transform.Find("HarvestPanel(Clone)") == null && gameObject.activeSelf)
            {
                harvestPanel = Instantiate(panel, transform);
                harvestPanel.transform.position = worldPosition;
                harvestPanel.transform.localScale = Vector3.zero;
                //根据地形生成收获/信息界面
                switch (selectedLattice.ground.type)
                {
                    
                    case GroundType.plain:
                        {
                            harvestPanel.transform.Find("TerrianText").GetComponent<Text>().text = "平原";
                            harvestPanel.transform.Find("FertilityText").GetComponent<Text>().text = "肥力等级：" + selectedLattice.ground.fertilityDegree.ToString();
                            currentLayerOrgans = IsSameLayer(selectedLattice);
                            StartCoroutine(ShowPanel());
                            CreateHarvestPanel();
                            break;
                        }
                    case GroundType.seawater:
                        {
                            harvestPanel.transform.Find("TerrianText").GetComponent<Text>().text = "海域";
                            harvestPanel.transform.Find("FertilityText").GetComponent<Text>().text = " ";
                            StartCoroutine(ShowPanel());
                            break;
                        }
                    default:
                        {
                            Debug.LogError("Select wrong lattice");
                            return;
                        }

                } 
                
                
            }
            
        }
    }

    IEnumerator ShowPanel()
    {
        float timer = 0;
        while (timer <= 1)
        {
            harvestPanel.transform.localScale = Vector3.one * showCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
            //Debug.Log(harvestPanel.transform.localScale);
        }
    }

    public IEnumerator HidePanel()
    {
        float timer = 0;
        while (timer <= 1)
        {
            harvestPanel.transform.localScale = Vector3.one * hideCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed * 2;
            yield return null;
        }
        //Destroy(this.gameObject);
        Destroy(harvestPanel.gameObject);
    }

    public void CreateHarvestPanel()
    {
        SoundManager.Instance.PlaySingle(ClipsType.select);

        CreateHervestButton();

        foreach(PlantOrgan o in currentLayerOrgans)
        {
            if(o != null)
            {
                CreateHervestButton(o);
            }
        }
        
    }

    void CreateHervestButton(PlantOrgan organ = null)
    {
        GameObject panel = Instantiate(button, harvestPanel.transform);
        Button harvestButton = panel.transform.GetChild(1).GetComponent<Button>();
        Text harvestText = panel.transform.GetChild(0).GetComponent<Text>();
        HighlightRange range = panel.GetComponent<HighlightRange>();
        
        if(organ != null)
        {
            range.positions = organPositions;
            organPositions.Clear();
            //harvestText.text = organ.type.ToString() + "'s" + organ.OrganType.ToString();
            harvestText.text = organ.OrganType.ToString();
        }
        else
        {
            range.positions = tilePositions;
            harvestText.text = "Tile";
        }

        harvestButton.onClick.AddListener(delegate ()
        {
            SoundManager.Instance.PlaySingle(ClipsType.harvest);
            Map.Instance.HarvestPlant(selectedLattice, organ);
            InfoPanel.Instance.UpdateHungerAndCost();
            StartCoroutine(HidePanel());
        });
    }

    List<PlantOrgan> IsSameLayer(Lattice lattice)
    {
        List<PlantOrgan> organs = new List<PlantOrgan>();

        foreach(PlantOrgan o in lattice.plantOrgans)
        {
            if (o.layer == currentLayer)
            {
                organs.Add(o);
            }
        }

        return organs;
    }

    public void RangePosition(PlantOrgan organ)
    {
        Vector2Int position = new Vector2Int((int)organ.lattice.position.x, (int)organ.lattice.position.y);
        
        if (organPositions.Contains(position) == false && organ.isPlanted == true)
        {
            organPositions.Add(position);

            if(tilePositions.Contains(position) == false)
            {
                tilePositions.Add(position);
            }

            foreach(PlantOrgan o in organ.spreadOrgans)
            {
                RangePosition(o);
            }
        }

        return ;
    }
}
