using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InvaderManager : MonoBehaviour
{
    public Bullet enemyBulletPrefab;
    
    public Transform invadersParent;
    public int rows = 5;
    public int columns = 11;
    public float timerAttack;
    public float MoveSpeed = 5f;
    public float distanceBtw;
    public float enemyHeight;
    public float enemyWidth; 
    public List<Color> colors = new List<Color>();

    private GameManager _gameManager;
    private Vector3 _direction = Vector2.right;
    private List<Invader> _invaders = new List<Invader>();
    private List<Invader> _destroyedInvaders = new List<Invader>();
    private int _destroyedInvadersCount = 0;
    private float _timeToShoot = 2f;

    private void Awake()
    {
        GenerateInvadersGrid();
    }
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        timerAttack = Time.deltaTime;
    }
    private void Update()
    {
        timerAttack += Time.deltaTime;
        Move();
        EnemyAttack();
        CheckInvadersAmount();
    }

    private void SpawnInvader(Invader prefab, int row, int col, Vector3 position)
    {
        var color = colors[UnityEngine.Random.Range(0, colors.Count)];
        var invader = Instantiate(prefab, invadersParent);
        _invaders.Add(invader);
        invader.OnStart(CheckInvaderNeighbours, color, new Tuple<int, int>(row, col), position);
    }

    private void CheckInvaderNeighbours(Invader invader)
    {
        _destroyedInvadersCount++;
        _destroyedInvaders.Add(invader);

        foreach (var inv in _invaders.Where(inv => inv.color == invader.color))
        {
            if (_destroyedInvaders.All(inv2 => inv2 != inv) && IsNeighbour(invader.matrixPosition, inv))
            {
                inv.Death();
            }
        }
        _destroyedInvadersCount--;
        
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
            return true;
        }
        return false;
    }

    private void Move()
    {
        transform.position += _direction * MoveSpeed * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in transform)
        {
            
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            
            if (_direction == Vector3.right && invader.position.x >= (rightEdge.x - 0.5f))
            {
                AdvanceRow();
                break;
            }
            else if (_direction == Vector3.left && invader.position.x <= (leftEdge.x + 0.5f))
            {
                AdvanceRow();
                break;
            }
        }
    }
    private void AdvanceRow()
    {
        _direction.x *= -1.0f;
        Vector3 position = transform.position;
        position.y -= 1.0f;
        transform.position = position;
    }

    private void EnemyAttack()
    {
        if (timerAttack > _timeToShoot)
        {
            var randomInvader = UnityEngine.Random.Range(0, _invaders.Count);

            for (var i = 0; i < _invaders.Count; i++)
            {
                if (_invaders[i].gameObject.activeInHierarchy && randomInvader == i)
                {
                    Instantiate(enemyBulletPrefab, _invaders[i].transform.position, Quaternion.identity);
                    timerAttack = 0;
                    break;
                }
            }  
        }
    }

    private void GenerateInvadersGrid()
    {
        var prefab = Resources.Load<Invader>("InvaderPrefab");

        for (var row = 0; row < rows; row++)
        {
            var width = distanceBtw * (columns - 1);
            var height = distanceBtw * (rows - 1);
            var centering = new Vector2(-width / enemyWidth, -height / enemyHeight);
            var rowPosition = new Vector3(centering.x, centering.y + row * distanceBtw, 0.0f);

            for (var col = 0; col < columns; col++)
            {
                var position = rowPosition;
                position.x += col * distanceBtw;
                SpawnInvader(prefab, row, col, position);
            }
        }
    }
    private void CheckInvadersAmount()
    {
        if (_destroyedInvadersCount == 0 && _destroyedInvaders.Count > 0)
        {
            _gameManager.CountScore(_destroyedInvaders.Count);
            _destroyedInvaders.Clear();
        }
        if (_invaders.All(invader => !invader.gameObject.activeInHierarchy))
        {
            _gameManager.WinLevel();
            _invaders.Clear();
            foreach (Transform child in invadersParent)
            { 
                GameObject.Destroy(child.gameObject);
            }
            GenerateInvadersGrid();
        }
    }
}
