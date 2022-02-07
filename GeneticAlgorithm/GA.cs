using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Optimiser.GeneticAlgorithm
{
    [DataContract]
    public class GA<T>
    {
        ChromosomeEnthumble<T> CurrentEnthumble { get; set;}

        /**
        public IEnumerable<T[]> GeneticManipulation(IList<IChromosome<T> enthumble){
            
        }
        */

        public static int[] Select(IList<IChromosome<T>> chromosomes, Selection selection){
            var select = GenesSelectorFactory<T>.Create(selection);
            var selectedGenesPair = select(chromosomes);
            return selectedGenesPair;
        } 

        public static T[][] CrossOver(T[][] originalGenesPair, CrossOverMethod crossOverMethod){
            var crossOver = GenesCrosserFactory<T>.Create(crossOverMethod);
            return crossOver(originalGenesPair);
        }

        public static void Mutate(ref IChromosome<T> chromosome, Mutation mutation){
            var mutate = GenesMutatorFactory<T>.Create(mutation);
            mutate(ref chromosome);
        }

    }
}
