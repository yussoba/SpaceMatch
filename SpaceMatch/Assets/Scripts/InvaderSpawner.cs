using System;
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

    private int[,] _invadersMatrix;
    private List<Invader> _invaders = new List<Invader>();

    private void Awake()
    {
        var prefab = Resources.Load<Invader>("InvaderPrefab");
        _invadersMatrix = new int[rows, columns];

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
                var color = colors[UnityEngine.Random.Range(0, colors.Count)];
                _invaders.Add(invader);
                invader.onDestroyed += CheckInvaderNeighbours;
                invader.position = new Tuple<int, int>(row,col);
                invader.color = color;
                var position = rowPosition;
                position.x += col * separation;
                invader.transform.position = position;
                invader.sprite.color = color;
            }
        }
    }

    private void CheckInvaderNeighbours(Tuple<int,int> position, Color color)
    {
        foreach (var invader in _invaders)
        {
            if (IsNeighbour(invader) && invader.color == color)
            {
                invader.Death();
            }
        }
    }
    public bool IsNeighbour(Invader invader)
    {
        var invaderNeighbours = new List<Invader>();
        var isNeighbour = false;
        int minX = Math.Max(invader.position.Item1 - 1, _invadersMatrix.GetLowerBound(0));
        int maxX = Math.Min(invader.position.Item1 + 1, _invadersMatrix.GetUpperBound(0));             
        int minY = Math.Max(invader.position.Item2 - 1, _invadersMatrix.GetLowerBound(1));             
        int maxY = Math.Min(invader.position.Item2 + 1, _invadersMatrix.GetUpperBound(1));             
                     
        for (int x = minX; x <= maxX; x++)             
        {                 
            for (int y = minY; y <= maxY; y++)                 
            {                       
                
                if (_invadersMatrix[x, y] == 1)
                {
                    return true;
                }
                    
            }             
        }             
        return isNeighbour;         
    }
}
