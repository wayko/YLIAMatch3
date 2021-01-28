﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FindMatches : MonoBehaviour
{
    private Board board;
    public List<GameObject> matchedPieces = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    public void FindAllMatches()
    {
        StartCoroutine(FindAllMatchesCo());
    }

    private IEnumerator FindAllMatchesCo()
    {
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                GameObject currentDot = board.allDots[i, j];
                if(currentDot != null)
                {
                    if(i > 0 && i < board.width -1)
                    {
                        GameObject leftDot = board.allDots[i - 1, j];
                        GameObject rightDot = board.allDots[i + 1, j];
                        if(leftDot != null && rightDot != null)
                        {
                            if(leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag)
                            {

                                if(currentDot.GetComponent<Dot>().isRowBomb || leftDot.GetComponent<Dot>().isRowBomb || rightDot.GetComponent<Dot>().isRowBomb)
                                {
                                    matchedPieces.Union(GetRowPieces(j));
                                }
                                if(!matchedPieces.Contains(leftDot))
                                {
                                    matchedPieces.Add(leftDot);
                                }
                                leftDot.GetComponent<Dot>().isMatched = true;
                                if (!matchedPieces.Contains(rightDot))
                                {
                                    matchedPieces.Add(rightDot);
                                }
                                rightDot.GetComponent<Dot>().isMatched = true;
                                if (!matchedPieces.Contains(currentDot))
                                {
                                    matchedPieces.Add(currentDot);
                                }
                                currentDot.GetComponent<Dot>().isMatched = true;
                            }
                        }
                    }
                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upDot = board.allDots[i, j +1];
                        GameObject downDot = board.allDots[i, j - 1];
                        if (upDot != null && downDot != null)
                        {
                            if (upDot.tag == currentDot.tag && downDot.tag == currentDot.tag)
                            {
                                if (!matchedPieces.Contains(upDot))
                                {
                                    matchedPieces.Add(upDot);
                                }
                                upDot.GetComponent<Dot>().isMatched = true;
                                if (!matchedPieces.Contains(downDot))
                                {
                                    matchedPieces.Add(downDot);
                                }
                                downDot.GetComponent<Dot>().isMatched = true;
                                if (!matchedPieces.Contains(currentDot))
                                {
                                    matchedPieces.Add(currentDot);
                                }
                                currentDot.GetComponent<Dot>().isMatched = true;
                            }
                        }
                    }
                }
            }
        }
    }

    List<GameObject> GetColumnPieces(int column)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.height; i++)
        {
            if(board.allDots[column, i]!= null)
            {
                dots.Add(board.allDots[column, i]);
                board.allDots[column, i].GetComponent<Dot>().isMatched = true;
            }
        }
            return dots;
    }

    List<GameObject> GetRowPieces(int row)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.width; i++)
        {
            if (board.allDots[i,row] != null)
            {
                dots.Add(board.allDots[i,row]);
                board.allDots[i, row].GetComponent<Dot>().isMatched = true;
            }
        }
        return dots;
    }
}
