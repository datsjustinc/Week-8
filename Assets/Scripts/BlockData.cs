using UnityEngine;

namespace Generation
{
    /// <summary>
    /// This class manages the object surrounding data.
    /// </summary>
    public class BlockData : MonoBehaviour
    {
        // block perimeter type
        [SerializeField] private GameObject grassMiddle;
        [SerializeField] private GameObject grassCornerRt;
        [SerializeField] private GameObject grassCornerRb;
        [SerializeField] private GameObject grassCornerLt;
        [SerializeField] private GameObject grassCornerLb;
        
        // blocks perimeter linking availability
        [SerializeField] private bool upSide;
        [SerializeField] private bool downSide;
        [SerializeField] private bool leftSide;
        [SerializeField] private bool rightSide;
        
        // getters and setters to modify perimeter availability variables
        public bool UpSide { get => upSide; set => upSide = value; }
        public bool DownSide { get => downSide; set => downSide = value; }
        public bool LeftSide { get => leftSide; set => leftSide = value; }
        public bool RightSide { get => rightSide; set => rightSide = value; }

        /// <summary>
        /// This method checks neighboring sides availability when instantiated.
        /// </summary>
        private void Start()
        {
            CheckSides();
        }

        private void Update()
        {
            CheckSides();

            // only left and top direction open
            if (UpSide && !DownSide && LeftSide && !RightSide)
            {
                grassMiddle.SetActive(false);
                grassCornerRt.SetActive(false);
                grassCornerRb.SetActive(false);
                grassCornerLt.SetActive(true);
                grassCornerLb.SetActive(false);
            }

            // only top and right direction open
            else if (UpSide && !DownSide && !LeftSide && RightSide)
            {
                grassMiddle.SetActive(false);
                grassCornerRt.SetActive(true);
                grassCornerRb.SetActive(false);
                grassCornerLt.SetActive(false);
                grassCornerLb.SetActive(false);
            }
            
            // only left and down direction open
            else if (!UpSide && DownSide && LeftSide && !RightSide)
            {
                grassMiddle.SetActive(false);
                grassCornerRt.SetActive(false);
                grassCornerRb.SetActive(false);
                grassCornerLt.SetActive(false);
                grassCornerLb.SetActive(true);
            }
            
            // only down and right direction open
            else if (!UpSide && DownSide && !LeftSide && RightSide)
            {
                grassMiddle.SetActive(false);
                grassCornerRt.SetActive(false);
                grassCornerRb.SetActive(true);
                grassCornerLt.SetActive(false);
                grassCornerLb.SetActive(false);
            }

            // all four directions open
            else
            {
                grassMiddle.SetActive(true);
                grassCornerRt.SetActive(false);
                grassCornerRb.SetActive(false);
                grassCornerLt.SetActive(false);
                grassCornerLb.SetActive(false);
            }
        }

        private void CheckSides()
        {
            // grab current block value for use later on
            var pos = transform.position;
            var xPos = (int) pos.x;
            var zPos = (int) pos.z;
            
            // check above row, current row, and below row of current block
            for (var rows = zPos + 1; rows >= zPos - 1; rows--)
            {
                // check left column, current column, and right column of current block
                for (var columns = xPos - 1; columns <= xPos + 1; columns++)
                {
                    // up side
                    if (rows == zPos + 1 && columns == xPos)
                    {
                        // check position above for availability
                        UpSide = CheckPosition(rows, columns);
                    }
                    
                    // left side
                    if (rows == zPos && columns == xPos - 1)
                    {
                        // check position left for availability
                        LeftSide = CheckPosition(rows, columns);
                    }
                    
                    // right side
                    if (rows == zPos && columns == xPos + 1)
                    {
                        // check position right for availability
                        RightSide = CheckPosition(rows, columns);
                    }
                    
                    // down side
                    if (rows == zPos - 1 && columns == xPos)
                    {
                        // check position down for availability
                        DownSide = CheckPosition(rows, columns);
                    }
                }
            }
        }

        /// <summary>
        /// this function checks if indicated position is occupied by a block.
        /// </summary>
        /// <param name="zPos">z position to check block</param>
        /// <param name="xPos">x position to check block</param>
        /// <returns>status if block exists at position (x, y)</returns>
        private bool CheckPosition(int zPos, int xPos)
        {
            return !Physics.CheckBox(new Vector3(xPos, 0, zPos), new Vector3(0.45f, 0.45f, 0.45f));
        }
    }
}
