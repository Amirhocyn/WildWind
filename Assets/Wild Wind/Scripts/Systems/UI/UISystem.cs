using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WildWind.Combat;
using UnityEngine.UI;

namespace WildWind.Systems
{

    public class UISystem : MonoSingleton<UISystem>
    {

        [SerializeField] private GameObject gameMenu;
        [SerializeField] private GameObject homeMenu;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject finishedMenu;

        #region Game Menu
        [SerializeField] Button pauseButton;
        [SerializeField] Text time;
        [SerializeField] Text score;
        #endregion

        #region Home Menu
        [SerializeField] Button nextPlaneButton;
        [SerializeField] Button previousPlaneButton;
        [SerializeField] Button startGameButton;
        #endregion

        #region Pause Menu
        [SerializeField] Button playButton;
        #endregion

        #region Finished Menu
        [SerializeField] Button playAgain;
        [SerializeField] Button homeButton;

        public override void Awake()
        {

            base.Awake();

            GameSystem.Instance.OnGameStateChange += UpdateMenusStates;

        }
        #endregion

        public override void Start()
        {

            base.Start();

            #region Setup Home Menu
            nextPlaneButton.onClick.AddListener(GameSystem.Instance.NextPlane);
            previousPlaneButton.onClick.AddListener(GameSystem.Instance.PreviousPlane);
            startGameButton.onClick.AddListener(GameSystem.Instance.StartGameSession);
            #endregion

            #region Setup Game Menu
            pauseButton.onClick.AddListener(PauseAction);
            #endregion

            #region Setup Pause Menu
            playButton.onClick.AddListener(PlayAction);
            #endregion

            #region Setup Finish Menu
            homeButton.onClick.AddListener(GameSystem.Instance.LoadHomeMenu);
            playAgain.onClick.AddListener(GameSystem.Instance.StartGameSession);
            #endregion

            GameSystem.Instance.OnGameStart += (() =>
            {
                time.text = "00:00";
                score.text = "0";
            });

        }

        public override void Update()
        {

            base.Update();

            score.text = ScoringSystem.Instance.score.ToString();

        }

        private void UpdateMenusStates()
        {
            homeMenu.SetActive(GameSystem.Instance.gameState == GameSystem.GameState.Home);
            gameMenu.SetActive(GameSystem.Instance.gameState == GameSystem.GameState.Playing || GameSystem.Instance.gameState == GameSystem.GameState.Paused);
            pauseMenu.SetActive(GameSystem.Instance.gameState == GameSystem.GameState.Paused);
            finishedMenu.SetActive(GameSystem.Instance.gameState == GameSystem.GameState.Finished);
            pauseButton.gameObject.SetActive(GameSystem.Instance.gameState == GameSystem.GameState.Playing);
        }

        private void PlayAction()
        {

            GameSystem.Instance.ResumeGame();

        }

        private void PauseAction()
        {

            GameSystem.Instance.PauseGame();

        }

    }

}
