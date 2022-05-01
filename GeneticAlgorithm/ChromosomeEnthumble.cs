using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Optimiser.GeneticAlgorithm
{
    /// <summary>
    /// Chromosome enthumble
    /// </summary>
    /// <typeparam name="T">GenoType</typeparam>
    [DataContract]
    public class ChromosomeEnthumble<T>
    {
        [DataMember (Name = "Generation")]
        public int Generation {get; set; }
        
        [DataMember (Name = "Chromosomes")]
        public IList<IChromosome<T>> Chromosomes { get; set;}
    }
}
