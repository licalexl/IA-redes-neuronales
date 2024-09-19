using UnityEngine;
using System;

public class NeuralNetwork
{
    private int[] layers;//inicializar la red neuronal  5,6,4,3
    private float[][] neurons;
    private float[][][] weights; // layer de origen, neurona del layer origen, neurona destino de la siguiente layer
    //fuerza de las conexiones y ajustes en la evolucion
    public NeuralNetwork(params int[] layers)
    {
        this.layers = layers;
        InitializeNeurons();
        InitializeWeights();
    }

    private void InitializeNeurons()
    {
        neurons = new float[layers.Length][];
        for (int i = 0; i < layers.Length; i++)
        {
            neurons[i] = new float[layers[i]];
        }
    }

    private void InitializeWeights()
    {
        weights = new float[layers.Length - 1][][];
        for (int i = 0; i < layers.Length - 1; i++)// cada capa de pesos (entre capas de neuronas).
        {
            weights[i] = new float[layers[i]][]; //Para cada capa, creare un array de arrays para las neuronas de origen.
            for (int j = 0; j < layers[i]; j++) //cada neurona en la capa actual.
            {
                weights[i][j] = new float[layers[i + 1]];//pesos de las conexiones a la siguiente capa.
                for (int k = 0; k < layers[i + 1]; k++)//cada neurona en la capa siguiente.
                {
                    weights[i][j][k] = UnityEngine.Random.Range(-1f, 1f);
                }
            }
        }
    }

    public float[] FeedForward(float[] inputs)
    {
        for (int i = 0; i < inputs.Length; i++) //guardo en mi primer capa de neuronas
        {
            neurons[0][i] = inputs[i];
        }

        for (int i = 1; i < layers.Length; i++)
        {
            for (int j = 0; j < layers[i]; j++)//N CAP AC
            {
                float sum = 0;
                for (int k = 0; k < layers[i - 1]; k++)//neurona de la capa anterior.
                {
                    sum += neurons[i - 1][k] * weights[i - 1][k][j];       //peso de la conexión entre neturan cap anteruior y neu act
                }
                neurons[i][j] = (float)Math.Tanh(sum);
            }
        }

        return neurons[neurons.Length - 1];
    }

    public void Mutate(float mutationRate)
    {
        for (int i = 0; i < weights.Length; i++)
        {
            for (int j = 0; j < weights[i].Length; j++)
            {
                for (int k = 0; k < weights[i][j].Length; k++)
                {
                    if (UnityEngine.Random.value < mutationRate)
                    {
                        weights[i][j][k] += UnityEngine.Random.Range(-0.1f, 0.1f);
                    }
                }
            }
        }
    }

    public NeuralNetwork Copy()
    {
        NeuralNetwork copy = new NeuralNetwork(layers);
        for (int i = 0; i < weights.Length; i++)
        {
            for (int j = 0; j < weights[i].Length; j++)
            {
                for (int k = 0; k < weights[i][j].Length; k++)
                {
                    copy.weights[i][j][k] = weights[i][j][k];
                }
            }
        }
        return copy;
    }

    public void Crossover(NeuralNetwork other)
    {
        for (int i = 0; i < weights.Length; i++)
        {
            for (int j = 0; j < weights[i].Length; j++)
            {
                for (int k = 0; k < weights[i][j].Length; k++)
                {
                    if (UnityEngine.Random.value < 0.5f)
                    {
                        weights[i][j][k] = other.weights[i][j][k];
                    }
                }
            }
        }
    }
}