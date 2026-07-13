using UnityEngine;

public class Pawns : MonoBehaviour
{
 
   public int playerId;

    public Vector3 IniPos;

    public int PathIndex;

    public bool IsInHome;

    public int pawnIndex;

    private void Awake()
    {
        IniPos = transform.position;
        IsInHome = true;
        PathIndex = -1;
        pawnIndex = transform.GetSiblingIndex();
    }
    public virtual void MovePawn(int index)
    {
        Debug.Log("movepawn pawn class");
    }

    public void ResetPawn()
    {
        transform.position = IniPos;
        IsInHome = true;
        PathIndex = -1;
    }

    private void OnMouseDown()
    {
        Debug.Log("Pawn Clicked pawn base class");
        //MovePawn(pawnIndex);
        PlayerBoardDataController.TakeTurnAction.Invoke(pawnIndex);
    }
}