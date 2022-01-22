using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithm
{
    /// <summary>
    /// Chromosome
    /// </summary>
    /// <typeparam name="T">Genotype</typeparam>
    public class Chromosome<T>
    {
        /// <summary>
        /// Indexer can be accessible to an each gene.
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
        int Length {get{return genes.Length;}}

        /// <summary>
        /// Delegate of Getting an allele
        /// </summary>
        /// <param name="gene"></param>
        /// <returns></returns>
        public delegate T AlleleFunction(T gene);

        /// <summary>
        /// Registered GetAllele Function
        /// </summary>
        public static AlleleFunction GetAlleleFunction{ get; set;}

        /// <summary>
        /// Get an allele
        /// </summary>
        /// <param name="GetAlleleDelegate(gene"></param>
        /// <returns></returns>
        public static T GetAllele(T gene) {return GetAlleleFunction(gene);}
    }
}
