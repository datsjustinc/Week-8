using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "IslandData", menuName = "Scriptable Object/Island Data", order = 0)]
public class IslandData : ScriptableObject
{
   // island block size 
   private int _width;
   private int _height;

   // range used to calculate random width size
   [SerializeField] private int minWidth;
   [SerializeField] private int maxWidth;

   // range used to calculate random height size
   [SerializeField] private int minHeight;
   [SerializeField] private int maxHeight;

   // island size getters and setters
   public int Width { get => _width; set => _width = value; }
   public int Height { get => _height; set => _height = value; }

   public void Randomize()
   {
      _width = Random.Range(minWidth, maxWidth);
      _height = Random.Range(minHeight, maxHeight);

      Debug.Log(Width);
      Debug.Log(Height);
   }
}
