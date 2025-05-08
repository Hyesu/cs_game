using UnityEngine;
using HEngine.Core;
using DesignTable.Core;

namespace HUnity.Entities
{
    public abstract class HGameInstance : MonoBehaviour
    {
        protected static HGameInstance Impl;
        public static HGameInstance GetInstance() => Impl;

        private DContext _d;
        private HSystemProvider _sysProvider;

        public DContext DContext => _d;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void MakeInstance()
        {
            if (Impl == null)
            {
                var gameObj = new GameObject(nameof(HGameInstance));
                Impl = gameObj.AddComponent<HGameInstance>();
                Impl.Initialize();

                DontDestroyOnLoad(gameObj);
            }
        }

        protected abstract HSystemProvider MakeSystemProvider();
        protected abstract void RegisterPresenter(HPresenterFactory factory);

        protected virtual void OnInitialize()
        {
        }

        protected virtual void OnStart()
        {
        }

        protected virtual void OnUpdate(float dt)
        {
        }
        
        private void Initialize()
        {
            // configure
            var settingPath = Application.dataPath + "/HData/setting.json";
            HConfiguration.Shared.Init(settingPath);

            // construct
            var dataTableRootPath = Application.dataPath + HConfiguration.Shared.DesignTableRoot;
            _d = new DContext(dataTableRootPath);
            _sysProvider = MakeSystemProvider();
            
            _d.Initialize(false);
            _sysProvider.Initialize();
            
            RegisterPresenter(HPresenterFactory.Shared);
            
            OnInitialize();

            // begin play
            _sysProvider.BeginPlay();
        }

        // mono behaviours
        void Start()
        {
            OnStart();
        }

        void Update()
        {
            var dt = Time.deltaTime;
            OnUpdate(dt);
        }
    }
}