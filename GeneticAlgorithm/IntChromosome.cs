using System;
using System.Collections.Generic;
using System.Text;

namespace Optimiser.GeneticAlgorithm
{
    /// <summary>
    /// Chromosome of integer
    /// </summary>
    public class IntChromosome : IChromosome<int>
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
        /// Indexer can be accessible to an each gene.
        /// </summary>
        /// <value></value>
        public int this[int locus]{
            get { return Genes[locus];}
            set { this.Genes[locus] = value;}
        }

        /// <summary>
        /// Array of Gene
        /// </summary>
        /// <value></value>
        public int[] Genes{
            get { return genes;}
            set { this.genes = value;}
        }
        int[] genes;

        /// <summary>
        /// Gene's Length
        /// </summary>
        /// <value></value>
        public int Length {get{return genes.Length;}}

        /// <summary>
        /// RandCounter is used for generating a random seed.
        /// </summary>
        /// <value></value>
        static int RandCounter {get; set;}

        /// <summary>
        /// Get a random value except gene
        /// </summary>
        /// <param name="gene"></param>
        /// <returns></returns>
        public int GetAllele(int gene){
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
