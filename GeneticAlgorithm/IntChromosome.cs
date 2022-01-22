using System;
using System.Collections.Generic;
using System.Text;

namespace Optimiser.GeneticAlgorithm
{
    /// <summary>
    /// Chromosome of integer
    /// </summary>
    public class IntChromosome : AbstractChromosome<int>
    {
        /// <summary>
        /// Lower Limit of Gene
        /// </summary>
        /// <value></value>
        public int MinGene{
            get { return minGene;}
            set { minGene = value;}
        }
        int minGene = int.MinValue;

        /// <summary>
        /// Upper Limit of Gene
        /// </summary>
        /// <value></value>
        public int MaxGene{
            get { return maxGene;}
            set { maxGene = value;}
        }
        int maxGene = int.MaxValue;

        /// <summary>
        /// RandCounter is used for generating a random seed.
        /// </summary>
        /// <value></value>
        static int RandCounter {get; set;}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="genes"></param>
        /// <param name="fitness"></param>
        public IntChromosome(int[] genes, double fitness) 
        : base(genes, fitness){}

        /// <summary>
        /// Get a random value except gene
        /// </summary>
        /// <param name="gene"></param>
        /// <returns></returns>
        public override int GetAllele(int gene){
            var rand = new Random(DateTime.Now.GetHashCode() + RandCounter);
            var allele = gene;
            while (allele == gene)
            {
                allele = rand.Next(MinGene, MaxGene);
            }
            return allele;
        }
    }
}
