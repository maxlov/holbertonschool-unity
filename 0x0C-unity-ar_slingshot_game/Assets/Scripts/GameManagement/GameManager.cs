using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameManager : MonoBehaviour
{
    private State currentState;
    public ARPlaneManager planeManager;
    public ARRaycastManager raycastManager;
    public GameObject UICanvas;
    [HideInInspector] public List<GameObject> UIList;
    [HideInInspector] public ARPlane gamePlane;

    private void Awake()
    {
        foreach (Transform child in UICanvas.transform)
            UIList.Add(child.gameObject);
    }

    void Start()
    {
        SetState(new PlaneTrackingState(this));
    }

    void Update()
    {
        currentState.Tick();
    }

    public void SetState(State state)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = state;

        if (currentState != null)
            currentState.OnStateEnter();
    }
}
