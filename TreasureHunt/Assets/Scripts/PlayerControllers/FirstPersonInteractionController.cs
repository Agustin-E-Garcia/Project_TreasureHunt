using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreasureRun.Items;

namespace TreasureRun.PlayerControllers
{
    public class FirstPersonInteractionController : MonoBehaviour
    {
        [SerializeField] private float interactionRange;
        [SerializeField] private Transform CameraTransform;
        [SerializeField] private Transform GrabbedItemTargetTransform;
        [SerializeField] private LayerMask interactableLayer;

        private Item _currentTarget;
        private bool _hasItemGrabbed;
        private InputState _input;

        public System.Action<Item> OnTargetItemChanged;

        private void Start()
        {
            _input = InputState.instance;
        }

        private void Update()
        {
            UpdateSelectedItem();

            if (_currentTarget != null) 
            {
                if (_input.interact) 
                    InteractWithItem();

                if (_input.grab)
                    GrabItem();
            }
        }

        private void UpdateSelectedItem() 
        {
            if (_hasItemGrabbed) return;

            RaycastHit hit;
            if (Physics.Raycast(CameraTransform.position, CameraTransform.forward, out hit, interactionRange, interactableLayer))
            {
                Item item = hit.collider.GetComponent<Item>();
                if (item.Select())
                {
                    _currentTarget = item;
                    Debug.LogError($"Selected {item.GetItemName()}");
                    OnTargetItemChanged?.Invoke(item);
                }
            }
            else if(_currentTarget != null) 
            {
                _currentTarget = null;
                OnTargetItemChanged?.Invoke(null);
            }
        }

        private void InteractWithItem() 
        {
            _currentTarget.Interact();
        }

        private void GrabItem() 
        {
            if (!_hasItemGrabbed && _currentTarget.Grab(GrabbedItemTargetTransform.position))
            {
                _hasItemGrabbed = true;
                _currentTarget.transform.parent = GrabbedItemTargetTransform;
            }
            else 
            {
                _hasItemGrabbed = false;
                _currentTarget.PutDown();
                _currentTarget.transform.parent = null;
            }
        }

        private void RotateItem() 
        {
            
        }

        private void OnDrawGizmos()
        {
            
        }
    }
}