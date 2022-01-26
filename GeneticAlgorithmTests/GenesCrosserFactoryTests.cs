using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optimiser.GeneticAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Optimiser.GeneticAlgorithm.Tests
{
    [TestClass()]
    public class GenesCrosserFactoryTests
    {
        [TestMethod()]
        public void CrossOverInTwoPointsTest()
        {
            var crossOverInTwoPoints = GenesCrosserFactory<int>.Create(CrossOverMethod.TwoPoints);

            /// <summary>
            /// Check wehther crossCnr is less than 3
            /// </summary>
            /// <typeparam name="int[]"></typeparam>
            /// <typeparam name="int[][]"></typeparam>
            /// <typeparam name="bool"></typeparam>
            /// <returns></returns>
            var crossChecker = new Func<int[], int[], int[], bool>((genes, originalGenes1, originalGenes2)=>{
                int crossCnt = 0;
                var geneLength = genes.Length;
                for (int locus = 0; locus < geneLength; ++locus){
                    if(crossCnt == 1){
                        if(genes[locus] != originalGenes2[locus]){
                            Assert.AreEqual(genes[locus], originalGenes1[locus]);
                            crossCnt++;
                        }
                    } else if (crossCnt == 0 || crossCnt == 2){
                        if(genes[locus] != originalGenes1[locus]){
                            Assert.AreEqual(genes[locus], originalGenes2[locus]);
                            crossCnt++;
                        }
                    } else {
                        Assert.Fail();
                        return false;
                    }
                }
                return crossCnt <= 2;
            });

            int maxGeneLength = 1000;
            /// <summary>
            /// test with various gene length
            /// </summary>
            /// <value></value>
            for (int geneLength = 2; geneLength < maxGeneLength; ++geneLength){
                var genesPair = (
                    from genes in Enumerable.Range(0,2)
                        select IntChromosomeTests.CreateRandomIntGenes(geneLength)
                ).ToArray();
                var crossedGenesPair = crossOverInTwoPoints(genesPair);

                Assert.IsTrue(crossChecker(crossedGenesPair[0], genesPair[0], genesPair[1]));
                Assert.IsTrue(crossChecker(crossedGenesPair[1], genesPair[1], genesPair[0]));
            }
        }
    }
}