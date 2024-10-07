using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronNetwork
{
    public class Neuron
    {
        public List<double> Weight { get; }
        public NeuronType NeuronType { get; }

        public double Output { get; private set; }

        public Neuron(int inputCount, NeuronType type=NeuronType.Normal)
        {
            //добавить проверку входных параметров
            NeuronType = type;
            Weight = new List<double>();

            for (int i = 0; i < inputCount; i++)
            {
                Weight.Add(1);
            }
        }

        public double FeedForward(List<double> inputs)
        {
            var summ = 0.0;
            for (int i = 0; i < inputs.Count; i++)
            {
                summ += inputs[i] * Weight[i];
            }

            if (NeuronType != NeuronType.Input)
            {
                Output = Sigmoid(summ);
            }
            else
            {
                Output = summ;
            }
            return Output;
        }

        private double Sigmoid(double x)
        {
            var result = 1.0 / (1.0 + Math.Pow(Math.E, -x));
            return result;
        }

        public void SetWeights(params double[] weights)
        {
            //TODO: удалить после добавления возможности обучения сети
            for (int i = 0; i < weights.Length; i++)
            {
                Weight[i]= weights[i];
            }
        }
        public override string ToString()
        {
            return Output.ToString();
        }
    }
}
