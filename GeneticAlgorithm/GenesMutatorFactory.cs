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
    public enum Mutation{
        SingleLocus = 0,
        Inversion,
        Translocation,
    }

    /// <summary>
    /// GenesMutatorFactory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class GenesMutatorFactory<T>
    {
        /// <summary>
        /// delegate of mutate method
        /// targetChromosome will be changed by mutation
        /// </summary>
        /// <param name="targetchromosome"></param>
        public delegate void Mutate(ref IChromosome<T> targetchromosome);

        public static Mutate Create(Mutation selection){
            switch (selection)
            {
                case Mutation.SingleLocus: 
                    return MutateSingleLocus;
                case Mutation.Inversion:
                case Mutation.Translocation: 
                default:
                    return null;
            }
        }
        
        static GenesMutatorFactory(){
        }

        /// <summary>
        /// Mutate at a single locus
        /// </summary>
        /// <param name="targetChromosome"></param>
        private static void MutateSingleLocus(ref IChromosome<T> targetChromosome){
            var locus = GeneManipulationUtility.GetRandomLocus(targetChromosome.Length);
            targetChromosome[locus] = targetChromosome.GetAllele(targetChromosome[locus]);
        }

        /// <summary>
        /// Chromosomal inversion
        /// Inverting the order of genes between two genes selected Randomly 
        /// </summary>
        /// <param name="targetChromsome"></param>
        private static void MutateInversion(ref IChromosome<T> targetChromsome){
            var selectedPair = GeneManipulationUtility.GetRandomLocusPair(targetChromsome.Length);
            var startLocus = selectedPair[0];
            var partialGenesLength = selectedPair[1] - selectedPair[0] +1;
            var partialGenes = new T[partialGenesLength];
            Array.Copy(targetChromsome.Genes, startLocus, partialGenes, 0, partialGenesLength);

            /// <summary>
            /// Invert
            /// </summary>
            /// <value></value>
            for (int locus = 0; locus < partialGenesLength; ++locus){
                targetChromsome[startLocus + locus] = partialGenes[partialGenesLength - 1 - locus];
            }
        }

    }
}
