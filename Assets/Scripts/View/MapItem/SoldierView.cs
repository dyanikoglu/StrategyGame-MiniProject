using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierView : MapItemView
{
    // Variables
    private List<Vector2> _openList;
    private List<Vector2> _closedList;

    // Features
    public bool FindATileToSpawn(Vector2 barracksPosition)
    {
        var searchStartPosition = barracksPosition - new Vector2(1, 1);
        var edgeLength = 5;
        RaycastHit2D hit;

        for (var i = (int)searchStartPosition.x; i < (int)searchStartPosition.x + edgeLength; i++)
        {
            // Check if tile is empty
            hit = Physics2D.Raycast(new Vector2(i + 0.5f, searchStartPosition.y + 0.5f), -Vector2.up);
            if (hit.collider != null)
            {
                // Tile is available
                if (hit.collider.GetComponent<MapView>())
                {
                    GetComponent<RectTransform>().anchoredPosition =
                        new Vector2(i, searchStartPosition.y);
                    return true;
                }
            }

            // Check symmetric position
            hit = Physics2D.Raycast(new Vector2(i + 0.5f, searchStartPosition.y + (edgeLength - 1) + 0.5f),
                -Vector2.up);
            if (hit.collider != null)
            {
                // Tile is available
                if (hit.collider.GetComponent<MapView>())
                {
                    GetComponent<RectTransform>().anchoredPosition =
                        new Vector2(i, searchStartPosition.y + (edgeLength - 1));
                    return true;
                }
            }

            // We haven't reached end of the horizontal edge, keep going
            if (i != (int) searchStartPosition.x + edgeLength - 1) continue;

            // Horizontal edge is full, try searching tiles on vertical edge
            for (var j = (int)searchStartPosition.y; j < (int)searchStartPosition.y + edgeLength; j++)
            {
                // Check if tile is empty
                hit = Physics2D.Raycast(new Vector2(i + 0.5f, j + 0.5f),
                    -Vector2.up);
                if (hit.collider != null)
                {
                    // Tile is available
                    if (hit.collider.GetComponent<MapView>())
                    {
                        GetComponent<RectTransform>().anchoredPosition = new Vector2(i, j);
                        return true;
                    }
                }

                // Check symmetric position
                hit = Physics2D.Raycast(new Vector2(i - (edgeLength - 1) + 0.5f, j + 0.5f),
                    -Vector2.up);
                if (hit.collider != null)
                {
                    // Tile is available
                    if (hit.collider.GetComponent<MapView>())
                    {
                        GetComponent<RectTransform>().anchoredPosition =
                            new Vector2(i - (edgeLength - 1), j);
                        return true;
                    }
                }

                // We haven't reached end of the horizontal edge, keep going
                if (j != (int) searchStartPosition.y + edgeLength - 1) continue;

                // Increase edge length, restart the empty tile search algorithm with new value
                edgeLength += 2;
                searchStartPosition = new Vector2(searchStartPosition.x - 1,
                    searchStartPosition.y - 1);
                i = (int)searchStartPosition.x - 1;
                break;
            }
        }

        // No tile to spawn, return false
        return false;
    }

    // Events
    public override void OnClicked()
    {
        Notify("soldierMapItem.onClicked", GetID());
    }
}
