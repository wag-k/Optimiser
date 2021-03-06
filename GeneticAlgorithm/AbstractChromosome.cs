using System;
using System.Collections.Generic;
using System.Text;

namespace Optimiser.GeneticAlgorithm
{
    /// <summary>
    /// AbstractChromosome
    /// </summary>
    /// <typeparam name="T">genotype</typeparam>
    public abstract class AbstractChromosome<T> : IChromosome<T>
    {
        /// <summary>
        /// Indexer can access to an each gene.
        /// </summary>
        /// <value></value>
        public T this[int locus]{
            get { return Genes[locus];}
            set { this.Genes[locus] = value;}
        }

        /// <summary>
        /// Array of Gene
        /// </summary>
        /// <value></value>
        public T[] Genes{
            get { return genes;}
            set { this.genes = value;}
        }
        T[] genes;

        /// <summary>
        /// Gene's Length
        /// </summary>
        /// <value></value>
        public int Length {get{return genes.Length;}}

        public double Fitness {get; set;}
        
        public AbstractChromosome(T[] genes, double fitness){
            Genes = genes;
            Fitness = fitness;
        }

        public abstract IChromosome<T>.GetAllele Allele{get;}

        public abstract object Clone();
    }
}
