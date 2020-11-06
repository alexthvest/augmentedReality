using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARObjectCreator : MonoBehaviour
{
    [SerializeField] 
    [InspectorName("Prefab")]
    private GameObject _prefab;

    private ARRaycastManager _raycastManager;

    private void Awake()
    {
        Debug.Assert(_prefab != null, "Prefab must not be null");
        
        _raycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (Input.touchCount == 0)
            return;

        var touchPosition = Input.GetTouch(0).position;
        var hits = new List<ARRaycastHit>();
        
        if (_raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            Instantiate(_prefab, hitPose.position, hitPose.rotation);
        }
    }
}