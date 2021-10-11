namespace DefaultNamespace
{
    public abstract class GhostStateBase
    {
        /// <summary>
        /// ステートを開始した時に呼ばれる
        /// </summary>
        public virtual void OnEnter(Ghost owner, GhostStateBase prevState) { }
        /// <summary>
        /// 毎フレーム呼ばれる
        /// </summary>
        public virtual void OnUpdate(Ghost owner) { }
        /// <summary>
        /// 毎フレーム一定間隔で呼ばれる
        /// </summary>
        public virtual void OnFixedUpdate(Ghost owner) { }
        /// <summary>
        /// ステートを終了した時に呼ばれる
        /// </summary>
        public virtual void OnExit(Ghost owner, GhostStateBase nextState) { }
    }
}