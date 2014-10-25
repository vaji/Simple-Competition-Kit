
namespace UltimateHackathonFramework
{
    using System;
    using System.Collections.Generic;
    using Caliburn.Micro;
    using UltimateHackathonFramework.Interfaces;
    using UltimateHackathonFramework.Models;
    using UltimateHackathonFramework;
    using UltimateHackathonFramework.Games;
    public class AppBootstrapper : BootstrapperBase
    {
        SimpleContainer container;

        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();

            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            container.PerRequest<IShell, ShellViewModel>();
            container.Singleton<IClientManager, ClientManager>();
            container.Singleton<GameViewModel>();
            container.Singleton<ResultsViewModel>();
            container.Singleton<ClientsViewModel>();
            container.Singleton<IGameManager, GameManager>();
            container.Singleton<ICommunication, Communication>();
            container.Singleton<CommunicationViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<IShell>();
        }
    }
}
