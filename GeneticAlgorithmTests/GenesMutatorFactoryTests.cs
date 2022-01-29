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
            /// Test for various geneLength chromosomes
            /// </summary>
            int maxGeneLength = 1000;
            for (int geneLength = 2; geneLength < maxGeneLength; ++geneLength){
                var chromosome = IntChromosomeTests.CreateSampleIntChrosome(geneLength, 0);
                var originalChromosome = chromosome.Clone() as IntChromosome;
                mutateSingleLocus(ref chromosome);
                
                /// <summary>
                /// Mutation must be happened once
                /// </summary>
                int totalMutateCnt = 0;
                for(int locus = 0; locus < geneLength; ++locus){
                    if (chromosome[locus] != originalChromosome[locus]){
                        totalMutateCnt++;
                    }
                }
                Assert.AreEqual(1, totalMutateCnt);
            }
        }
    }
}