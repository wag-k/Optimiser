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
        public Selection Selection {get; set;}
        public CrossOverMethod CrossOverMethod {get; set;}

        public int MutationRate{ get; set; }
        ChromosomeEnthumble<T> CurrentEnthumble { get; set;}

        public List<T[]> GeneticManipulation(IList<IChromosome<T>> enthumble){
            var nextGenerationEnthumble = new List<T[]>();
            while(nextGenerationEnthumble.Count < enthumble.Count){
                var selectedGenesPair = Select(enthumble, Selection);
                var crossedGenesPair = CrossOver(
                    new T[2][]{
                        enthumble[selectedGenesPair[0]].Genes, 
                        enthumble[selectedGenesPair[1]].Genes
                    }, CrossOverMethod
                );
                for (int index = 0; index < crossedGenesPair.Length; ++index){
                    var nextGenes = crossedGenesPair[index];
                    Mutate(nextGenes, enthumble[index].Allele, Mutation.SingleLocus);
                }
                nextGenerationEnthumble.Add(crossedGenesPair[0]);
                nextGenerationEnthumble.Add(crossedGenesPair[1]);
            }
            return nextGenerationEnthumble;
        }

        public static int[] Select(IList<IChromosome<T>> chromosomes, Selection selection){
            var select = GenesSelectorFactory<T>.Create(selection);
            var selectedGenesPair = select(chromosomes);
            return selectedGenesPair;
        } 

        public static T[][] CrossOver(T[][] originalGenesPair, CrossOverMethod crossOverMethod){
            var crossOver = GenesCrosserFactory<T>.Create(crossOverMethod);
            return crossOver(originalGenesPair);
        }

        public static void Mutate(T[] genes, IChromosome<T>.GetAllele getAllele, Mutation mutation){
            var mutate = GenesMutatorFactory<T>.Create(mutation);
            mutate(genes, getAllele);
        }

    }
}
