using UnityEngine;

public class Grid_Spawner : MonoBehaviour
{
    public GameObject Blockprefab;
    public int rows = 3;
    public int colums = 8;
    public float spacing = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnGrid()
    {
        //figuring out the bounds of the camera
        Camera cam = Camera.main;
        float screenheight = cam.orthographicSize * 2f;
        float screenwidth = screenheight * cam.aspect;

        //calculating the starting position
        float startX = -screenwidth / 2f + spacing / 2f;
        float startY = screenheight / 2f - spacing / 2f;
        Color[] basicColors = new Color[]
   {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow,
        Color.cyan,
        Color.magenta
   };

        // Spawn Grid
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < colums; col++)
            {
                Vector2 spawnPos = new Vector2(startX + (col * spacing), startY - (row * spacing));
                

                GameObject block =Instantiate(Blockprefab,spawnPos, Quaternion.identity);
                //change blocks to random colors

                SpriteRenderer spriterender = block.GetComponent<SpriteRenderer>();
                if (spriterender != null)
                {
                    spriterender.color = basicColors[Random.Range(0, basicColors.Length)];
                }
            }
        }

    }
}
