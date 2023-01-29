using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HighlightRange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    Image highlightImage;

    List<Image> images = new List<Image>();

    public List<Vector2Int> positions;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.transform.parent.localScale.x > 0.9)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                images.Add(CreateRange.Instance.CreateRangeHighlight((Vector3Int)positions[i]));

            }
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        if(images != null)
        {
            foreach (Image i in images)
            {
                Destroy(i.gameObject);
            }
            images.Clear();
        }
    }
}
