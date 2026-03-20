using Ball;
using BallController;
using BallInteractor;
using BallPresenter;
using ConsoleBallView;
using ConsoleMatchView;
using ConsolePaddlesInput;
using ConsolePaddleView;
using ConsolePlayerView;
using ConsoleStageView;
using ConsoleViewBatch;
using Container;
using Match;
using MatchController;
using MatchInteractor;
using MatchPresenter;
using Microsoft.Extensions.DependencyInjection;
using PaddlePresenter;
using Paddles;
using PaddlesController;
using PaddlesInteractor;
using PlayerPresenter;
using Players;
using PlayersController;
using PlayersInteractor;
using Stage;
using StageController;
using StageInteractor;
using StagePresenter;
using TickService;

namespace Main;

internal static class ConsoleContainer
{
    public static IServiceProvider ServiceProvider
    {
        get
        {
            if (_serviceProvider == null)
            {
                _serviceProvider = BuildServiceProvider();
                DependencyContainer.ServiceProvider = _serviceProvider;
                return _serviceProvider;
            }

            return _serviceProvider;
        }
    }

    private static IServiceProvider? _serviceProvider;

    private static ServiceProvider BuildServiceProvider()
    {
        var serviceCollection = new ServiceCollection();
        
        // Entities
        serviceCollection.AddSingleton<IBallService, BallService>();
        serviceCollection.AddSingleton<IMatchService, MatchService>();
        serviceCollection.AddSingleton<IPaddlesService, PaddlesService>();
        serviceCollection.AddSingleton<IPlayersService, PlayersService>();
        serviceCollection.AddSingleton<IStageService, StageService>();
        
        // Interactors
        serviceCollection.AddSingleton<IBallInteractor, BallInteractor.BallInteractor>();
        serviceCollection.AddSingleton<IMatchInteractor, MatchInteractor.MatchInteractor>();
        serviceCollection.AddSingleton<IPaddlesInteractor, PaddlesInteractor.PaddlesInteractor>();
        serviceCollection.AddSingleton<IPlayersInteractor, PlayersInteractor.PlayersInteractor>();
        serviceCollection.AddSingleton<IStageInteractor, StageInteractor.StageInteractor>();

        // Controllers
        serviceCollection.AddTransient<IBallController, BallController.BallController>();
        serviceCollection.AddTransient<IMatchController, MatchController.MatchController>();
        serviceCollection.AddTransient<IPaddlesController, PaddlesController.PaddlesController>();
        serviceCollection.AddSingleton<IPaddlesInputService, PaddlesInputService>();
        serviceCollection.AddTransient<IPlayersController, PlayersController.PlayersController>();
        serviceCollection.AddTransient<IStageController, StageController.StageController>();

        // Presenters
        serviceCollection.AddTransient<IBallPresenter, BallPresenter.BallPresenter>();
        serviceCollection.AddTransient<IMatchPresenter, MatchPresenter.MatchPresenter>();
        serviceCollection.AddTransient<IPaddlePresenter, PaddlePresenter.PaddlePresenter>();
        serviceCollection.AddTransient<IPlayerPresenter, PlayerPresenter.PlayerPresenter>();
        serviceCollection.AddTransient<IStagePresenter, StagePresenter.StagePresenter>();

        // Infrastructure

        // Views
        serviceCollection.AddTransient<IBallView, BallView>();
        serviceCollection.AddTransient<IMatchView, MatchView>();
        serviceCollection.AddTransient<IPaddleView, PaddleView>();
        serviceCollection.AddTransient<IPlayerView, PlayerView>();
        serviceCollection.AddTransient<IStageView, StageView>();
        serviceCollection.AddSingleton<IViewBatch, ViewBatch>();

        // Other concrete implementations
        serviceCollection.AddSingleton<ITickService, ConsoleTickService.ConsoleTickService>();

        return serviceCollection.BuildServiceProvider();
    }
}
