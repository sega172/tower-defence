using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] Material _materialOk;
    [SerializeField] Material _materialError;

    private BuildingPreview _preview;
    private bool _error;

    public void SetPreview(BuildingPreview preview)
    {
        if(_preview != null)
            Destroy(_preview);

        _preview = Instantiate(preview, transform);
        UpdateMaterial(_error);
    }    

    public void UpdateMaterial(bool error)
    {
        if(_error == error)
        {
            return;
        }

        _preview.meshRenderer.material = error ? _materialError : _materialOk;
        _error = error;
    }

    public void Enable(bool enabled)
    {
        gameObject.SetActive(enabled);
    }

    private void OnDisable()
    {
        if (_preview != null)
        {
            Destroy(_preview.gameObject);
            _preview = null;
        }
    }

}
