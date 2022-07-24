using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TreasureRun.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private string itemName;
        [SerializeField] private bool isInteractable;
        [SerializeField] private bool isGrabable;

        private Vector3 initialPosition;
        private Quaternion initialRotation;

        private void Start()
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;
        }

        public string GetItemName() 
        {
            return itemName;
        }

        public virtual bool Grab(Vector3 position) 
        {
            // should change into a lerp, for now it just sets
            transform.position = position;
            return true;
        }

        public virtual void PutDown() 
        {
            // Should be a lerp, but for now it just sets
            transform.position = initialPosition;
            transform.rotation = initialRotation;
        }

        public virtual bool Interact() 
        {
            return false;
        }

        public virtual bool Select() 
        {
            return false;
        }
    }
}