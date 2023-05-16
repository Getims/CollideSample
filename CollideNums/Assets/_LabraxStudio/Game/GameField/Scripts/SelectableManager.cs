using LabraxStudio.Managers;
using Lean.Common;
using Lean.Touch;
using UnityEngine;

namespace LabraxStudio.Game.Puzzle
{
    public class SelectableManager : SharedManager<SelectableManager>
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private LeanSelectByFinger _selectByFinger;

        // PUBLIC METHODS: -----------------------------------------------------------------------

        public void Select(LeanSelectable leanSelectable)
        {
            _selectByFinger.Select(leanSelectable, new LeanFinger());
        }
    }
}