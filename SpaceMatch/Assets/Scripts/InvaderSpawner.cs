using System;
using System.Collections.Generic;
using System.Linq;
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

    private List<Invader> _invaders = new List<Invader>();
    private List<Invader> _destroyedInvaders = new List<Invader>();

    private void Awake()
    {
        var prefab = Resources.Load<Invader>("InvaderPrefab");

        for (var row = 0; row < rows; row++)
        {
            var width = separation * (columns - 1);
            var height = separation * (rows - 1);
            var centering = new Vector2(-width / enemyWidth, -height / enemyHeight);
            var rowPosition = new Vector3(centering.x, centering.y + row * separation, 0.0f);

            for (var col = 0; col < columns; col++)
            {
                var position = rowPosition;
                position.x += col * separation;
                SpawnInvader(prefab, row, col, position);
            }
        }
    }

    private void SpawnInvader(Invader prefab, int row, int col, Vector3 position)
    {
        var color = colors[UnityEngine.Random.Range(0, colors.Count)];
        var invader = Instantiate(prefab, invadersParent);
        _invaders.Add(invader);
        invader.OnStart(CheckInvaderNeighbours, color, new Tuple<int, int>(row,col), position);
    }

    private void CheckInvaderNeighbours(Tuple<int,int> position, Color color)
    {
        foreach (var invader in _invaders.Where(invader => invader.color == color))
        {
            if (_destroyedInvaders.All(inv => inv != invader) && IsNeighbour(position, invader))
            {
                invader.Death();
            }
        }
    }
    
    private bool IsNeighbour(Tuple<int,int> position, Invader invader)
    {
        var (item1, item2) = position;
        if (invader.matrixPosition.Item1 == item1 + 1 && 
            invader.matrixPosition.Item2 == item2 ||
            invader.matrixPosition.Item1 == item1 - 1 && 
            invader.matrixPosition.Item2 == item2 ||
            invader.matrixPosition.Item2 == item2 + 1 && 
            invader.matrixPosition.Item1 == item1 ||
            invader.matrixPosition.Item2 == item2 - 1 && 
            invader.matrixPosition.Item1 == item1)
        {
            _destroyedInvaders.Add(invader);
            return true;
        }
        return false;
    }
}
