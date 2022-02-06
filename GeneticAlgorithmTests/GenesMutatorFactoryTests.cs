using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optimiser.GeneticAlgorithm;
using System;
using System.Collections.Generic;
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
        static void TestVariousGeneLength(GenesMutatorFactory<int>.Mutate mutate, Action<IntChromosome, IntChromosome> assertFunc){
            int maxGeneLength = 1000;
            for (int geneLength = 2; geneLength < maxGeneLength; ++geneLength){
                var chromosome = IntChromosomeTests.CreateSampleIntChrosome(geneLength, 0);
                var originalChromosome = chromosome.Clone() as IntChromosome;
                mutate(ref chromosome);
                
                assertFunc(chromosome as IntChromosome, originalChromosome);
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
            var checkMutationCnt = new Action<IntChromosome, IntChromosome>((mutated, original)=>{
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
            var checkMutationCnt = new Action<IntChromosome, IntChromosome>((mutated, original)=>{
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
    }
}