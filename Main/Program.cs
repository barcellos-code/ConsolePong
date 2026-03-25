using System;
using BallController;
using ConsoleViewBatch;
using MatchController;
using PaddlesController;
using PlayersController;
using StageController;

namespace Main
{
    internal class Program
    {
        // Game Parameters
        private const int NumberOfPlayers = 2;
        private const int StageWidth = 80;
        private const int StageHeight = 25;
        private const int PaddleSize = 5;
        private const int BallInitialDirX = 1;
        private const int BallInitialDirY = 1;
        private const int WinningScore = 3;

        private static void Main()
        {
            // Retrieve Controllers
            var ballController = ConsoleContainer.GetService<IBallController>() ?? throw new NullReferenceException($"Unable to retrieve {nameof(IBallController)}");
            var matchController = ConsoleContainer.GetService<IMatchController>() ?? throw new NullReferenceException($"Unable to retrieve {nameof(IMatchController)}");
            var paddlesController = ConsoleContainer.GetService<IPaddlesController>() ?? throw new NullReferenceException($"Unable to retrieve {nameof(IPaddlesController)}");
            var playersController = ConsoleContainer.GetService<IPlayersController>() ?? throw new NullReferenceException($"Unable to retrieve {nameof(IPlayersController)}");
            var stageController = ConsoleContainer.GetService<IStageController>() ?? throw new NullReferenceException($"Unable to retrieve {nameof(IStageController)}");

            // Retrieve Infrastructure
            var viewBatch = ConsoleContainer.GetService<IViewBatch>() ?? throw new NullReferenceException($"Unable to retrieve {nameof(IViewBatch)}");
        
            // Create Game Entities
            ballController.CreateBall(StageWidth / 2, StageHeight / 2, BallInitialDirX, BallInitialDirY);
            matchController.CreateMatch(WinningScore, StageWidth, StageHeight);
            paddlesController.CreatePaddles(NumberOfPlayers, PaddleSize, StageWidth, StageHeight);
            playersController.CreatePlayers(NumberOfPlayers, StageWidth, StageHeight);
            stageController.CreateStage(StageWidth, StageHeight);

            // Bind Events
            playersController.BindGoalEvents();
            matchController.BindScoreEvents();

            // Start UI
            viewBatch.Start();

            // Start match
            matchController.StartMatch();

            // Start Ball Tick
            ballController.StartBallTick();
        }
    }
}
