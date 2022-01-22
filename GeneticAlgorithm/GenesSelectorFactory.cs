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
            var selectedGenes = new T[2][];
            return selectedGenes;
        }
    }
}
