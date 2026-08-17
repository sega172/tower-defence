using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _buildingPrefab;
    [SerializeField] LayerMask _layerMask;
    [SerializeField] Pointer _pointer;
    private Regime regime;

    

    List<Vector3> occupiedCells;

    private Building selectedBuilding;

    private void Start()
    {
        _pointer.Enable(false);
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

                _pointer.transform.position = position;

                _pointer.UpdateMaterial(error: false);
            }
            else
            {
                _pointer.UpdateMaterial(true);
            }

            bool canBuild = true;
            if(occupiedCells.Contains(_pointer.transform.position))
            {
                _pointer.UpdateMaterial(error: true);
                canBuild = false;
            }

            if (Input.GetMouseButtonUp(0) && canBuild)
            {
                Build(_pointer.transform.position);
                _pointer.Enable(false);
                regime = Regime.Idle;
            }
            //if (Input.GetMouseButtonDown(1))
            //{
            //    _pointer.Enable(false);
            //    regime = Regime.Idle;
            //}
        }
    }

    public void BuildingRegime(Building building)
    {
        _pointer.SetPreview(building.preview);
        _pointer.Enable(true);
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