using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class HighlightButtonExtra : MonoBehaviour, ISelectHandler, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    Sprite normalSprite;
    [SerializeField]
    Sprite highlightedSprite;

    Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }
    public void OnSelect(BaseEventData eventData)
    {
        // Do something.
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        btn.image.sprite = highlightedSprite;
        //((IPointerEnterHandler)btn).OnPointerEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventdata)
    {
        btn.image.sprite = normalSprite;
    }
}
