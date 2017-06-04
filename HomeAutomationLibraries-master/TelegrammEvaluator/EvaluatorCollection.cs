using System.Collections.Generic;

namespace TelegrammEvaluator
{
    public class EvaluatorCollection
    {
        List<TelegrammEvaluator> _Collection = new List<TelegrammEvaluator>()
        {
            new WindowTelegrammEvaluator(),
            new DoorTelegrammEvaluator()
        };

        public List<TelegrammEvaluator> Collection { get => _Collection; set => _Collection = value; }
    }
}
