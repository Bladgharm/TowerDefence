using UnityEngine;

namespace Assets.Code
{
    public class TestInputManager : MonoBehaviour
    {
        public WorldGrid GridComponent;
        public GameObject TurretPrefab;

        private bool _isBuildState = false;
        private GameObject _buildItem;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _isBuildState = !_isBuildState;

                if (!_isBuildState)
                {
                    ClearBuildItem();
                }
            }

            if (_isBuildState)
            {
                UpdateInput();
            }
        }

        private void ClearBuildItem()
        {
            if (_buildItem != null)
            {
                Destroy(_buildItem);
                _buildItem = null;
            }
        }

        private void UpdateInput()
        {
            if (_buildItem == null)
            {
                _buildItem = Instantiate(TurretPrefab, Vector3.zero, Quaternion.identity);
            }

            var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(mouseRay, out hitInfo))
            {
                var snappedToGrid = GridComponent.SnapToGridCell(hitInfo.point);
                _buildItem.transform.position = snappedToGrid;

                if (Input.GetMouseButtonDown(0))
                {
                    var turret = Instantiate(TurretPrefab, snappedToGrid, Quaternion.identity);
                    GridComponent.FillGridCellAtPoint(snappedToGrid);
                }
            }

        }
    }
}