using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuildButton : MonoBehaviour
{
    [SerializeField] Builder builder;
    [SerializeField] Building building;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(BuildCommand);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    private void BuildCommand()
    {
        builder.BuildingRegime(building);
    }
}
