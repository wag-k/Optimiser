using System;
using System.Collections.Generic;
using System.Text;

namespace Optimiser.GeneticAlgorithm
{
    /// <summary>
    /// Interface of Chromosome
    /// </summary>
    /// <typeparam name="T">Genotype</typeparam>
    public interface IChromosome<T> : ICloneable
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
        /// Chromosome's Fitness
        /// </summary>
        /// <value></value>
        double Fitness {get;}

        delegate T GetAllele(T gene);
        /// <summary>
        /// Get an allele
        /// </summary>
        /// <returns></returns>
        GetAllele Allele{get;}
    }
}
