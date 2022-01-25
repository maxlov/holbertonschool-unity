using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneSetupState : State
{
    private ARPlaneManager planeManager;
    private ARRaycastManager rayManager;

    private ARPlane targetPlane;

    private GameObject stateUI;

    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

    public PlaneSetupState(GameManager gameManager) : base(gameManager)
    {
        planeManager = gameManager.planeManager;
        rayManager = gameManager.raycastManager;
    }

    public override void Tick()
    {
        SelectPlane();
    }

    private void SelectPlane()
    {
        if (Input.touchCount == 0)
            return;
        if (rayManager.Raycast(Input.GetTouch(0).position, m_Hits, TrackableType.PlaneWithinPolygon))
        {
            targetPlane = planeManager.GetPlane(m_Hits[0].trackableId);
            gameManager.SetState(new GameSetupState(gameManager));
        }
    }

    public override void OnStateEnter()
    {
        foreach (var UIElement in gameManager.UIList)
            if (UIElement.name == "SelectUI")
                stateUI = UIElement;
        if (stateUI)
            stateUI.SetActive(true);
    }

    public override void OnStateExit()
    {
        foreach (var plane in planeManager.trackables)
            if (plane.trackableId != targetPlane.trackableId)
                plane.gameObject.SetActive(false);
        planeManager.enabled = false;
        gameManager.gamePlane = targetPlane;

        stateUI.SetActive(false);
    }
}
