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
        /// delegate of mutate method.
        /// </summary>
        /// <param name="targetGenes"></param>
        /// <param name="getAllele"></param>
        /// <returns>mutatedGenes</returns>
        public delegate T[] Mutate(T[] targetGenes, IChromosome<T>.GetAllele getAllele);

        public static Mutate Create(Mutation selection){
            switch (selection)
            {
                case Mutation.SingleLocus: 
                    return MutateSingleLocus;
                case Mutation.Inversion:
                    return MutateInversion;
                case Mutation.Translocation: 
                    return MutateTranslocation;
                default:
                    return null;
            }
        }
        
        static GenesMutatorFactory(){
        }

        /// <summary>
        /// Mutate at a single locus
        /// </summary>
        /// <param name="targetGenes"></param>
        private static T[] MutateSingleLocus(T[] targetGenes, IChromosome<T>.GetAllele getAllele){
            var mutatedGenes = GeneManipulationUtility.CopyGenes(targetGenes);
            var locus = GeneManipulationUtility.GetRandomLocus(mutatedGenes.Length);
            mutatedGenes[locus] = getAllele(mutatedGenes[locus]);
            return mutatedGenes;
        }

        /// <summary>
        /// Chromosomal inversion
        /// Inverting the order of genes between two genes selected Randomly 
        /// </summary>
        /// <param name="targetGenes"></param>
        private static T[] MutateInversion(T[] targetGenes, IChromosome<T>.GetAllele getAllele){
            var mutatedGenes = GeneManipulationUtility.CopyGenes(targetGenes);
            var selectedPair = GeneManipulationUtility.GetRandomLocusPair(targetGenes.Length);
            var startLocus = selectedPair[0];
            var partialGenesLength = selectedPair[1] - selectedPair[0] +1;
            var partialGenes = new T[partialGenesLength];
            Array.Copy(mutatedGenes, startLocus, partialGenes, 0, partialGenesLength);

            /// <summary>
            /// Invert
            /// </summary>
            /// <value></value>
            for (int locus = 0; locus < partialGenesLength; ++locus){
                mutatedGenes[startLocus + locus] = partialGenes[partialGenesLength - 1 - locus];
            }
            return mutatedGenes;
        }

        /// <summary>
        /// Chromosomal translocation
        /// </summary>
        /// <param name="targetGenes"></param>
        private static T[] MutateTranslocation(T[] targetGenes, IChromosome<T>.GetAllele getAllele){
            if(targetGenes.Length == 2){
                // just swap
                var mutatedGenes = GeneManipulationUtility.CopyGenes(targetGenes);
                var tempGene = mutatedGenes[0];
                mutatedGenes[0] = mutatedGenes[1];
                mutatedGenes[1] = tempGene;
                return mutatedGenes;
            }
            var selectedPair = GeneManipulationUtility.GetRandomLocusPair(targetGenes.Length-1);
            var startLocus = selectedPair[0];
            var partialGenesLength = selectedPair[1] - startLocus + 1;
            var partialGenes = new T[partialGenesLength];
            Array.Copy(targetGenes, startLocus, partialGenes, 0, partialGenesLength);

            // translocatedGenes will be translocated
            var translocatedGenes = new List<T>(targetGenes);

            // translocate
            translocatedGenes.RemoveRange(startLocus, partialGenesLength);
            var translocationPos = startLocus;
            while (translocationPos == startLocus){
                translocationPos = GeneManipulationUtility.GetRandomLocus(translocatedGenes.Count+1);
            }
            translocatedGenes.InsertRange(translocationPos, partialGenes);

            return translocatedGenes.ToArray();
        }


    }
}
