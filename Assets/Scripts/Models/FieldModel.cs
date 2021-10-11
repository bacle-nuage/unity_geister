using System.Collections.Generic;

namespace Models
{
    public class FieldModel
    {
        private static Dictionary<string, float> _fieldSize = new Dictionary<string, float>()
        {
            {"xMax", 0.3f},  //フィールドのX座標上限
            {"xMin", -0.3f}, //フィールドのX座標下限
            {"yMax", 0.3f},  //フィールドのY座標上限
            {"yMin", -0.3f}  //フィールドのY座標下限
        };

        public static Dictionary<string, float> FieldSize
        {
            get => _fieldSize;
        }

        public FieldModel()
        {
            
        }
    }
}