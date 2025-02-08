using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    [SerializeField]
    float index_stars;
    void Start()
    {
        if (GameManager.TOTAL_STARS >= index_stars)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }
}
