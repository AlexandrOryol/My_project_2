using System.Runtime.CompilerServices;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private float _checkDistance;
    [SerializeField] private Transform _raycastPoint;
    [SerializeField] private Block _blockPrefab;
    [SerializeField] private BuildPreview _buildPreview;

    private RaycastHit _hitinfo;

    private Vector3 BuildPosition => _hitinfo.transform.position + _hitinfo.normal;

    private void Update()
    {
        if (_hitinfo.transform == null)
            return;

        if (_hitinfo.transform.GetComponent<Block>() == null)
            return;

        if (Input.GetMouseButtonDown(0))
            Build();
    }

    private void FixedUpdate()
    {
        if(Physics.Raycast(_raycastPoint.position, _raycastPoint.forward, out _hitinfo, _checkDistance))
        {
            if(_buildPreview.IsActive == false)
            {
                _buildPreview.Enable();
            }

            _buildPreview.SetPosition(BuildPosition);
        }
        else
        {
            _buildPreview.Disable();
        }
    }

    private void Build()
    {
        Vector3 position = BuildPosition;

        Instantiate(_blockPrefab, position, Quaternion.identity);
    }
}
