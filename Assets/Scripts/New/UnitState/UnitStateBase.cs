namespace DefaultNamespace
{
    public abstract class UnitStateBase
    {
        /// <summary>
        /// ステートを開始した時に呼ばれる
        /// </summary>
        public virtual void OnEnter(Unit owner, UnitStateBase prevState) { }
        /// <summary>
        /// 毎フレーム呼ばれる
        /// </summary>
        public virtual void OnUpdate(Unit owner) { }
        /// <summary>
        /// 毎フレーム一定間隔で呼ばれる
        /// </summary>
        public virtual void OnFixedUpdate(Unit owner) { }
        /// <summary>
        /// ステートを終了した時に呼ばれる
        /// </summary>
        public virtual void OnExit(Unit owner, UnitStateBase nextState) { }
    }
}