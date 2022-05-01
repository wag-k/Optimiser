using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optimiser.GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Optimiser.GeneticAlgorithm.Tests
{
    [TestClass()]
    public class GATests
    {
        static int _geneLength100 = 100;

        /// <summary>
        /// Create Random Chromosome with genelength 100
        /// </summary>
        /// <returns></returns>
        public static IChromosome<int> CreateSampleChromosome100()
        {
            return IntChromosomeTests.CreateSampleIntChrosome(_geneLength100, 0);
        }

        [TestMethod()]
        public void GeneticManipulationTest()
        {
            var ga = new GA<int>()
            {
                Selection = Selection.Roulette,
                CrossOverMethod = CrossOverMethod.TwoPoints,
                SingleMutationRate = 0.3,
                InversingMutationRate = 0.3,
                TranslocatingMutationRate = 0.3,
            };
            var testEnthumble = IntChromosomeTests.CreateSampleIntChromosomes(1000);
            var nextGenerationEnthumble = ga.GeneticManipulation(testEnthumble);
            Assert.AreEqual(1000, nextGenerationEnthumble.Count);
        }

        [TestMethod()]
        public void CreateInitialEnthumbleTest()
        {
            int enthumbleSize = 1000;
            var ga = new GA<int>(CreateSampleChromosome100)
            {
                EnthumbleSize = enthumbleSize,
            };
            var enthumble = ga.CreateInitialEnthumble();
            Assert.AreEqual(0, enthumble.Generation);
            Assert.AreEqual(enthumbleSize, enthumble.Chromosomes.Count);
            foreach (var chromosome in enthumble.Chromosomes)
            {
                Assert.AreEqual(100, chromosome.Length);
                for(int locus = 0; locus < chromosome.Length; ++locus)
                {
                    var gene = chromosome[locus];
                    Assert.IsTrue(0 <= gene && gene < int.MaxValue);
                }
                Assert.AreEqual(0, chromosome.Fitness);
            }
        }
    }
}