using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generation
{
    /// <summary>
    /// This class managers the procedural generation of the terrain map.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        // declare class as singleton
        private static GameManager _gameManager;
        
        // objects representing terrain data and material
        [SerializeField] private TerrainData terrainData;
        [SerializeField] private IslandData islandData;
        [SerializeField] private GameObject grassBase;
        [SerializeField] private Transform grassParent;

        // grid representing terrain map
        private bool[,] _gridStatus;
        private GameObject[,] _gridBlocks;
        
        // stack keeps tracking of base blocks
        private Stack<GameObject> _blockPaths;
        
        // game state variables
        public enum GameState {Load, Start};
        public GameState gameState;

        /// <summary>
        /// This method checks for duplicate manager objects.
        /// </summary>
        private void Awake()
        {
            if (_gameManager == null)
            {
                _gameManager = this;
            }
            else
            {
                Destroy(gameObject);
            }

            // initialize starting game state
            gameState = GameState.Load;
        }

        private void Start()
        {
            // defines 2D arrays for storing terrain block data and location
            _gridStatus = new bool[terrainData.Width, terrainData.Height];
            _gridBlocks = new GameObject[terrainData.Width, terrainData.Height];
            _blockPaths = new Stack<GameObject>();
            
            // loop through rows of grid, offset value of 1 to avoid index-out-of-bounds error
            for (int rows = 0; rows < terrainData.Height; rows++)
            {
                // loop through columns of grid, offset value of 1 to avoid index-out-of-bounds error
                for (int columns = 0; columns < terrainData.Width; columns++)
                {
                    // set low chance of generating a root block
                    _gridStatus[rows, columns] = Random.value > 0.98f;

                    // if position generates a root block
                    if (_gridStatus[rows, columns])
                    {
                        // create instance of root block set in position
                        _gridBlocks[rows, columns] = Instantiate(grassBase, 
                            new Vector3(columns, 0, rows), Quaternion.Euler(0, 0, 0), grassParent);
                        
                        _blockPaths.Push(_gridBlocks[rows, columns]);
                    }
                }
            }

            // initialize main game state
            gameState = GameState.Start;
            
            print(_blockPaths.Count);
        }

        private void Update()
        {
            Generate();
        }

        private void Generate()
        {
            if (_blockPaths.Count < 1)
            {
                Debug.Log("Block path count less than 1: " + _blockPaths.Count);
                return;
            }

            var currentBlock = _blockPaths.Pop().transform.position;
            var centerX = (int) currentBlock.x;
            var centerZ = (int) currentBlock.z;

            //print(centerZ - islandData.Height / 2 + " | " + centerZ + islandData.Height / 2);
            
            for (var rows = centerZ + islandData.Height / 2; rows >= centerZ - islandData.Height / 2; rows--)
            {
                Debug.Log("first loop entered");
                for (var columns = centerX - islandData.Width / 2; columns <= centerX + islandData.Width / 2; columns++)
                {
                    Debug.Log("second loop entered");
                    if (rows == centerZ && columns == centerX)
                    {
                        Debug.Log("continue in loop same position");
                        continue;
                    }
                    
                    var newBlock = Instantiate(grassBase, 
                        new Vector3(columns, 0, rows), Quaternion.Euler(0, 0, 0), grassParent);
                }
            }
        }
    }
}
