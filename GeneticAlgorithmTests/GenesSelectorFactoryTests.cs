﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optimiser.GeneticAlgorithm;
using System;
using System.Collections.Generic;
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
            Assert.AreEqual("SelectByRoulette", selectByRoulette.Method.Name);

            
        }
        
    }
}