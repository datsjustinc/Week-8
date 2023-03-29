using UnityEngine;

[CreateAssetMenu(fileName = "IslandData", menuName = "Scriptable Object/Island Data", order = 0)]
public class IslandData : ScriptableObject
{
   // island block size 
   [SerializeField] private int width;
   [SerializeField] private int height;

   // island size getters and setters
   public int Width { get => width; set => width = value; }
   public int Height { get => height; set => height = value; }
}
