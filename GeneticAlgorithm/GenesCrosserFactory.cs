using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// CrossOver Genes in two points
        /// </summary>
        /// <param name="originalGenesPair"></param>
        /// <returns>crossed genes pair</returns>
        public static T[][] CrossOverInTwoPoints(T[][] originalGenesPair){
            if (originalGenesPair.Length != 2){
                throw new ArgumentException($"{nameof(CrossOverInTwoPoints)}Error: originalGenesPair Length must be 2");
            }
            var locusPair = GeneManipulationUtility.GetRandomLocusPair(originalGenesPair.Length);
            var startLocus = locusPair[0];
            var endLocus = locusPair[1];
            var crossedLength = endLocus - startLocus;
            var crossedGenesArea1 = 
                (from locus in Enumerable.Range(startLocus, crossedLength)
                    select originalGenesPair[0][locus]
                ).ToArray();
            var crossedGenes1 = new T[originalGenesPair[0].Length];
            Array.Copy(originalGenesPair[0], crossedGenes1, crossedGenes1.Length);
            var crossedGenes2 = new T[originalGenesPair[1].Length];
            Array.Copy(originalGenesPair[1], crossedGenes2, crossedGenes2.Length);

            for (int index = 0; index < crossedLength; ++ index){
                crossedGenes1[startLocus + index] = crossedGenes2[startLocus + index];
                crossedGenes2[startLocus + index] = crossedGenesArea1[index];
            }

            return new T[2][]{crossedGenes1, crossedGenes2};
        }

    }
}
