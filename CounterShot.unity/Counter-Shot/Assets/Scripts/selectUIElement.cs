using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class selectUIElement : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private EventSystem eventsystem;
    [SerializeField] private Selectable elementToSelect;

    [Header("Visualization")]
    [SerializeField] private bool showVisualization;
    [SerializeField] private Color navigationColor = Color.cyan;

    private void OnDrawGizmos()
    {
        if (!showVisualization)
            return;

        if (elementToSelect == null)
            return;
        Gizmos.color = navigationColor;
        Gizmos.DrawLine(gameObject.transform.position, elementToSelect.gameObject.transform.position);
    }
    private void Reset()
    {
        eventsystem = FindObjectOfType<EventSystem>();
        if (eventsystem == null)
            Debug.Log("no event system");
    }

    public void JumpToElement()
    {
        eventsystem.SetSelectedGameObject(elementToSelect.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
