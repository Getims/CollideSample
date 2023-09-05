using System;
using LabraxStudio.Localization;
using LabraxStudio.Meta.Tutorial;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LabraxStudio.UI.GameScene.Tutorial
{
    [Serializable]
    public class TutorialTextController
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private Image _tutorialTitleIMG;

        [SerializeField]
        private GameObject _containerTMP;

        [SerializeField]
        private TextMeshProUGUI _tutorialTitleTMP;

        // PROPERTIES: ----------------------------------------------------------------------------

        public Image TutorialTitleImg => _tutorialTitleIMG;

        public TextMeshProUGUI TutorialTitleTmp => _tutorialTitleTMP;

        // FIELDS: -------------------------------------------------------------------

        private GameObject _currentTitle;
        private bool _useTMP = false;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void SetupTitle(LevelRules rules)
        {
            _useTMP = !rules.UseSpriteTitle;

            if (_useTMP)
            {
                _currentTitle = _containerTMP;
                _tutorialTitleIMG.gameObject.SetActive(false);
                SetString(rules.TutorialTitleString);
            }
            else
            {
                _currentTitle = _tutorialTitleIMG.gameObject;
                _containerTMP.SetActive(false);
                SetSprite(rules.TutorialTitleSprite);
            }
        }

        public void SetState(bool enabled)
        {
            _currentTitle.SetActive(enabled);
        }

        public void SetTitle(string titleString = "", Sprite titleSprite = null)
        {
            if (_useTMP)
            {
                if (titleString != null)
                    SetString(titleString);
            }
            else
            {
                if (titleSprite != null)
                    SetSprite(titleSprite);
            }
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void SetSprite(Sprite title)
        {
            _tutorialTitleIMG.sprite = title;
        }

        private void SetString(string title)
        {
            _tutorialTitleTMP.text = GameLocalization.Translate(title);
        }
    }
}