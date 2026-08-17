using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] Builder builder;
    [SerializeField] Building building;
    private Button button;

    private bool _isDragging = false;
    private bool _wasInside = true;
    private RectTransform _rectTransform;
    
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    private void BuildCommand()
    {
        builder.BuildingRegime(building);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
        _wasInside = true; // При нажатии курсор точно внутри
        Debug.Log("Начали тащить");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging) return;

        bool isInside = RectTransformUtility.RectangleContainsScreenPoint(
            _rectTransform,
            eventData.position,
            eventData.pressEventCamera
        );

        if (_wasInside && !isInside)
        {
            OnExitWhileDragging(); 
            _wasInside = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;
    }

    private void OnExitWhileDragging()
    {
        BuildCommand();
    }
}
