using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderSpawner : MonoBehaviour
{
    public int rows = 5;
    public int columns = 11;
    public float separation;
    public List<Color> colors = new List<Color>();

    private void Awake()
    {
        var prefab = Resources.Load<Invader>("InvaderPrefab");

        for (int row = 0; row < rows; row++)
        {
            //Ancho de la grilla de enemigos
            var width = separation * (columns - 1);
            //Alto de la grilla de enemigos
            var height = separation * (rows - 1);
            var centering = new Vector2(-width / 2, -height / 5);
            var rowPosition = new Vector3(centering.x, centering.y + (row * separation), 0.0f);

            for (int col = 0; col < columns; col++)
            {
                var invader = Instantiate(prefab);
                var position = rowPosition;
                position.x += col * separation;
                invader.transform.position = position;
                invader.sprite.color = colors[Random.Range(0, colors.Count)];
            }
        }
    }
}
