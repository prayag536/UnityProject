using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
public class PlayerData : MonoBehaviour
{
    public string Name;
    public int id;
    public List<Transform> Path;
    public Pawns[] PawnsData;
    public bool HasWon;


    private void Start()
    {
       
    }
    public void PlayerDataInit(string name, int id)
    {
        Name = name;
        this.id = id;
      
    }
    


    public async Task<bool> MovePawn(int PawnIndex,int DiceValue)
    {
        var TargetIndex = PawnsData[PawnIndex].PathIndex + DiceValue;

        while (PawnsData[PawnIndex].PathIndex != TargetIndex)
        {
            PawnsData[PawnIndex].transform.DOJump(Path[PawnsData[PawnIndex].PathIndex+1].position, 2, 1, 0.3f);
            await Task.Delay(300);
            PawnsData[PawnIndex].PathIndex++;

            
        }

        PawnsData[PawnIndex].transform.parent = Path[PawnsData[PawnIndex].PathIndex];
        Debug.Log(TargetIndex);
        return true;
    }
}
