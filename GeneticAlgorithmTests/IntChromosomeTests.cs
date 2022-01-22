using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optimiser.GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Optimiser.GeneticAlgorithm.Tests
{
    [TestClass()]
    public class IntChromosomeTests
    {
        static Random Rand {get; set;}
        static IntChromosomeTests(){
            Rand = new Random(DateTime.Now.GetHashCode());
        }

        public static int[] CreateRandomIntGenes(int geneLength){
            var genes = (
                from index in Enumerable.Range(0, geneLength)
                select Rand.Next()
            ).ToArray();
            return genes;
        }

        public static IntChromosome CreateSampleIntChrosome(int geneLength, double fitness){
            var genes = CreateRandomIntGenes(geneLength);
            return new IntChromosome(genes, fitness);
        }

        public static List<IntChromosome> CreateSampleIntChromosomes(int numChromosome){
            var chromosomes = (
                from index in Enumerable.Range(0, numChromosome)
                let geneLength = index + 2 // minGeneLength = 2
                select CreateSampleIntChrosome(geneLength, index)
            ).ToList();
            return chromosomes;
        }

        /// <summary>
        /// various gene length test
        /// </summary>
        [TestMethod()]
        public void GetAlleleTest1()
        {
            var rand = new Random(DateTime.Now.GetHashCode());
            int totalTestCnt = 1000;
            for(int geneLength = 2; geneLength <= totalTestCnt; ++geneLength){
                var chromosome = CreateSampleIntChrosome(geneLength, geneLength);
                int gene = rand.Next();
                Assert.IsFalse(gene == chromosome.GetAllele(gene));
            }
        }
    }
}