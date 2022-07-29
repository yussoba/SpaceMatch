using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderSpawner : MonoBehaviour
{
    public Transform invadersParent;
    public int rows = 5;
    public int columns = 11;
    public float separation;
    public float enemyHeight;
    public float enemyWidth;
    public List<Color> colors = new List<Color>();

    private void Awake()
    {
        var prefab = Resources.Load<Invader>("InvaderPrefab");

        for (int row = 0; row < rows; row++)
        {
            //Enemy width position
            var width = separation * (columns - 1);
            //Enemy height position
            var height = separation * (rows - 1);
            var centering = new Vector2(-width / enemyWidth, -height / enemyHeight);
            var rowPosition = new Vector3(centering.x, centering.y + (row * separation), 0.0f);

            for (int col = 0; col < columns; col++)
            {
                var invader = Instantiate(prefab, invadersParent);
                var position = rowPosition; 
                position.x += col * separation;
                invader.transform.position = position;
                invader.sprite.color = colors[Random.Range(0, colors.Count)];
            }
        }
    }
}
