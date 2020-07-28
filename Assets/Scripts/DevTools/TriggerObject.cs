using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

namespace Gongulus
{
    public class TriggerObject : MonoBehaviour
    {
        public event Action<GameObject> EntityEntered;
        public event Action<GameObject> EntityExit;

        [SerializeField] bool debug;
        [SerializeField] UnityEvent actionOnTriggerEnter;
        [SerializeField] UnityEvent actionOnTriggerExit;
        // [SerializeField] TransformEvent transformOnTriggerEnter;
        // [SerializeField] RigidbodyEvent rigidbodyEnterEvent;
        // [SerializeField] Rigidbody targetRB;

        [SerializeField, ReorderableList] List<string> filterTag;
        List<GameObject> enteredEntities = new List<GameObject>();

        void OnValidate()
        {
            var col = GetComponent<Collider>();

            if (col == null)           
                gameObject.AddComponent<BoxCollider>().isTrigger = true;      
        }

        void OnTriggerEnter(Collider other)
        {
            var entity = other.gameObject;
            var isEntityValid = filterTag.Find(x => other.CompareTag(x)) != null;

            if (!isEntityValid)
                return;

            isEntityValid = enteredEntities.Find(x => x == entity) == null;

            if (!isEntityValid)
                return;

            enteredEntities.Add(entity);
            EntityEntered?.Invoke(entity);
            actionOnTriggerEnter?.Invoke();
            // transformOnTriggerEnter?.Invoke(transform);
            // rigidbodyEnterEvent?.Invoke(targetRB);
        }

        void OnTriggerExit(Collider other)
        {
            enteredEntities.Remove(other.gameObject);
            EntityExit?.Invoke(other.gameObject);
            actionOnTriggerExit?.Invoke();
            // transformOnTriggerEnter?.Invoke(transform);
        }
    }

    [Serializable]
    public class TransformEvent : UnityEvent<Transform> { }

    [Serializable]
    public class RigidbodyEvent : UnityEvent<Rigidbody> { }
}