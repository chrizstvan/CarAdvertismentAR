using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;

public class TapToPlace : MonoBehaviour
{
    public GameObject placementIndicator;
    public GameObject objectToPlace;

    ARSessionOrigin _aROrigin;
    Pose _posePlacement;
    bool _placementPlacaeIsValid = false;

    // Start is called before the first frame update
    void Start()
    {
        _aROrigin = FindObjectOfType<ARSessionOrigin>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();


        if (_placementPlacaeIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        Instantiate(objectToPlace, _posePlacement.position, _posePlacement.rotation);
    }

    private void UpdatePlacementIndicator()
    {
        if (_placementPlacaeIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(_posePlacement.position, _posePlacement.rotation);
        }
        else
            placementIndicator.SetActive(false);
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        _aROrigin.Raycast(screenCenter, hits, TrackableType.Planes);

        _placementPlacaeIsValid = hits.Count > 0;
        if (_placementPlacaeIsValid)
        {
            _posePlacement = hits[0].pose;
        }
    }
}
