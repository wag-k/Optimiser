using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optimiser.GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Optimiser.GeneticAlgorithm.Tests
{
    [TestClass()]
    public class GeneManipulationUtilityTests
    {
        [TestMethod()]
        public void GetRandomLocusPairTest()
        {
            int maxGeneLength = 10000;
            for(int geneLength = 2; geneLength < maxGeneLength; ++geneLength){
                var locusPair = GeneManipulationUtility.GetRandomLocusPair(geneLength);
                Assert.IsTrue(locusPair[0] != locusPair[1]);
                Assert.IsTrue(locusPair[0] < locusPair[1]);
                Assert.IsTrue(0 <= locusPair[0]);
                Assert.IsTrue(locusPair[1] < geneLength);
            }

        }
    }
}