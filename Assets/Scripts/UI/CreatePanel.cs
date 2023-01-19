using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePanel : MonoBehaviour
{
    [SerializeField]
    GameObject panel,harvestPanel;

    [SerializeField]
    AnimationCurve hideCurve, showCurve;

    [SerializeField]
    float animationSpeed;

    [SerializeField]
    GameObject button;

    IEnumerator ShowPanel()
    {
        float timer = 0;
        while (timer <= 1)
        {
            harvestPanel.transform.localScale = Vector3.one * showCurve.Evaluate(timer);
            timer += Time.deltaTime * animationSpeed;
            yield return null;
            Debug.Log(harvestPanel.transform.localScale);
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

        Destroy(harvestPanel.gameObject);
    }

    private void OnMouseDown()
    {

        if (harvestPanel == null)
        {
            harvestPanel = Instantiate(panel,GameObject.Find("GamePanel").transform);
            harvestPanel.transform.position = transform.position;
            harvestPanel.transform.localScale = Vector3.zero;
            StartCoroutine(ShowPanel());
            CreateHarvestPanel();
        }
        //else if (Input.GetMouseButtonDown(1) )
        //{
        //    StartCoroutine(ShowPanel());
        //}

    }

    public void CreateHarvestPanel()
    {
        CreateHervestButton();
        CreateHervestButton();
        CreateHervestButton();
    }

    void CreateHervestButton()
    {
        Button harvestButton = Instantiate(button, harvestPanel.transform).transform.GetChild(1).GetComponent<Button>();
        harvestButton.onClick.AddListener(delegate ()
        {
            StartCoroutine(HidePanel());
        });
    }

}
