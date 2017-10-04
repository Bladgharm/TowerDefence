using System;
using UnityEngine;

namespace Assets.Code
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private PlayerResourcesInventory _playerInventory;

        private PlayerState _currentPlayerState;

        public PlayerState CurrentPlayerState
        {
            get { return _currentPlayerState; }
        }

        public PlayerResourcesInventory PlayerInventory
        {
            get { return _playerInventory; }
        }

        public Action<PlayerState> OnPlayerStateChanged;

        public void ChangePlayerState(PlayerState newPlayerState)
        {
            if (_currentPlayerState != newPlayerState)
            {
                _currentPlayerState = newPlayerState;
                if (OnPlayerStateChanged != null)
                {
                    OnPlayerStateChanged.Invoke(_currentPlayerState);
                }
            }
        }
    }

    public enum PlayerState
    {
        None,
        Build
    }
}