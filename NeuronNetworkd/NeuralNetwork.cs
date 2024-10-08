﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronNetwork
{
    public class NeuralNetwork
    {
        public List<Layer> Layers { get; }

        public Topology Topology { get; }
        public NeuralNetwork(Topology topology)
        {
            Topology = topology;

            Layers = new List<Layer>();
            CreateInputLayer();
            CreateHiddenLayers();
            CreateOutputLayer();

        }

        public Neuron FeedForward(List<double> inputSignals)
        {
            // проверка количества входных сигналов 
            SendSignalsToInputNeurons(inputSignals);
            FeedForwardAllLayersAfterInput();
            if (Topology.OutputCount == 1)
            {
                return Layers.Last().Neurons[0];

            }
            else
            {
                return Layers.Last().Neurons.OrderByDescending(x => x.Output).First();
            }
        }

        private void FeedForwardAllLayersAfterInput()
        {
            for (int i = 1; i < Layers.Count; i++)
            {
                var layer = Layers[i];
                var previousLayerSignals = Layers[i - 1].GetSignals();
                foreach (var neuron in layer.Neurons)
                {
                    neuron.FeedForward(previousLayerSignals);
                }
            }
        }

        private void SendSignalsToInputNeurons(List<double> inputSignals)
        {
            for (int i = 0; i < inputSignals.Count; i++)
            {
                var signal = new List<double>() { inputSignals[i] };
                var neuron = Layers[0].Neurons[i];

                neuron.FeedForward(signal);
            }
        }

        private void CreateOutputLayer()
        {
            var outputNeurons = new List<Neuron>();
            var lastLayer=Layers.Last();
            for (int i = 0; i < Topology.OutputCount; i++)
            {
                var neuron = new Neuron(lastLayer.Count, NeuronType.Output);
                outputNeurons.Add(neuron);
            }
            var outputLayer = new Layer(outputNeurons, NeuronType.Output);
            Layers.Add(outputLayer);
        }

        private void CreateHiddenLayers()
        {
            for(int j = 0; j < Topology.HiddenLayers.Count; j++)
            {
                var hiddenNeurons = new List<Neuron>();
                var lastLayer = Layers.Last();
                for (int i = 0; i < Topology.HiddenLayers[j]; i++)
                {
                    var neuron = new Neuron(lastLayer.Count);
                    hiddenNeurons.Add(neuron);
                }
                var hiddenLayer = new Layer(hiddenNeurons);
                Layers.Add(hiddenLayer);
            }
        }

        public void CreateInputLayer()
        {
            var inputsNeurons=new List<Neuron>();
            for(int i=0; i < Topology.InputCount; i++)
            {
               var neuron = new Neuron(1, NeuronType.Input);
               inputsNeurons.Add(neuron);
            }
            var inputLayer=new Layer(inputsNeurons,NeuronType.Input);
            Layers.Add(inputLayer);
        }
    }
}
