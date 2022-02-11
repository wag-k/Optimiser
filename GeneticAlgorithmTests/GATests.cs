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
        [TestMethod()]
        public void GeneticManipulationTest()
        {
            var ga = new GA<int>(){
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
    }
}