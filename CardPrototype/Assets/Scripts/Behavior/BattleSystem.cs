using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleSystem : MonoBehaviour
{
    public enum BattleState
    {
        START,
        PLAYING,
        WON,
        LOST
    }

    public BattleState state;
    public BattleState State
    {
        get { return state; }
        set
        {
            if (value == state) return; 
            state = value;
            switch (value)
            {
                case BattleState.START:
                    StartCoroutine(Setup());
                    break;
                case BattleState.PLAYING:
                    StartCoroutine(AllowTurns());
                    break;
                case BattleState.WON:
                    Debug.Log("Player Won");
                    Application.Quit();
                    break;
                case BattleState.LOST:
                    Debug.Log("Player Lost");
                    Application.Quit();
                    break;
                default:
                    break;
            }
        }
    }
    
    [SerializeField] private GameObject gameBoard;
    [SerializeField] private GameObject playerPrefab, enemyPrefab;
    private List<DeckMaster> masters = new();

    void Start()
    {
        State = BattleState.START;
    }

    private void FlipBoard()
    {
        float rotation = -1f * gameBoard.transform.rotation.z;
        gameBoard.transform.rotation = Quaternion.Euler(0, 0, rotation + 180f); 
    }

    private IEnumerator Setup()
    {
        GameObject playerGO = Instantiate(playerPrefab);
        masters.Add(playerGO.GetComponent<DeckMaster>());

        GameObject enemyGO = Instantiate(enemyPrefab);
        masters.Add(enemyGO.GetComponent<DeckMaster>());


        yield return new WaitForSeconds(2f);
        State = BattleState.PLAYING;
    }

    private IEnumerator AllowTurns()
    {
        while (BattleState.PLAYING == State)
        {
            foreach (var m in masters)
            {
                yield return StartCoroutine(m.TakeTurn());
                FlipBoard();
            }
            EvalField();
        }
        yield return null;
    }
    
    private void EvalField()
    {
        for (int i = 0; i < 3; i++) // 3 battleLanes
        {
            DeckMaster z = null;
            foreach (var m in masters)
            {
                if (null == z || m.EvalLane(i) > z.EvalLane(i))
                {
                    z = m;
                }
            }

            foreach (var m in masters)
            {
                if (m == z) continue;
                m.TakeDamage(z.EvalLane(i) - m.EvalLane(i));
            }
        }

        if (masters[0].Dead) State = BattleState.LOST;
        else if (masters[1].Dead) State = BattleState.WON;
    }
}
