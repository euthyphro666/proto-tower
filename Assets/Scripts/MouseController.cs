using System;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class MouseController : MonoBehaviour
{
    enum ClickMode
    {
        Select,
        PlacingTowers
    }
    
    public Camera mainCamera;
    public List<string> clickableLayers;
    
    
    private Player playerInput;
    private bool interactPressed;
    private bool cancelPressed;
    private bool modifierPressed;
    private Mouse mouse;
    private int clickLayer;
    private ClickMode clickMode;

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
        
        foreach (var layer in clickableLayers)
        {
            clickLayer |= 1 << LayerMask.NameToLayer(layer);
        }
        
        clickMode = ClickMode.PlacingTowers;
    }

    // Update is called once per frame
    void Update () {
        GetInput();
        ProcessInput();
    }

    private void GetInput() {
        interactPressed = playerInput.GetButtonDown(Constants.Actions.Interact);
        cancelPressed = playerInput.GetButtonDown(Constants.Actions.Cancel);
        modifierPressed = playerInput.GetButton(Constants.Actions.Modifier);
    }

    private void ProcessInput() {

        if (cancelPressed)
        {
            // Todo remaining deselect tasks here
            clickMode = ClickMode.Select;
            Debug.Log($"Cancelling selection");
            return;
        }
        
        // Process raycast to see what was clicked
        if(interactPressed) {
            var ray = mainCamera.ScreenPointToRay(mouse.screenPosition);
            
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, clickLayer)) {
                var objectHit = hit.transform;
                
                //TODO perform some action
                //TODO For when we get to the UI https://answers.unity.com/questions/1410936/how-to-prevent-a-ui-element-from-clicking-the-game.html

                switch (clickMode)
                {
                    case ClickMode.Select:
                        Debug.Log($"Selected {objectHit.name}");
                        if (objectHit.gameObject.layer == LayerMask.NameToLayer(Constants.LayerNames.Tower))
                        {
                            Debug.Log($"Hey {objectHit.name} is a tower! We should display its stats or something.");
                        }
                        break;
                    case ClickMode.PlacingTowers:
                        HandlePlacingTower();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    private void HandlePlacingTower()
    {
        Debug.Log("Placing tower!");
        
        if (!modifierPressed)
        {
            clickMode = ClickMode.Select;
            Debug.Log("Aight no more towers to place!");
            return;
        }
        
        Debug.Log("Looks like we're placing more towers, hell yeah!");
    }
}
