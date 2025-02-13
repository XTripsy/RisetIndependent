using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RecipeManager : MonoBehaviour
{
    public GameObject[] recipes;
    public GameObject[] page;
    public List<List<piece>> Pieces;
    public List<piece> Fpieces;
    public List<piece> Spieces;
    public List<piece> Tpieces;
    private bool[] done;

    void Start()
    {
        done = new bool[recipes.Length];
        for (int i = 0; i < recipes.Length; i++)
        {
            recipes[i].SetActive(false);
            done[i] = false;
        }
    }

    void Update()
    {
        if (!done[0] && IsAnyPieceRight(Fpieces))
        {
            GameObject fpage = page[0];
            foreach (UnityEngine.Transform child in fpage.transform)
            {
                child.gameObject.SetActive(false);
            }
            recipes[0].SetActive(true);
            done[0] = true;
        }

        if (!done[1] && IsAnyPieceRight(Spieces))
        {
            GameObject spage = page[1];
            foreach (UnityEngine.Transform child in spage.transform)
            {
                child.gameObject.SetActive(false);
            }
            recipes[1].SetActive(true);
            done[1] = true;
        }
        if (!done[2] && IsAnyPieceRight(Tpieces))
        {
            GameObject tpage = page[2];
            foreach (UnityEngine.Transform child in tpage.transform)
            {
                child.gameObject.SetActive(false);
            }
            recipes[2].SetActive(true);
            done[2] = true;
        }
    }

    private bool IsAnyPieceRight(List<piece> pieces)
    {
        foreach (piece p in pieces)
        {
            
            if (p.right)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}

