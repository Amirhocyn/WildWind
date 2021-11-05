using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityFx.Async;
using WildWind.Control;
using WildWind.Core;
using WildWind.Movement;
using Random = UnityEngine.Random;

namespace WildWind.Systems
{

    public class GameSystem : MonoSingleton<GameSystem>
    {

        [SerializeField] PlayerController[] planes;
        [SerializeField] CameraController P_cameraController;
        CameraController cameraController;

        internal Action OnGameStart;
        internal Action OnFinished;
        internal Action OnGameStateChange;

        public enum GameState { Home, Paused, Playing, Finished, None };
        private GameState _gameState;
        public GameState gameState 
        { 
            get
            {

                return _gameState;

            }
            private set 
            {

                _gameState = value;
                OnGameStateChange(); 

            } 
        }

        [HideInInspector] public PlayerController player;

        private PlayerController plane;
        private int id
        {

            get
            {

                return PlayerPrefs.GetInt("Default Plane");

            }
            set
            {

                PlayerPrefs.SetInt("Default Plane", value);
                PlayerPrefs.Save();

            }

        }

        #region Scene roots
        public static Transform S_pre { get { return GameObject.Find("S_pre").transform; } }
        public static Transform S_Home { get { return GameObject.Find("S_Home").transform; } }
        public static Transform S_Game { get { return GameObject.Find("S_Game").transform; } }
        #endregion

        public override void Awake()
        {

            base.Awake();

        }

        public override void Start()
        {

            base.Start();

            LoadHomeMenu();

            Application.targetFrameRate = Screen.currentResolution.refreshRate;


        }

        public void LoadHomeMenu()
        {

            
            SceneManager.LoadSceneAsync("S_Home", LoadSceneMode.Additive).completed += (op =>
            {
                gameState = GameState.Home;
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("S_Home"));
                InstantiatePlane();
            });

            for(int j = 0;j < SceneManager.sceneCount;j++)
            {

                if(SceneManager.GetSceneAt(j).name == "S_Game")
                {

                    SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(j));

                }

            }

        }

        public IEnumerator E_LoadHomeMenu()
        {
            yield return SceneManager.LoadSceneAsync("S_Home", LoadSceneMode.Additive);//.completed += (op =>
            //{
            //    gameState = GameState.Home;
            //});
            gameState = GameState.Home;
            InstantiatePlane();
            //SceneManager.LoadSceneAsync("S_Home", LoadSceneMode.Additive);
            //gameState = GameState.Home;

        }

        public override void Update()
        {

            base.Update();

        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override void OnEnable()
        {
            base.OnEnable();
        }

        private void OnApplicationQuit()
        {



        }

        private void OnApplicationFocus(bool focus)
        {



        }

        private void OnApplicationPause(bool pause)
        {



        }

        private void SetPlayerAsAlertCenter()
        {

            Alert.alertCenter = player.transform;

        }

        private void SetupCamera()
        {

            cameraController = Instantiate(P_cameraController);
            cameraController.SetFollowTarget(player.gameObject);

        }

        private void InstantiatePlayer()
        {

            int planeToLoad = PlayerPrefs.GetInt("Default Plane");
            gameState = GameState.Playing;
            player = Instantiate(planes[planeToLoad], Vector3.zero, Quaternion.identity);
            if (OnGameStart != null)
                OnGameStart();
            SetupCamera();
            SetPlayerAsAlertCenter();
            player.OnDeath += FinishGame;

        }

        public void PauseGame()
        {

            gameState = GameState.Paused;
            Time.timeScale = 0;

        }

        public void ResumeGame()
        {

            gameState = GameState.Playing;
            Time.timeScale = 1;

        }

        public PlayerController[] GetPlanes()
        {

            return planes;

        }

        public void StartGameSession()
        {

            StartCoroutine(E_StartGameSession());

        }

        private IEnumerator E_StartGameSession()
        {

            Scene[] scenes = SceneManager.GetAllScenes();
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
            foreach (Scene scene in scenes)
            {

                if (scene.name == "S_Home")
                    yield return SceneManager.UnloadSceneAsync("S_Home");
                if (scene.name == "S_Game")
                    yield return SceneManager.UnloadSceneAsync("S_Game");

            }
            yield return SceneManager.LoadSceneAsync("S_Game", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("S_Game"));
            if (OnGameStart != null)
                OnGameStart();

            gameState = GameState.Playing;

            InstantiatePlayer();

        }

        private void FinishGame()
        {

            if (OnFinished != null)
                OnFinished();

            gameState = GameState.Finished;

        }

        #region Home Menu
        internal void NextPlane()
        {

            id = Mathf.Clamp(id + 1, 0, GameSystem.Instance.GetPlanes().Length - 1);
            InstantiatePlane();

        }

        internal void PreviousPlane()
        {

            id = Mathf.Clamp(id - 1, 0, GameSystem.Instance.GetPlanes().Length);
            InstantiatePlane();

        }

        private void InstantiatePlane()
        {

            if (plane != null)
                Destroy(plane.gameObject);
            plane = Instantiate(GameSystem.Instance.GetPlanes()[id]);
            plane.GetComponent<PlayerController>().enabled = false;
            plane.GetComponent<Mover>().enabled = false;
            plane.GetComponent<Combat.Combat>().enabled = false;
            //plane.transform.parent = S_Home.transform;

        }
        #endregion

    }

}
