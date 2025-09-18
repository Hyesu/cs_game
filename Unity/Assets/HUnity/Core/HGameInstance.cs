using UnityEngine;
using UnityEngine.SceneManagement;
using HEngine.Core;
using HUnity.UI;
using DesignTable.Core;

namespace HUnity.Core
{
    public class HGameInstance : MonoBehaviour
    {
        protected static HGameInstance Impl;
        public static HGameInstance GetInstance() => Impl;

        private DContext _d;
        private HSystemProvider _sysProvider;

        public DContext DContext => _d;
        public HSystemProvider SystemProvider => _sysProvider;

        protected virtual HSystemProvider MakeSystemProvider()
        {
            return new HSystemProvider();
        }

        protected virtual void RegisterPresenter(HPresenterFactory factory)
        {
        }

        protected virtual void OnInitialize()
        {
        }

        protected virtual void OnStart()
        {
        }
        
        protected virtual void OnUpdate(float dt)
        {
        }

        protected virtual void OnSceneLoaded()
        {
            _sysProvider.BeginPlay();
        }

        protected virtual void OnSceneUnloaded()
        {
            _sysProvider.EndPlay();
        }

        public void Initialize()
        {
            // configure
            var settingPath = Application.dataPath + "/HData/setting.json";
            HConfiguration.Shared.Init(settingPath);

            // construct
            var dataTableRootPath = Application.dataPath + HConfiguration.Shared.DesignTableRoot;
            _d = new DContext(dataTableRootPath);
            _sysProvider = MakeSystemProvider();

            _d.Initialize(false);
            foreach (var sys in _sysProvider.As)
            {
                sys.SetDataContext(_d);
            }
            
            _sysProvider.Initialize();

            RegisterPresenter(HPresenterFactory.Shared);

            OnInitialize();
        }
        
        

        public T GetSystem<T>() where T : HSystem
        {
            return _sysProvider.GetSystem<T>();
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            OnSceneLoaded();
        }

        private void OnSceneUnloaded(Scene scene)
        {
            OnSceneUnloaded();
        }
        
        // mono behaviours
        void Start()
        {
            OnStart();
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        void Update()
        {
            var dt = Time.deltaTime;
            OnUpdate(dt);
        }
        
        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
            
            _sysProvider.Shutdown();
        }
    }
}