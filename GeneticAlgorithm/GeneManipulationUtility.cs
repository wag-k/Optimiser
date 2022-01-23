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
    }
}
