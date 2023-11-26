using System;
using System.Collections.Generic;

namespace Model
{
    public enum GamePhase
    {
        Deploy,
        Prepare,
        Battle
    }

    public class Game
    {
        public static Game instance;

        public Action<DTO.GameState> StateChanged;

        private DTO.GameState latestState;

        private GamePhase currentPhase;

        private Player playerA;
        private AIPlayer playerB;
        private Player winningPlayer = null;

        public Game()
        {
            instance = this;
            Reset();
        }

        public void Reset()
        {
            currentPhase = GamePhase.Deploy;
            ActionManager.Reset();
            CardManager.Reset();

            playerA = new Player();
            playerB = new AIPlayer();
            winningPlayer = null;

            UpdateGameState(DTO.GameStateCause.GameStarted);
        }

        public void PlayCard(int cardID, int targetId = -1) 
        {
            if (winningPlayer != null) return;

            var target = CardManager.GetCard<AbstractMercenary>(targetId);
            switch (currentPhase)
            {
                case GamePhase.Deploy:
                    var mercToPlay = CardManager.GetCard<AbstractMercenary>(cardID);

                    if (playerA.CanPlayMerc(mercToPlay) && playerA.inPlay.Count < 3)
                    {
                        ActionManager.AddNext(new PlayMercAction(mercToPlay, playerA, mercToPlay));
                        ActionManager.PlayQueuedActions();
                    }

                    break;
                case GamePhase.Prepare:
                    var abilityToPlay = CardManager.GetCard<AbstractAbility>(cardID);
                    playerA.PreparePlay(abilityToPlay, target);
                    break;
            }
        }

        public void UnPlayCard(int mercID)
        {
            if (winningPlayer != null) return;

            CardManager.GetCard<AbstractMercenary>(mercID)?.ClearPreparedPlay();
        }

        public void EndTurn()
        {
            if (winningPlayer != null) return;

            switch (currentPhase)
            {
                case GamePhase.Deploy:
                    currentPhase = GamePhase.Prepare;
                    playerB.PlayMercsAI();

                    PlayAllPossibleMercs(playerA);
                    PlayAllPossibleMercs(playerB);

                    playerB.PrepareAbilitiesAI();
                    break;

                case GamePhase.Prepare:
                    currentPhase = GamePhase.Battle;
                    ActionManager.AddLast(new CombatAction(playerA, playerB));
                    ActionManager.PlayQueuedActions();

                    if (playerA.inPlay.Count < 3 && playerA.bank.Count > 0 ||
                        playerB.inPlay.Count < 3 && playerB.bank.Count > 0)
                    {
                        currentPhase = GamePhase.Deploy;
                        UpdateGameState(DTO.GameStateCause.TurnStarted);
                        return;
                    }

                    EndTurn();
                    break;
                case GamePhase.Battle:
                    currentPhase = GamePhase.Prepare;
                    playerB.PrepareAbilitiesAI();
                    break;
            }

            UpdateGameState(DTO.GameStateCause.TurnStarted);
        }

        #region model internal functions

        private void PlayAllPossibleMercs(Player player)
        {
            while (player.inPlay.Count < 3 && player.bank.Count > 0)
            { 
                ActionManager.AddNext(new PlayMercAction(player.bank[0], player, player.bank[0]));
                ActionManager.PlayQueuedActions();
            }
        }

        public static void Loose(Player player)
        {
            instance.winningPlayer = player.enemy;
            ActionManager.CancelAllActions();
            UpdateGameState(DTO.GameStateCause.GameOver);
        }

        public static AbstractMercenary FindOwnerOf(AbstractAbility card)
        {
            var owner = instance.playerA.FindOwnerOf(card);
            if (owner != null) return owner;

            owner = instance.playerB.FindOwnerOf(card);
            if (owner != null) return owner;

            return null;
        }

        public static AbstractMercenary FindOwnerOf(AbstractBuff buff)
        {
            var owner = instance.playerA.FindOwnerOf(buff);
            if (owner != null) return owner;

            owner = instance.playerB.FindOwnerOf(buff);
            if (owner != null) return owner;

            return null;
        }

        public static Player FindPlayerOf(AbstractAbility card)
        {
            if (instance.playerA.ContainsAbility(card)) return Game.instance.playerA;
            if (instance.playerB.ContainsAbility(card)) return Game.instance.playerB;
            return null;
        }

        public static Player FindPlayerOf(AbstractMercenary card)
        {
            if (instance.playerA.ContainsMerc(card)) return Game.instance.playerA;
            if (instance.playerB.ContainsMerc(card)) return Game.instance.playerB;
            return null;
        }

        public static Player FindEnemyOf(Player player) => player == instance.playerA ? instance.playerB : instance.playerA;

        public static AbstractMercenary GetRandomValidTarget(abilityTargets selection, Player playerOwner)
        {
            var targets = new List<AbstractMercenary>();
            if ((selection & abilityTargets.enemy) != 0)
            {
                targets.AddRange(FindEnemyOf(playerOwner).inPlay);
            }
            if ((selection & abilityTargets.ally) != 0)
            {
                targets.AddRange(playerOwner.inPlay);
            }

            if((selection & abilityTargets.damaged) != 0)
            {
                var toRemove = new List<AbstractMercenary>();
                foreach (var target in targets)
                {
                    if(target.health.value >= target.health.maxValue)
                    {
                        toRemove.Add(target);
                    }
                }

                foreach(var target in toRemove)
                {
                    targets.Remove(target);
                }
            }
            

            if (targets.Count == 0) return null;

            return targets[UnityEngine.Random.Range(0, targets.Count - 1)];
        }

        public static AbstractMercenary GetRandomValidTarget(abilityTargets selection, AbstractMercenary owner)
            => GetRandomValidTarget(selection, FindPlayerOf(owner));

        public static AbstractMercenary GetAlternativeTarget(AbstractMercenary originalTarget)
        {
            var targets = new List<AbstractMercenary>();

            if (instance.playerA.graveYard.Contains(originalTarget))
            {
                targets.AddRange(instance.playerA.inPlay);
            }
            if (instance.playerB.graveYard.Contains(originalTarget))
            {
                targets.AddRange(instance.playerB.inPlay);
            }

            if (targets.Count == 0) return null;

            return targets[UnityEngine.Random.Range(0, targets.Count - 1)];
        }
        #endregion

        #region Create DTO
        public static void UpdateGameState(DTO.GameStateCause cause, AbstractAbility lastAbility = null)
        {
            instance.latestState = new DTO.GameState()
            {
                cause = cause,
                currentPhase = instance.currentPhase,
                players = new DTO.PlayerState[] { instance.playerA.GetState(), instance.playerB.GetState() },
                lastAbility = lastAbility?.GetState(),
            };

            if (instance.winningPlayer != null)
            {
                instance.latestState.winningPlayer = instance.winningPlayer == instance.playerA ? 0 : 1;
            }

            instance.StateChanged?.Invoke(instance.latestState);
        }
        #endregion
    }
}