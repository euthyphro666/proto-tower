using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public GameObject UiPrefab;
    public readonly int MaxHealth;

    private GameObject uiInstance;
    private int Health;

    public void Start()
    {
        Health = MaxHealth;
        //Creates the UI element and sets is as a child of the scene canvas
        var canvas = transform.root.GetComponentInChildren<Canvas>();
        var targetPos = Camera.main.WorldToScreenPoint(transform.position);
        uiInstance = Instantiate(UiPrefab, targetPos, Quaternion.identity, canvas.transform);
    }

    public void Update()
    {
        // Update ui position
        var targetPos = Camera.main.WorldToScreenPoint(transform.position);
        uiInstance.transform.position = targetPos;
    }

    public void DoDamage(int health)
    {
        Health -= health;
        if (Health <= 0)
        {
            Health = 0;
            // TODO change this to death animation
            Destroy(this.gameObject);
        }
        SetUiHealth(Health);
    }

    private void SetUiHealth(int health)
    {
        var rects = GetComponentsInChildren<RectTransform>();
        foreach (var rect in rects)
        {
            if (rect.name == "Health")
            {
                // Margin of 2 on each side with total being 100
                var percent = health / (float)MaxHealth;
                rect.SetRight(2f + (percent * 98f));
            }
        }
    }
}
