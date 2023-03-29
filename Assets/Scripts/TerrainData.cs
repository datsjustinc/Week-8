using UnityEngine;

[CreateAssetMenu(fileName = "TerrainData", menuName = "Scriptable Object/Terrain Data", order = 0)]
public class TerrainData : ScriptableObject
{
   // terrain dimensions
   [SerializeField] private int width;
   [SerializeField] private int height;
   
   // dimension getters and setters
   public int Width { get => width; set => width = value; }
   public int Height { get => height; set => height = value; }
}
