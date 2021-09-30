using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace WildWind.Systems
{

    public class UISystem : MonoBehaviourMaster<UISystem>
    {

        private UIDocument doc;
        private VisualElement rootElement;

        private Button pauseButton;
        private Button playButton;

        private VisualElement pauseMenu;

        public override void Start()
        {

            base.Start();
            doc = GetComponentInChildren<UIDocument>();
            rootElement = doc.rootVisualElement;
            pauseButton = rootElement.Q<Button>("Pause");
            playButton = rootElement.Q<Button>("Play");
            pauseMenu = rootElement.Q<VisualElement>("Pause-Menu");
            pauseButton.clicked += PauseAction;
            playButton.clicked += PlayAction;

        }

        public override void Update()
        {

            base.Update();

        }

        private void PlayAction()
        {

            GameSystem.Instance.PlayGame();
            pauseButton.style.display = DisplayStyle.Flex;
            pauseMenu.style.display = DisplayStyle.None;

        }

        private void PauseAction()
        {

            GameSystem.Instance.PauseGame();
            pauseButton.style.display = DisplayStyle.None;
            pauseMenu.style.display = DisplayStyle.Flex;

        }

    }

}
