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
    AudioClip harvestSFX, selectedSFX;

    [SerializeField]
    Tilemap map;

    Lattice selectedLattice;
    Vector2 worldPosition;
    Vector3Int cellPosition;
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Tilemap tilemap = map.GetComponent<Tilemap>();
            cellPosition = tilemap.WorldToCell(worldPosition);
            //selectedLattice = Map.Instance.latticeMap[cellPosition.x, cellPosition.y];
            worldPosition = tilemap.CellToWorld(cellPosition);
            if (harvestPanel == null && transform.Find("HarvestPanel(Clone)") == null && gameObject.activeSelf)
            {
                harvestPanel = Instantiate(panel, transform);
                harvestPanel.transform.position = worldPosition;
                harvestPanel.transform.localScale = Vector3.zero;
                StartCoroutine(ShowPanel());
                CreateHarvestPanel();
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
        SoundManager.instance.PlaySingle(selectedSFX);
        CreateHervestButton();
        CreateHervestButton();
        CreateHervestButton();
    }

    void CreateHervestButton()
    {
        GameObject panel = Instantiate(button, harvestPanel.transform);
        Button harvestButton = panel.transform.GetChild(1).GetComponent<Button>();
        Text harvestText = panel.transform.GetChild(0).GetComponent<Text>();
        //Plant plant = new Plant((Vector2Int)cellPosition,4);
        //harvestText.text = plant.plant;
        //panel.GetComponent<Highlight>().positions = plant.positions;

        harvestButton.onClick.AddListener(delegate ()
        {
            SoundManager.instance.PlaySingle(harvestSFX);
            StartCoroutine(HidePanel());
        });
    }

}
