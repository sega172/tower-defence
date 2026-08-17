using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _buildingPrefab;
    [SerializeField] Transform pointer;
    [SerializeField] LayerMask _layerMask;
    
    private Regime regime;

    [SerializeField] MeshRenderer _pointerRenderer;
    [SerializeField] Material _materialOk;
    [SerializeField] Material _materialError;

    List<Vector3> occupiedCells;

    private Building selectedBuilding;

    private void Start()
    {
        pointer.gameObject.SetActive(false);
        occupiedCells = new List<Vector3>();
    }

    private void Update()
    {
        if(regime == Regime.Building)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000, layerMask: _layerMask))
            {
                Vector3 position = hit.point;
                position = new Vector3(Mathf.RoundToInt(position.x), 0, Mathf.RoundToInt(position.z));

                pointer.position = position;
                _pointerRenderer.material = _materialOk;
            }
            else
            {
                _pointerRenderer.material = _materialError;
            }

            bool canBuild = true;
            if(occupiedCells.Contains(pointer.position))
            {
                _pointerRenderer.material = _materialError;
                canBuild = false;
            }

            if (Input.GetMouseButtonDown(0) && canBuild)
            {
                Build(pointer.position);
                pointer.gameObject.SetActive(false);
                regime = Regime.Idle;
            }
            if (Input.GetMouseButtonDown(1))
            {
                pointer.gameObject.SetActive(false);
                regime = Regime.Idle;
            }
        }
    }

    public void BuildingRegime(Building building)
    {
        pointer.gameObject.SetActive(true);
        regime = Regime.Building;
        selectedBuilding = building;
    }

    public void Build(Vector3 position)
    {
        Instantiate(selectedBuilding, position, Quaternion.identity);
        occupiedCells.Add(position);
    }

    enum Regime
    {
        Idle,
        Building,
    }
}