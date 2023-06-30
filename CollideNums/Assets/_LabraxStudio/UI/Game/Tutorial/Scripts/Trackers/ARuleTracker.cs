using System;
using LabraxStudio.Meta.Tutorial;

namespace LabraxStudio.UI.GameScene.Tutorial
{
    public abstract class ARuleTracker
    {
        protected RuleType _type;
        protected bool _isComplete;
        
        public RuleType Type => _type;
        public bool IsComplete => _isComplete;

        public bool LockMoves()
        {
            switch (_type)
            {
                case RuleType.Null:
                case RuleType.TaskComplete:
                    return false;
                case RuleType.TileSwipe:
                case RuleType.BoosterUse:
                case RuleType.BoosterTarget:
                case RuleType.Merge:
                case RuleType.MoveToGate:
                    return true;
            }    
            
            return false;
        }  
        
        public abstract void SetComplete();
        public abstract void OnDestroy();
    }
}