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

        public double SingleMutationRate{ 
            get {return singleMutationRate;}
            set {
                if(0 <= singleMutationRate && singleMutationRate <= 1){
                    singleMutationRate = value;
                }else{
                    throw new ArgumentException($"{nameof(SingleMutationRate)} must be 0 <= rate <= 1");
                }
            }
        }
        double singleMutationRate;
        public double InversingMutationRate{ 
            get {return inversingMutationRate;}
            set {
                if(0 <= inversingMutationRate && inversingMutationRate <= 1){
                    inversingMutationRate = value;
                }else{
                    throw new ArgumentException($"{nameof(SingleMutationRate)} must be 0 <= rate <= 1");
                }
            }
        }
        double inversingMutationRate;
        public double TranslocatingMutationRate{ 
            get {return translocatingMutationRate;}
            set {
                if(0 <= translocatingMutationRate && translocatingMutationRate <= 1){
                    translocatingMutationRate = value;
                }else{
                    throw new ArgumentException($"{nameof(SingleMutationRate)} must be 0 <= rate <= 1");
                }
            }
        }
        double translocatingMutationRate;
        ChromosomeEnthumble<T> CurrentEnthumble { get; set;}

        static Random Rand{
            get {return rand;} 
        }
        static Random rand = new Random();

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
                    if(Rand.NextDouble() < SingleMutationRate){
                        nextGenes = Mutate(nextGenes, enthumble[index].Allele, Mutation.SingleLocus);
                    }
                    if(Rand.NextDouble() < InversingMutationRate){
                        nextGenes = Mutate(nextGenes, enthumble[index].Allele, Mutation.Inversion);
                    }
                    if(Rand.NextDouble() < TranslocatingMutationRate){
                        nextGenes = Mutate(nextGenes, enthumble[index].Allele, Mutation.Translocation);
                    }
                    nextGenerationEnthumble.Add(nextGenes);
                }
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

        public static T[] Mutate(T[] genes, IChromosome<T>.GetAllele getAllele, Mutation mutation){
            var mutate = GenesMutatorFactory<T>.Create(mutation);
            return mutate(genes , getAllele);
        }

    }
}
