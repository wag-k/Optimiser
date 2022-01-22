using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optimiser.GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Optimiser.GeneticAlgorithm.Tests
{
    [TestClass()]
    public class GenesSelectorFactoryTests
    {
        [TestMethod()]
        public void CreateTest()
        {
            var selectByRoulette = GenesSelectorFactory<int>.Create(Selection.Roulette);
            Assert.AreEqual("SelectByRoulette", selectByRoulette.Method.Name);
        }
        
        [TestMethod()]
        public void SelectByRouletteTest()
        {
            var selectByRoulette = GenesSelectorFactory<int>.Create(Selection.Roulette);

            int maxEnthumbleSize = 10;
            int totalSelectCnt = 10000;
            for (int enthumbleSize = 2; enthumbleSize < maxEnthumbleSize; ++enthumbleSize){
                var chromosomes = IntChromosomeTests.CreateSampleIntChromosomes(enthumbleSize);
                var selectedLocusCounters = new int[enthumbleSize];
                
                for (int selectCnt = 0; selectCnt < totalSelectCnt; ++selectCnt){
                    var selectedPair = selectByRoulette(chromosomes);
                    foreach (var locus in selectedPair)
                    {
                        selectedLocusCounters[locus] += 1;
                    }
                }
                Debug.WriteLine($"EnthumbleSize: {enthumbleSize}");
                Debug.WriteLine(String.Join(",", selectedLocusCounters));
                for(int locus = 1; locus < selectedLocusCounters.Length; ++locus){
                    Assert.IsTrue(selectedLocusCounters[locus-1] <= selectedLocusCounters[locus]);
                }
            }
        }
        
    }
}