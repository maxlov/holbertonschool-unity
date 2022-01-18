using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GameManager : MonoBehaviour
{
    private State currentState;
    public ARPlaneManager planeManager;

    [System.Serializable]
    public class UIStruct
    {
        public string name;
        public GameObject UIAsset;
    }

    public UIStruct[] UIAssets;

    void Start()
    {
        SetState(new GameStartState(this));
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
