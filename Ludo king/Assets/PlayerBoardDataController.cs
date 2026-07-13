using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Unity.Properties;
using UnityEngine.Events;
using UnityEngine.UIElements;
using System.Threading;

public class PlayerBoardDataController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<PlayerData> PlayersData;
    public List<Transform>GreenPath, YellowPath, BluePath, RedPath;
    public Transform[] GreenPawns, YellowPawns, BluePawns, RedPawns;
    public Rigidbody DiceRb;
    public bool IsdiceRolled,CanDiceRoll;
    public int DiceNumber;
    public int CurrentPlayerIndex = 0;
    public static UnityAction<int> TakeTurnAction;


    private void OnEnable()
    {
        TakeTurnAction += async (int pawnIndex) => await TakeTurn(pawnIndex);
       
    }

    private void OnDisable()
    {
        TakeTurnAction -= async (int pawnIndex) => await TakeTurn(pawnIndex);

    }

    void Awake()
    {
       
    }

    void Start()
    {
        PlayersData[0].Path = GreenPath;
        PlayersData[1].Path = YellowPath;
        PlayersData[2].Path = BluePath;
        PlayersData[3].Path = RedPath;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsdiceRolled)
        {
            if (DiceRb.linearVelocity.sqrMagnitude == 0)
            {
                IsdiceRolled = false;
                DiceValue();
            }
        }
    }


    public async void RollDice()
    {
        if (!CanDiceRoll)
        {
            return;
        }
        Vector3 Pos = DiceRb.position;
        Pos = new Vector3 (0,10,0);
        DiceRb.position = Pos;

        DiceRb.linearVelocity = Vector3.zero;

        var direction = Random.RandomRange(1,20)>10?-1:1;
        float X = Random.Range(500, 1000) * direction;

        direction = Random.RandomRange(1, 20) > 10 ? -1 : 1;
        float Z = Random.Range(500, 1000) * direction;

        direction = Random.RandomRange(1, 20) > 10 ? -1 : 1;
        float Y = Random.Range(500, 1000) * direction;

        DiceRb.AddTorque(X,Y,Z,ForceMode.Impulse);
        await Task.Delay(500);
        IsdiceRolled = true;

    }

    //private void OnDrawGizmos()
    //{
    //    float rayLength = 1.0f;
    //    Vector3[] direction = new Vector3[] {
    //        DiceRb.transform.forward,
    //        -DiceRb.transform.forward,
    //        DiceRb.transform.right,
    //        -DiceRb.transform.right,
    //        DiceRb.transform.up,
    //        -DiceRb.transform.up
    //    };
    //    Gizmos.color = Color.red;
    //    for (int i = 0; i < direction.Length; i++)
    //    {
    //        Gizmos.DrawRay(DiceRb.transform.position, direction[i] * rayLength);
    //    }
    //}


    public void HighlightPOwns()
    {
        bool canMove = false;

        //setting coliders
        for(int i = 0; i < PlayersData[CurrentPlayerIndex].PawnsData.Length;i++)
        {
            PlayersData[CurrentPlayerIndex].PawnsData[i].transform. GetComponent<CapsuleCollider>().enabled = DiceNumber==6|| PlayersData[CurrentPlayerIndex].PawnsData[i].PathIndex!=-1;
            canMove= canMove || PlayersData[CurrentPlayerIndex].PawnsData[i].GetComponent<CapsuleCollider>().enabled;

        }

        CanDiceRoll = !canMove;
        if (!canMove)
        {
            ChangeTurn();
        }
    }



    public void ChangeTurn()
    {

        int count = 0;

        CurrentPlayerIndex = (CurrentPlayerIndex + 1) % PlayersData.Count;
        foreach (var player in PlayersData)
        {
            if (player.HasWon)
            {
               count++;
            }
        }

        if(count== PlayersData.Count-1)
        {
            Debug.Log("Game Over");
            return;
        }


        //Skip player who has won
        while (PlayersData[CurrentPlayerIndex].HasWon && count < 4)
        {
            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % PlayersData.Count;
        }



        Debug.Log("Count" + count);



    }

     async Task TakeTurn(int PawnIndex)
    {
        //Reset coliders
        for (int i = 0; i < PlayersData[CurrentPlayerIndex].PawnsData.Length; i++)
        {
            PlayersData[CurrentPlayerIndex].PawnsData[i].transform.GetComponent<CapsuleCollider>().enabled = false;
        }


        //Moving Pawns
        var LastDiceValue = DiceNumber;
        DiceNumber = PlayersData[CurrentPlayerIndex].PawnsData[PawnIndex].PathIndex == -1 ? 1 : DiceNumber;
        await PlayersData[CurrentPlayerIndex].MovePawn(PawnIndex, DiceNumber);
        //Check if any other pawn is present on the same path index
        if (PlayersData[CurrentPlayerIndex].Path[PlayersData[CurrentPlayerIndex].PawnsData[PawnIndex].PathIndex].childCount > 1 && !PlayersData[CurrentPlayerIndex].Path[PlayersData[CurrentPlayerIndex].PawnsData[PawnIndex].PathIndex].transform.Equals("Safe"))
        {
            int count = 0;
            var childs = PlayersData[CurrentPlayerIndex].Path[PlayersData[CurrentPlayerIndex].PawnsData[PawnIndex].PathIndex].GetComponentsInChildren<Pawns>();
           

            foreach (var item in childs)
            {
                var pawn = item.GetComponent<Pawns>();
                if (pawn != null)
                {
                    if (item.playerId != PlayersData[CurrentPlayerIndex].id)
                    {
                        item.ResetPawn();
                    }
                    else
                    {
                        count++;
                    }
                }
            }



            //Check if player has won

            if ((count == PlayersData[CurrentPlayerIndex].PawnsData.Length) && PlayersData[CurrentPlayerIndex].PawnsData[PawnIndex].PathIndex == PlayersData[CurrentPlayerIndex].Path.Count - 1)
            {
                Debug.Log("Player " + PlayersData[CurrentPlayerIndex].Name + " Wins");
                PlayersData[CurrentPlayerIndex].HasWon = true;

            }
            
        }

        if (LastDiceValue!=6)
        {
            ChangeTurn();
        }

        CanDiceRoll = true;
       
    }



    //sert dice value
    public void DiceValue()
    {
        float rayLength = 1.0f;
        Vector3[] direction = new Vector3[] {

            DiceRb.transform.forward,
            -DiceRb.transform.forward,  
            DiceRb.transform.right,
            -DiceRb.transform.right,
            DiceRb.transform.up,
            -DiceRb.transform.up
        };

        string[] directionName = new string[] {
            "Forward",
            "Back",
            "Right",
            "Left",
            "Up",
            "Down"
        };



        for (int i = 0; i < direction.Length; i++)
        {
            //Ray ray = new Ray(DiceRb.transform.position, direction[i]);
         
            RaycastHit hit;
            if (Physics.Raycast(DiceRb.transform.position,direction[i], out hit,rayLength))
            {
                //Debug.Log("Hit " + directionName[i]);
                DiceNumber = directionName[i] switch
                {
                    "Up" => 5,
                    "Down" => 2,
                    "Right" => 3,
                    "Left" => 4,
                    "Forward" => 6,
                    "Back" => 1,
                    _ => 0
                };
                //Debug.Log(DiceNumber);


            }
        }
        HighlightPOwns();
    }

    
}








