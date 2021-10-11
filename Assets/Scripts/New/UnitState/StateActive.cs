namespace DefaultNamespace
{
    public partial class Unit
    {
        public class StateActive : UnitStateBase
        {
            /// <summary>
            /// ステートを開始した時に呼ばれる
            /// </summary>
            public override void OnEnter(Unit owner, UnitStateBase prevState) { }
            
            /// <summary>
            /// 毎フレーム呼ばれる
            /// </summary>
            public override void OnUpdate(Unit owner) { }
            
            /// <summary>
            /// 毎フレーム一定間隔で呼ばれる
            /// </summary>
            public override void OnFixedUpdate(Unit owner) { }

            /// <summary>
            /// ステートを終了した時に呼ばれる
            /// </summary>
            public override void OnExit(Unit owner, UnitStateBase nextState) { }
        }   
    }
}