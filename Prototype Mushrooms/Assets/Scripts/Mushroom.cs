using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private Collider2D col;
    private Vector3 startDragPosition;
    // Start is called before the first frame update
    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        startDragPosition = transform.position;
        transform.position = GetMousePositionInWorldSpace();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePositionInWorldSpace();
    }

    private void OnMouseUp()
    {
        col.enabled = false;
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        col.enabled = true;

        if(hitCollider != null && hitCollider.TryGetComponent(out IDropArea dropArea)) //checking if it collided with drop area collider
        {
            dropArea.OnMushroomDrop(this);
        }
        else
        {
            transform.position = startDragPosition;
        }
    }

    public Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        return p;
    }
}
