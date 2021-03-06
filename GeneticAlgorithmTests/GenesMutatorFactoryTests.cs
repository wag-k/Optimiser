using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optimiser.GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Optimiser.GeneticAlgorithm.Tests
{
    [TestClass()]
    public class GenesMutatorFactoryTests
    {
        /// <summary>
        /// Test for various geneLength chromosomes
        /// </summary>
        /// <param name="mutate"></param>
        /// <param name="assertFunc">mutatedChromosome, originalChromosome</param>
        static void TestVariousGeneLength(GenesMutatorFactory<int>.Mutate mutate, Action<int[], int[]> assertFunc){
            int maxGeneLength = 1000;
            for (int geneLength = 2; geneLength < maxGeneLength; ++geneLength){
                var chromosome = IntChromosomeTests.CreateSampleIntChrosome(geneLength, 0);
                var originalChromosome = chromosome.Clone() as IntChromosome;
                var mutaedGenes = mutate(chromosome.Genes, chromosome.Allele);
                Assert.IsTrue(originalChromosome.Genes.SequenceEqual(chromosome.Genes));
                assertFunc(mutaedGenes, originalChromosome.Genes);
            }
        }

        [TestMethod()]
        public void CreateTest()
        {
            var mutateSingleLocus = GenesMutatorFactory<int>.Create(Mutation.SingleLocus);
            Assert.AreEqual("MutateSingleLocus", mutateSingleLocus.Method.Name);
        }

        [TestMethod()]
        public void MutateSingleLocusTest()
        {
            var mutateSingleLocus = GenesMutatorFactory<int>.Create(Mutation.SingleLocus);

            /// <summary>
            /// Mutation must be happened once
            /// </summary>
            var checkMutationCnt = new Action<int[], int[]>((mutated, original)=>{
                int totalMutateCnt = 0;
                for(int locus = 0; locus < original.Length; ++locus){
                    if (mutated[locus] != original[locus]){
                        totalMutateCnt++;
                    }
                }
                Assert.AreEqual(1, totalMutateCnt);
            });

            TestVariousGeneLength(mutateSingleLocus, checkMutationCnt);
        }
        
        [TestMethod()]
        public void MutateInversionTest()
        {
            var mutateInversion = GenesMutatorFactory<int>.Create(Mutation.Inversion);

            /// <summary>
            /// Inversion must be happened once
            /// </summary>
            var checkMutationCnt = new Action<int[], int[]>((mutated, original)=>{
                Assert.AreEqual(original.Length, mutated.Length);
                
                int totalMutateCnt = 0;
                int frontLocus, backLocus;
                for(frontLocus = 0; frontLocus < original.Length; ++frontLocus){
                    if (mutated[frontLocus] != original[frontLocus]){
                        totalMutateCnt++;
                        break;
                    }
                }
                /// <summary>
                /// was mutation detected?
                /// </summary>
                /// <param name="="></param>
                /// <returns></returns>
                Assert.AreEqual(1, totalMutateCnt);

                /// <summary>
                /// find inverted area
                /// </summary>
                /// <value></value>
                for (backLocus = original.Length-1; 0 <= backLocus; --backLocus){
                    if (mutated[backLocus] != original[backLocus]){
                        break;
                    }
                }

                var invertedLength = backLocus - frontLocus;
                for(int locus = 0; locus < invertedLength; ++locus){
                    Assert.AreEqual(mutated[frontLocus+locus], original[backLocus-locus]);
                }
            });

            TestVariousGeneLength(mutateInversion, checkMutationCnt);
        }
        
        [TestMethod()]
        public void MutateTranslocationTest()
        {
            var mutateTranslocation = GenesMutatorFactory<int>.Create(Mutation.Translocation);

            /// <summary>
            /// Translocaton must 
            /// - happen once
            /// - just translocate 
            /// 
            /// このテストは十分な検証ができていない。
            /// </summary>
            var checkTranslocation = new Action<int[], int[]>((mutated, original)=>{
                Assert.AreEqual(original.Length, mutated.Length);
                /*
                Debug.Write("original: ");
                foreach(var gene in original){
                    Debug.Write($"{gene},");
                }
                Debug.WriteLine("");
                Debug.Write("muta: ");
                foreach(var gene in mutated){
                    Debug.Write($"{gene},");
                }
                Debug.WriteLine("");
                */
                Assert.IsFalse(mutated.SequenceEqual(original));
            });

            TestVariousGeneLength(mutateTranslocation, checkTranslocation);
        }
    }
}