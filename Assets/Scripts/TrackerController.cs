using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TrackerController : MonoBehaviour
{
    [SerializeField]
    Mediator mediator;

    ARPlaneManager _planeManager;
    ARPointCloudManager _cloudManager;

    [HideInInspector]
    public bool isClicked = false;

    [HideInInspector]
    public bool isTracked = false;

    public UnityEvent EnableTracking, DisableTracking;

    private void Awake()
    {
        _cloudManager = mediator._placeOnPlane._cloudManager;
        _planeManager = mediator._placeOnPlane._planeManager;

        _planeManager.planesChanged += PlaneTrackingState;
    }

    private void OnDestroy()
    {
        _planeManager.planesChanged -= PlaneTrackingState;
    }

    private void PlaneTrackingState(ARPlanesChangedEventArgs eventArgs)
    {
        foreach(var _event in eventArgs.added)
        {
            isTracked = true;
        }

        foreach(var _event in eventArgs.updated)
        {
                isTracked = true;
        }
    }

    public void StopTrackingMg()
    {
        if (isTracked)
        {
            isClicked = !isClicked;

            if (isClicked)
            {
                foreach (var plane in _cloudManager.trackables)
                {
                    plane.gameObject.SetActive(false);
                }

                foreach (var plane in _planeManager.trackables)
                {
                    plane.gameObject.SetActive(false);
                }

                _cloudManager.enabled = false;
                _planeManager.enabled = false;

                DestroySpawnedObj();

                DisableTracking.Invoke();
            }
            else
            {
                foreach (var plane in _cloudManager.trackables)
                {
                    plane.gameObject.SetActive(true);
                }

                foreach (var plane in _planeManager.trackables)
                {
                    plane.gameObject.SetActive(true);
                }

                _cloudManager.enabled = true;
                _planeManager.enabled = true;

                EnableTracking.Invoke();
            }
        }

    }

    public void DestroySpawnedObj()
    {
        GameObject[] spawnedObj = GameObject.FindGameObjectsWithTag("Cube");

        foreach (GameObject obj in spawnedObj)
        {
            Destroy(obj);
        }

    }


}
