using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronNetwork
{
    public class Topology
    {
        public int InputCount { get; }
        public int OutputCount { get; }
        public List<int> HiddenLayers { get; }

        public Topology(int inputCount,int outputCount, params int[] layers)
        {
            // params это скрытый слой с его количеством нейронов 
            InputCount=inputCount;
            OutputCount=outputCount;
            HiddenLayers = new List<int>();
            HiddenLayers.AddRange(layers);

        }
    }
}
