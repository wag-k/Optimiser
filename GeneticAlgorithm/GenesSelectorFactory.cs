using System;
using System.Collections.Generic;
using System.Text;

using Algorithm;

namespace Optimiser.GeneticAlgorithm
{
    /// <summary>
    /// enum of selection method option
    /// </summary>
    public enum Selection{
        Roulette = 0,
        Ranking,
        Tournament,
    }

    /// <summary>
    /// GenesSelectorFactory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class GenesSelectorFactory<T>
    {
        /// <summary>
        /// delegate of selecting genes method
        /// </summary>
        /// <param name="chromosomes"> an ensenble of chromosomes</param>
        /// <returns></returns>
        public delegate int[] Select(IList<IChromosome<T>> chromosomes);

        static Random Rand { get; set; }


        public static Select Create(Selection selection){
            switch (selection)
            {
                case Selection.Roulette: 
                    return SelectByRoulette;
                case Selection.Ranking:
                case Selection.Tournament: 
                default:
                    return null;
            }
        }
        
        static GenesSelectorFactory(){
            Rand = new Random(DateTime.Now.GetHashCode());
        }

        /// <summary>
        /// Select locus pair by roulette
        /// </summary>
        /// <param name="chromosomes"></param>
        /// <returns></returns>
        public static int[] SelectByRoulette(IList<IChromosome<T>> chromosomes){
            double totalFitness = 0;
            foreach (var chromosome in chromosomes)
            {
                totalFitness += chromosome.Fitness;
            }
            
            /// <summary>
            /// 規格化した確率の和
            /// </summary>
            double integratedProbability = 0;
            /// <summary>
            /// if iP[k-1] < rand <= iP[k] => select k 
            /// </summary>
            var integratedProbabilities = new double[chromosomes.Count];
            for(int locus = 0; locus < chromosomes.Count; ++locus)
            {
                var chromosome = chromosomes[locus];
                var normalizedProbability = chromosome.Fitness / totalFitness;
                integratedProbability += normalizedProbability;
                integratedProbabilities[locus] = integratedProbability;
            }

            /// <summary>
            /// select locus pair randomly
            /// </summary>
            /// <returns></returns>
            var locusSelector1 = Rand.NextDouble();
            var locus1 = BinarySearch<double>.Search(integratedProbabilities, locusSelector1);
            var locus2 = locus1;
            while(locus2 == locus1){
                var locusSelector2 = Rand.NextDouble();
                locus2 = BinarySearch<double>.Search(integratedProbabilities, locusSelector2);
            }

            return new int[2]{locus1, locus2};
        }

    }
}
