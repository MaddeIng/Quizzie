using NUnit.Framework;
using Quizzie.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quizzie
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test()
        {
            QuizzieDBContext context = new QuizzieDBContext();

            var quizExists = context.Quizs
                .Any(q => q.ID == 9999);

            Assert.AreEqual(quizExists, true);
        }
    }
}