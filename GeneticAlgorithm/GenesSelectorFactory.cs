using System;
using System.Collections.Generic;
using System.Text;

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
        public delegate T[] Select(IList<IChromosome<T>> chromosomes);

        public static Select Create(Selection selection){
            switch (selection)
            {
                case Selection.Roulette: 
                case Selection.Ranking:
                case Selection.Tournament: 
                default:
                    return null;
            }
        }
        
        public static T[][] SelectByRoulette(IList<IChromosome<T>> chromosomes){
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
            /// if iP[k-1] < rand < iP[k] => select k 
            /// </summary>
            var integratedProbabilities = new double[chromosomes.Count];
            for(int locus = 0; locus < chromosomes.Count; ++locus)
            {
                var chromosome = chromosomes[locus];
                var normalizedProbability = chromosome.Fitness / totalFitness;
                integratedProbability += normalizedProbability;
                integratedProbabilities[locus] = integratedProbability;
            }

            
            var selectedGenes = new T[2][];
            return selectedGenes;
        }
    }
}
