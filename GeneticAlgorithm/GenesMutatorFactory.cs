﻿using System;
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

        /// <summary>
        /// Chromosomal translocation
        /// </summary>
        /// <param name="targetChromosome"></param>
        private static void MutateTranslocation(ref IChromosome<T> targetChromosome){
            if(targetChromosome.Length == 2){
                // just swap
                var tempGene = targetChromosome[0];
                targetChromosome[0] = targetChromosome[1];
                targetChromosome[1] = tempGene;
                return;
            }
            var selectedPair = GeneManipulationUtility.GetRandomLocusPair(targetChromosome.Length-1);
            var startLocus = selectedPair[0];
            var targetGenes = new T[targetChromosome.Length];
            Array.Copy(targetChromosome.Genes, targetGenes, targetGenes.Length);
            var partialGenesLength = selectedPair[1] - startLocus + 1;
            var partialGenes = (
                from locus in Enumerable.Range(startLocus, partialGenesLength)
                select targetGenes[locus]
            );

            // translocatedGenes will be translocated
            var translocatedGenes = new List<T>(targetGenes);

            // translocate
            translocatedGenes.RemoveRange(startLocus, partialGenesLength);
            var translocationPos = startLocus;
            while (translocationPos == startLocus){
                translocationPos = GeneManipulationUtility.GetRandomLocus(translocatedGenes.Count+1);
            }
            translocatedGenes.InsertRange(translocationPos, partialGenes);

            targetChromosome.Genes = translocatedGenes.ToArray();
        }


    }
}
