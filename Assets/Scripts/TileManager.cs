using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    [SerializeField]
    private int rows = 5;
    [SerializeField]
    private int cols = 8;
    [SerializeField]
    private float tileSize = 1;

    //public GameObject uiManager = new GameObject();

    public List<GameObject> tiles = new List<GameObject>();

    // Start is called before the first frame update
    void Start() {
        DrawGrid();
    }

    private void DrawGrid() {
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("Tile_Box"));
        int nameIndex = 0;
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {

                GameObject tile = (GameObject)Instantiate(referenceTile, transform);

                float posX = col * tileSize;
                float posY = row * -tileSize;

                tile.transform.position = new Vector2(posX, posY);
                tile.name = nameIndex.ToString();
                nameIndex++;
                tiles.Add(tile);
            }
        }
        Destroy(referenceTile);
        float gridW = cols * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
    }

    // Update is called once per frame
    void Update() {

    }
}
