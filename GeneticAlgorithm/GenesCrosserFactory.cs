using System;
using System.Collections.Generic;
using System.Text;

using Algorithm;

namespace Optimiser.GeneticAlgorithm
{
    /// <summary>
    /// enum of cross over method option
    /// </summary>
    public enum CrossOverMethod{
        OnePoint = 0,
        TwoPoints,
        Uniform,
    }

    /// <summary>
    /// GenesCrosserFactory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class GenesCrosserFactory<T>
    {
        /// <summary>
        /// delegate of Crossing over genes pair method
        /// </summary>
        /// <param name="chromosomes"> an ensenble of chromosomes</param>
        /// <returns></returns>
        public delegate T[][] CrossOver(T[][] originalGenesPair);

        static Random Rand { get; set; }


        public static CrossOver Create(CrossOverMethod selection){
            switch (selection)
            {
                case CrossOverMethod.OnePoint: 
                case CrossOverMethod.TwoPoints:
                case CrossOverMethod.Uniform: 
                default:
                    return null;
            }
        }
        
        static GenesCrosserFactory(){
            Rand = new Random(DateTime.Now.GetHashCode());
        }

    }
}
