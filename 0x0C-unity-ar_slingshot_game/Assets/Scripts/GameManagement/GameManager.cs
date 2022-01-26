using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameManager : MonoBehaviour
{
    private State currentState;

    public ARSessionOrigin sessionOrigin;
    public GameObject aRSession;
    public ARPlaneManager planeManager;
    public ARRaycastManager raycastManager;
    public TargetManager targetManager;
    [HideInInspector] public ARPlane gamePlane = null;

    public GameObject UICanvas;
    [HideInInspector] public List<GameObject> UIList;

    public GameObject ammoLauncher;

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
        if (currentState != null)
            currentState.Tick();
    }

    public void SetState(State state)
    {
        if (currentState != null)
            Debug.Log($"Exiting state {currentState}");
        if (currentState != null)
            currentState.OnStateExit();

        Debug.Log($"Setting state to {state}");
        currentState = state;

        Debug.Log($"Entering state {currentState}");
        if (currentState != null)
            currentState.OnStateEnter();
    }
}
