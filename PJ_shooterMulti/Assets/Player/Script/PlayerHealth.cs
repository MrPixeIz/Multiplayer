using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour, Damagable
{
    private PlayerController playerController;
    private ScoreDisplayer scoreDisplayer;
    private TeamManager teamManager;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        teamManager = GetComponent<TeamManager>();
    }
    public override void OnStartServer()
    {
        scoreDisplayer = FindObjectOfType<ScoreDisplayer>();
    }

    public void DealDamage(GameObject from, int damage)
    {
        CmdDealDamage(from, damage);
    }
    [Command]
    public void CmdDealDamage(GameObject from, int damage)
    {
        TeamManager fromTeanManager = from.GetComponent<TeamManager>();
        if (fromTeanManager != null)
        {

            if (scoreDisplayer != null)
            {
                if (fromTeanManager.GetTeam() != teamManager.GetTeam())
                {
                    scoreDisplayer.RpcAddKill(fromTeanManager.GetTeam());
                }
            }
        }
playerController.RpcDie();
    }
}
