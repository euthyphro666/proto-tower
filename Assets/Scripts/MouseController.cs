using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class MouseController : MonoBehaviour
{
    public Camera mainCamera;
    public List<string> ClickableLayers;
    
    
    private Player playerInput;
    private bool interactPressed;
    private Mouse mouse;
    private int clickLayer;

    private void Awake()
    {
        playerInput = ReInput.players.GetPlayer(0); // Only one player ID: 0
        mouse = ReInput.controllers.Mouse;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!mainCamera)
        {
            Debug.LogError("MouseController script is missing the reference to the main Camera.\n" +
                           "This means mouse selections won't work cause we can't raycast. hashtag frowny face emoji.");
        }
        
        foreach (var layer in ClickableLayers)
        {
            clickLayer |= 1 << LayerMask.NameToLayer(layer);
        }
    }

    // Update is called once per frame
    void Update () {
        GetInput();
        ProcessInput();
    }

    private void GetInput() {
        interactPressed = playerInput.GetButtonDown(ActionConstants.Interact);
    }

    private void ProcessInput() {
        // Process raycast to see what was clicked
        if(interactPressed) {
            var ray = mainCamera.ScreenPointToRay(mouse.screenPosition);
            
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, clickLayer)) {
                var objectHit = hit.transform;
                Debug.Log($"Clicked {objectHit.name}");
                
                //TODO perform some action
            }
        }
    }
}
