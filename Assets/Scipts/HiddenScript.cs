using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenScript : MonoBehaviour
{
    private Tilemap tilemap;
    private TilemapRenderer tilemapRenderer;
    private Color color = Color.white;

    private void Awake() 
    {
        tilemap = GetComponent<Tilemap>();
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            color.a = 0.3f;
            tilemapRenderer.sortingLayerName = "Terrain";
            tilemap.color = color;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            color.a = 1f;
            tilemapRenderer.sortingLayerName = "Default";
            tilemap.color = color;
        }
    }

    
}
