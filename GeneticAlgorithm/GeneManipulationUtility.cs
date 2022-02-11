using System;
using System.Collections.Generic;
using System.Text;

namespace Optimiser.GeneticAlgorithm
{
    /// <summary>
    /// Utility of gene manipulation
    /// </summary>
    /// <typeparam name="T">Genotype</typeparam>
    public static class GeneManipulationUtility
    {
        static Random Rand { get; set;}
        
        static GeneManipulationUtility(){
            Rand = new Random(DateTime.Now.GetHashCode());
        }
        /// <summary>
        /// Get a RandomLocus
        /// </summary>
        /// <param name="geneLength"></param>
        /// <returns>locus< locus2</returns>
        public static int GetRandomLocus(int geneLength){
            var locus1 = Rand.Next(0, geneLength);
            return locus1;
        }
    
        /// <summary>
        /// getRandomLocusPair
        /// </summary>
        /// <param name="geneLength"></param>
        /// <returns>locua1 != locus2 && locus1 < locus2</returns>
        public static int[] GetRandomLocusPair(int geneLength){
            if(geneLength < 2){
                throw new ArgumentException($"{nameof(GetRandomLocusPair)}Error: geneLength must be greater than 2");
            }
            var locus1 = Rand.Next(0, geneLength);
            var locus2 = locus1;
            while(locus1 == locus2){
                locus2 = Rand.Next(0, geneLength);
            }
            var sortedPair = locus1<locus2 ? new int[2]{locus1, locus2} : new int[2]{locus2 , locus1};
            return sortedPair;
        }

        /// <summary>
        /// CopyGenes
        /// </summary>
        /// <param name="original"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>copiedGenes</returns>
        public static T[] CopyGenes<T>(T[] original){
            var copied = new T[original.Length];
            Array.Copy(original, copied, original.Length);
            return copied;
        }
    }
}
