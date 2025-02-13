using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piece : MonoBehaviour
{
    [SerializeField]
    private Transform pieceholder;
    private Vector2 initPosition;
    private float deltaX, deltaY;
    public bool right;

    void Start()
    {
        initPosition = transform.position;
        right = false;
    }

    void OnMouseDown()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
        {
            deltaX = touchPos.x - transform.position.x;
            deltaY = touchPos.y - transform.position.y;
        }
    }

    void OnMouseDrag()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
        {
            transform.position = new Vector2(touchPos.x - deltaX, touchPos.y - deltaY);
        }
    }

    void OnMouseUp()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Mathf.Abs(transform.position.x - pieceholder.position.x) <= 0.5f &&
            Mathf.Abs(transform.position.y - pieceholder.position.y) <= 0.5f)
        {
            transform.position = new Vector2(pieceholder.position.x, pieceholder.position.y);
            right = true;
        }
        else
        {
            transform.position = new Vector2(touchPos.x - deltaX, touchPos.y - deltaY);
            right = false;
        }
    }
}
