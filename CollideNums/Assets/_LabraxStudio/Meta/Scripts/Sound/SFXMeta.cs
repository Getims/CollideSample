using LabraxEditor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LabraxStudio.Meta
{
    public class SFXMeta : ScriptableMeta
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [TitleGroup(Settings)]
        [BoxGroup(SettingsGroup, showLabel: false), SerializeField]
        private bool _isDisabled;

        [BoxGroup(SettingsGroup), SerializeField, Required, AssetsOnly]
        [HideIf(nameof(_isDisabled))]
        private AudioClip _audioClip;

        [BoxGroup(SettingsGroup), SerializeField, Range(0, 1), LabelText("Sound Percent")]
        [HideIf(nameof(_isDisabled))]
        private float _soundPrecent = 1;

        [BoxGroup(SettingsGroup), SerializeField, HideLabel, TextArea(1, 4), Space(4)]
        private string _description = "Description...";

        // PROPERTIES: ----------------------------------------------------------------------------

        public bool IsDisabled => _isDisabled;
        public AudioClip AudioClip => _audioClip;
        public float SoundPercent => _soundPrecent;

        // FIELDS: --------------------------------------------------------------------------------

        private const string Settings = "Settings";
        private const string SettingsGroup = Settings + "/Box";
    }
}