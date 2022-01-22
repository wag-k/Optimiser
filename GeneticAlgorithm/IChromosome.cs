using System;
using System.Collections.Generic;
using System.Text;

namespace Optimiser.GeneticAlgorithm
{
    /// <summary>
    /// Interface of Chromosome
    /// </summary>
    /// <typeparam name="T">Genotype</typeparam>
    public interface IChromosome<T>
    {
        /// <summary>
        /// Indexer can be accessible to an each gene.
        /// </summary>
        /// <value></value>
        T this[int locus]{ get ; set; }

        /// <summary>
        /// Array of Gene
        /// </summary>
        /// <value></value>
        T[] Genes{get; set;}

        /// <summary>
        /// Gene's Length
        /// </summary>
        /// <value></value>
        int Length {get;}


        /// <summary>
        /// Get an allele
        /// </summary>
        /// <returns></returns>
        T GetAllele(T gene);
    }
}
