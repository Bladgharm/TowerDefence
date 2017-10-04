using System;
using UnityEngine;

namespace Assets.Code
{
    public class TestInputManager : MonoBehaviour
    {
        public WorldGrid GridComponent;
        public GameObject TurretPrefab;
        public Player CurrentPlayer;

        private bool _isBuildState = false;
        private GameObject _buildItem;

        private float _lastShootTime = 0f;
        private float _coolDown = 0.5f;

        private void Start()
        {
            if (CurrentPlayer != null)
            {
                CurrentPlayer.OnPlayerStateChanged += OnPlayerStateChanged;
            }
        }

        private void OnPlayerStateChanged(PlayerState playerState)
        {
            _isBuildState = playerState == PlayerState.Build;

            if (!_isBuildState)
            {
                ClearBuildItem();
            }
        }

        private void Update()
        {
            UpdateInput();
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
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (CurrentPlayer != null)
                {
                    CurrentPlayer.ChangePlayerState(_isBuildState ? PlayerState.None : PlayerState.Build);
                }
            }

            if (_isBuildState)
            {
                BuildStateUpdate();
            }
            else
            {
                NoneStateUpdate();
            }
        }

        private void BuildStateUpdate()
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

        private void NoneStateUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(mouseRay, out hitInfo))
                {
                    var resourceComponent = hitInfo.transform.GetComponent<ResourceItem>();
                    if (resourceComponent != null)
                    {
                        //Debug.LogFormat("Hit +1 {0}", resourceComponent.ItemResource.ToString());
                        resourceComponent.HitItem();
                        if (CurrentPlayer != null && CurrentPlayer.PlayerInventory != null)
                        {
                            CurrentPlayer.PlayerInventory.AddResource(resourceComponent.ItemResource);
                        }
                    }
                }
            }
        }

        private void Harvest()
        {
            if (_lastShootTime <= 0)
            {
                _lastShootTime = _coolDown;
                Debug.Log("Shoot");
            }
            else
            {
                _lastShootTime -= Time.deltaTime;
            }
        }
    }
}