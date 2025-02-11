using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstpiece : MonoBehaviour
{
    [SerializeField]
    private Transform pieceholder;
    private Vector2 initposition;
    private float deltaX, deltaY;
    private static bool locked;

    // Start is called before the first frame update
    void Start()
    {
        initposition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && !locked)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchpos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchpos));
                    {
                        deltaX = touchpos.x - transform.position.x;
                        deltaY = touchpos.y - transform.position.y;
                    }
                    break;
                case TouchPhase.Moved:
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchpos)) ;
                    {
                       transform.position = new Vector2(touchpos.x - deltaX, touchpos.y - deltaY);
                    }
                    break;
                case TouchPhase.Ended:
                    if (Mathf.Abs(transform.position.x - pieceholder.position.x) <= 0.5f &&
                        Mathf.Abs(transform.position.y - pieceholder.position.y) <= 0.5f)
                    {
                        transform.position = new Vector2(pieceholder.position.x, pieceholder.position.y);
                        locked = true;
                    }
                    else
                    {
                        transform.position = new Vector2(initposition.x, initposition.y);
                    }
                    break;
}
        }
    }
}
