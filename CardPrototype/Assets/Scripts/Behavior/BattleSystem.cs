using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleSystem : MonoBehaviour
{
    public enum BattleState
    {
        START,
        PLAYERTURN,
        EnemyTurn,
        WON,
        LOST
    }

    public BattleState state;

    [SerializeField] GameObject playerPrefab, enemyPrefab;
    private List<DeckMaster> masters = new();

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(Setup());
    }

    private IEnumerator Setup()
    {
        GameObject playerGO = Instantiate(playerPrefab);
        masters.Add(playerGO.GetComponent<DeckMaster>());

        GameObject enemyGO = Instantiate(enemyPrefab);
        masters.Add(enemyGO.GetComponent<DeckMaster>());


        yield return new WaitForSeconds(2f);
        state = BattleState.PLAYERTURN;
    }

    private IEnumerator DoTurns()
    {
        foreach (var m in masters) 
        {
            yield return StartCoroutine(m.TakeTurn());
        }
        yield return null;
    }
}
