using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private GameObject _buildingPrefab;
    [SerializeField] Transform _currentBuilding;
    [SerializeField] Transform pointer;
    [SerializeField] LayerMask _layerMask;
    private void Update()
    {

        if (_currentBuilding != null && Input.GetKeyDown(KeyCode.Alpha2))
        {
            Destroy(_currentBuilding.gameObject);
            _currentBuilding = null;
        }

        if (_currentBuilding == null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _currentBuilding = Instantiate(_buildingPrefab).transform;
            }
            else
            {
                return;
            }
        }

        Vector3 origin = Camera.main.transform.position;
        Vector3 direction = Camera.main.transform.forward;

        

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, layerMask: _layerMask))
        {
            print(hit.point);
            Vector3 position = hit.point;
            position = new Vector3(Mathf.RoundToInt(position.x),0, Mathf.RoundToInt(position.z));

            pointer.position = position;
            _currentBuilding.transform.position = position;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _currentBuilding = null;
        }
    }
}