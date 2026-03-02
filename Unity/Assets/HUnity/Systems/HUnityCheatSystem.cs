using System.Collections.Generic;
using DesignTable.Types;
using HEngine.Core;
using Herring.UI.Presenter;
using HFin.Cheat;
using HUnity.Core;
using HUnity.UI;

namespace HUnity.Systems
{
    public class HUnityCheatSystem : HSystem, IHInputInterpretable
    {
        protected readonly CheatSpotlight SpotLight = new();
        private CheatPresenter _openedPresenter;

        public IEnumerable<CheatAction> AllCheats => SpotLight.All;

        public override void BeginPlay()
        {
            base.BeginPlay();
            RegisterCheats();
        }

        protected virtual void RegisterCheats()
        {
        }

        public HResultCode Execute(string query)
        {
            return SpotLight.Execute(query);
        }

        public void UpdateCommand(HInputCommand command, float dt)
        {
            var uiSys = GetSystem<UISystem>();
            if (command.HasFlag(HInputCommand.Cheat))
            {
                if (!uiSys.IsOpened(PresenterType.Cheat))
                {
                    _openedPresenter = uiSys.Open(PresenterType.Cheat) as CheatPresenter;
                }
                else
                {
                    uiSys.Close(PresenterType.Cheat);
                    _openedPresenter = null;
                }
            }

            if (null == _openedPresenter)
                return;

            if (command.HasFlag(HInputCommand.Up))
            {
                _openedPresenter.DecrementIndex();
            }
            else if (command.HasFlag(HInputCommand.Down))
            {
                _openedPresenter.IncrementIndex();
            }
        }
    }
}